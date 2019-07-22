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
	public abstract class StarverNPC : BaseNPC
	{
		#region Fields
		protected float[] AIUsing;
		protected int RawType;
		protected int DefaultLife;
		protected int DefaultDefense;
		protected DateTime LastSpawn = DateTime.Now;
		protected StarverNPC Root;
		protected SpawnChecker Checker;
		protected abstract void RealAI();
		#endregion
		#region ctor
		protected StarverNPC(int AIs = 0)
		{
			AIUsing = new float[AIs];
		}
		static StarverNPC()
		{
			Type[] types = System.Reflection.Assembly.GetExecutingAssembly().GetTypes();
			foreach(var type in types)
			{
				if (type.IsAbstract || IsBossFollow(type) || !type.IsSubclassOf(typeof(StarverNPC))) 
				{
					continue;
				}
				NPCTypes.Add(type);
				RootNPCs.Add(Activator.CreateInstance(type) as StarverNPC);
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
		#region CheckSpawn
		/// <summary>
		/// 检查是否满足创建NPC条件
		/// </summary>
		/// <param name="player"></param>
		/// <returns></returns>
		protected virtual bool CheckSpawn(StarverPlayer player)
		{
			return StarverConfig.Config.TaskNow >= Checker.Task && Checker.Match(player.GetSpawnChecker()) && Timer % Checker.SpawnRate == 0 && Rand.NextDouble() < Checker.SpawnChance;
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
					if (Target == None || Vector2.Distance(TargetPlayer.Center, Center) > 16 * 400)
					{
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
		#region Globals
		protected static int SpawnTimer;
		protected static List<Type> NPCTypes = new List<Type>();
		protected static List<StarverNPC> RootNPCs = new List<StarverNPC>();
		protected static int NewNPC<T>(Vector where, Vector Velocity)
			where T : StarverNPC, new()
		{
			StarverNPC npc = new T();
			npc._active = true;
			npc.Index = NewNPCStatic(where, Velocity, npc.RawType, npc.DefaultLife, npc.DefaultDefense);
			npc.RealNPC.life = npc.DefaultDefense;
			npc.RealNPC.defense = npc.DefaultDefense;
			npc.OnSpawn();
			Starver.NPCs[npc.Index] = NPCs[npc.Index] = npc;
			return npc.Index;
		}
		protected static int NewNPC(Vector where, Vector Velocity,Type NPCType)
		{
			if(NPCType.IsAbstract == false && NPCType.IsSubclassOf(typeof(StarverNPC)))
			{
				StarverNPC npc = Activator.CreateInstance(NPCType) as StarverNPC;
				npc._active = true;
				npc.Index = NewNPCStatic(where, Velocity, npc.RawType, npc.DefaultLife, npc.DefaultDefense);
				npc.RealNPC.life = npc.DefaultDefense;
				npc.RealNPC.defense = npc.DefaultDefense;
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
			StarverNPC npc = Root.MemberwiseClone() as StarverNPC;
			npc._active = true;
			npc.Index = NewNPCStatic(where, Velocity, npc.RawType, npc.DefaultLife, npc.DefaultDefense);
			npc.RealNPC.life = npc.DefaultDefense;
			npc.RealNPC.defense = npc.DefaultDefense;
			npc.OnSpawn();
			Starver.NPCs[npc.Index] = NPCs[npc.Index] = npc;
			return npc.Index;
		}
		protected static bool IsBossFollow(Type type)
		{
			return type == typeof(NPCs.BrainFollow) || type == typeof(NPCs.PrimeExArm);
		}




		public static StarverNPC[] NPCs = new StarverNPC[Terraria.Main.maxNPCs];
		public static void DoUpDate(object args)
		{
			SpawnTimer++;
			foreach (var player in Starver.Players)
			{
				if (player is null || !player.Active)
				{
					continue;
				}
				foreach (var npc in RootNPCs)
				{
					if (npc.CheckSpawn(player))
					{
						NewNPC((Vector)(player.Center + Rand.NextVector2(16 * 8)), Vector.Zero, npc);
					}
				}
			}
		}
		#endregion
	}
}
