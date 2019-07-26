using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TShockAPI;

namespace Starvers.NPCSystem
{
	using Vector = TOFOUT.Terraria.Server.Vector2;
	public abstract class StarverNPC : BaseNPC , ICloneable
	{
		#region Fields
		protected float[] AIUsing;
		protected int[] Types;
		protected int AIStyle = 1;
		protected int RawType;
		protected int DefaultLife;
		protected int DefaultDefense;
		protected int CollideDamage;
		protected bool NoTileCollide;
		protected DateTime LastSpawn = DateTime.Now;
		protected StarverNPC Root;
		protected SpawnChecker Checker;
		protected abstract void RealAI();
		#endregion
		#region Properties
		/// <summary>
		/// 碰撞伤害系数
		/// </summary>
		protected virtual float CollidingIndex { get; set; } = 1;
		public override float DamageIndex
		{
			get
			{
				return (StarverConfig.Config.TaskNow - 19) / 4f + 1;
			}
		}
		#endregion
		#region ctor
		protected StarverNPC(int AIs = 0)
		{
			AIUsing = new float[AIs];
			Name = GetType().Name;
		}
		static StarverNPC()
		{
			Type[] types = System.Reflection.Assembly.GetExecutingAssembly().GetTypes();
			foreach (var type in types)
			{
				if (type.IsAbstract || IsBossFollow(type) || !type.IsSubclassOf(typeof(StarverNPC)))
				{
					continue;
				}
				NPCTypes.Add(type);
				RootNPCs.Add(Activator.CreateInstance(type) as StarverNPC);
#if DEBUG
				//StarverPlayer.All.SendDeBugMessage($"{RootNPCs[RootNPCs.Count - 1].Name} Loaded");
				Console.ForegroundColor = ConsoleColor.Blue;
				Console.WriteLine($"{RootNPCs[RootNPCs.Count - 1].Name} Loaded");
				Console.ResetColor();
#endif
			}
		}
		#endregion
		#region Spawn
		public virtual bool Spawn(Vector where)
		{
			_active = true;
			Index = NewNPC(where, Vector.Zero, RawType, DefaultLife, DefaultDefense);
			return true;
		}
		#endregion
		#region DefenseIndex
		protected virtual float DefenseIndex
		{
			get
			{
				return (StarverConfig.Config.TaskNow - 20) / 2f;
			}
		}
		#endregion
		#region OnSpawn
		public override void OnSpawn()
		{
			#region Varient Types
			if (Types != null)
			{
				RawType = Types[Rand.Next(Types.Length)];
			}
			#endregion
			RealNPC.SetDefaults(RawType);
			RealNPC.life = DefaultLife;
			RealNPC.lifeMax = DefaultLife;
			RealNPC.aiStyle = AIStyle;
			RealNPC.noTileCollide = NoTileCollide;
			RealNPC.defense = (int)(DefaultDefense * DefenseIndex);
			if(CollideDamage > 0)
			{
				RealNPC.damage = CollideDamage;
			}
			RealNPC.damage = (int)(RealNPC.damage * CollidingIndex);
			SendData();
#if DEBUG
			StarverPlayer.All.SendDeBugMessage($"{Name}({this.Index}) has Spawned");
#endif
		}
		#endregion
		#region CheckSpawn
		/// <summary>
		/// 检查是否满足创建NPC条件
		/// </summary>
		/// <param name="player"></param>
		/// <returns></returns>
		protected virtual bool CheckSpawn(StarverPlayer player)
		{
			bool Flag = StarverConfig.Config.TaskNow >= Checker.Task && Checker.Match(player.GetSpawnChecker()) && SpawnTimer % Checker.SpawnRate == 0 && Rand.NextDouble() < Checker.SpawnChance;
			return Flag;
		}
		#endregion
		#region ToString
		public override string ToString()
		{
			return $"{Name}:Index({Index}),_active({_active}),RealNPC({RealNPC})";
		}
		#endregion
		#region CheckSecond
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		protected bool CheckSecond(double seconds)
		{
			return Timer % (int)(seconds * 60) == 0;
		}
		#endregion
		#region AI
		public override void AI(object args = null)
		{
			if (!Active)
			{
				return;
			}
			if (RealNPC.aiStyle == None)
			{
				if (Target < 0 || Target > 40 || TargetPlayer == null || !TargetPlayer.Active)
				{
					TargetClosest();
					if (Target == None || Vector2.Distance(TargetPlayer.Center, Center) > 16 * 500)
					{
#if DEBUG
						StarverPlayer.All.SendDeBugMessage($"{Name} killed itself");
#endif
						KillMe();
						return;
					}
				}
				Velocity = FakeVelocity;
			}
			RealAI();
			SendData();
			++Timer;
		}
		#endregion
		#region Collide
		private static void Collide()
		{
			bool Handled;
			Terraria.NPC RealNPC;
			for (int i = 0; i < Terraria.Main.maxNPCs; i++)
			{
				if(!Terraria.Main.npc[i].active)
				{
					continue;
				}
				foreach (var ply in Starver.Players)
				{
					if (ply == null || !ply.Active)
					{
						continue;
					}
					RealNPC = Terraria.Main.npc[i];
					Handled = false;
					if (
						RealNPC.position.X - ply.TPlayer.width < ply.TPlayer.position.X &&
						ply.TPlayer.position.X < RealNPC.position.X + RealNPC.width &&
						RealNPC.position.Y - ply.TPlayer.height < ply.Position.Y &&
						ply.Position.Y < RealNPC.position.Y + RealNPC.height
						)
					{
						if (NPCs[i] != null && NPCs[i].Active)
						{
							NPCs[i].OnCollide(ply, ref Handled);
						}
						if ((!Handled) && RealNPC.damage > 0 && !ply.TPlayer.immune)
						{
							ply.Damage(RealNPC.damage);
						}
					}
				}
			}
		}
		#endregion
		#region OnCollide
		/// <summary>
		/// 
		/// </summary>
		/// <param name="player">撞上那个玩家</param>
		/// <param name="handled">是否处理完毕</param>
		protected virtual void OnCollide(StarverPlayer player,ref bool handled)
		{

		}
		#endregion
		#region Globals
		private static int SpawnTimer;
		protected static int counting;
		protected static int count;
		protected static int Count
		{
			get
			{
				return count;
			}
			set
			{
				count = value;
#if DEBUG
				StarverPlayer.All.SendDeBugMessage($"Count: {count}, MaxSpawns: {TShock.Config.DefaultMaximumSpawns * Alives}");
#endif
			}
		}
		protected static int Alives;
		protected static List<Type> NPCTypes = new List<Type>();
		protected static List<StarverNPC> RootNPCs = new List<StarverNPC>();
		protected static int NewNPC<T>(Vector where, Vector Velocity)
			where T : StarverNPC, new()
		{
			StarverNPC npc = new T();
			npc._active = true;
			npc.Index = NewNPCStatic(where, Velocity, npc.RawType, npc.DefaultLife, npc.DefaultDefense);
			npc.OnSpawn();
			Starver.NPCs[npc.Index] = NPCs[npc.Index] = npc;
			return npc.Index;
		}
		protected static int NewNPC(Vector where, Vector Velocity, Type NPCType)
		{
			if (NPCType.IsAbstract == false && NPCType.IsSubclassOf(typeof(StarverNPC)))
			{
				StarverNPC npc = Activator.CreateInstance(NPCType) as StarverNPC;
				npc._active = true;
				npc.Index = NewNPCStatic(where, Velocity, npc.RawType, npc.DefaultLife, npc.DefaultDefense);
				npc.OnSpawn();
				Starver.NPCs[npc.Index] = NPCs[npc.Index] = npc;
				return npc.Index;
			}
			else
			{
				throw new ArgumentException($"{NPCType.Name}不是StarverNPC的子类或是抽象类");
			}
		}
		protected static int NewNPC(Vector where, Vector Velocity, StarverNPC Root)
		{
			StarverNPC npc = Root.Clone() as StarverNPC;
			npc._active = true;
			npc.Spawn(where);
			npc.RealNPC.velocity = Velocity;
			npc.OnSpawn();
			Starver.NPCs[npc.Index] = NPCs[npc.Index] = npc;
			return npc.Index;
		}
		protected static bool IsBossFollow(Type type)
		{
			return type == typeof(NPCs.BrainFollow) || type == typeof(NPCs.PrimeExArm);
		}
		protected static void UpdateCount()
		{
			counting = 0;
			foreach(var npc in NPCs)
			{
				if(npc is null)
				{
					continue;
				}
				if(npc.Active)
				{
					counting++;
				}
			}
			if (Count != counting)
			{
				Count = counting;
			}
		}




		public static StarverNPC[] NPCs = new StarverNPC[Terraria.Main.maxNPCs];
		public static void DoUpDate(object args)
		{
			SpawnTimer++;
			if(SpawnTimer % 60 == 0)
			{
				UpdateCount();
				Alives = AlivePlayers();
			}
			foreach (var player in Starver.Players)
			{
				if (player is null || !player.Active)
				{
					continue;
				}
				foreach (var npc in RootNPCs)
				{
					if (Count < Alives * TShock.Config.DefaultMaximumSpawns && npc.CheckSpawn(player)) 
					{
						NewNPC((Vector)(player.Center + Rand.NextVector2(16 * 20)), Vector.Zero, npc);
					}
				}
			}
			if (SpawnTimer % 3 == 0)
			{
				Collide();
			}
		}
		public static void OnNPCKilled(TerrariaApi.Server.NpcKilledEventArgs args)
		{
			StarverNPC npc = NPCs[args.npc.whoAmI];
			if (npc._active)
			{
				npc._active = false;
				npc.OnDead();
				npc.KillMe();
			}
		}
		#endregion
		#region Clone
		public object Clone()
		{
			return MemberwiseClone();
		}
		#endregion
	}
}
