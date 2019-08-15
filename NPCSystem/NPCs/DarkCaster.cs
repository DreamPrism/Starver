using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Starvers.TaskSystem;

namespace Starvers.NPCSystem.NPCs
{
	using TOFOUT.Terraria.Server;
	using Vector = TOFOUT.Terraria.Server.Vector2;
	public class DarkCaster : StarverNPC
	{
		#region Fields
		protected delegate void MyAIDelegate(DarkCaster This);
		/// <summary>
		/// 长度为MaxInternalAIStyle
		/// </summary>
		protected static MyAIDelegate[] InternalAIs = new MyAIDelegate[MaxInternalAIStyle]
		{
			AI_0,
			AI_1,
			AI_2
		};
		/// <summary>
		/// <para>0 : 一次3幽灵同时发出</para>
		/// <para>1 : 直接在玩家身边召唤幽灵</para>
		/// <para>2 : 5连发</para>
		/// </summary>
		protected int InternalAIStyle;
		protected const int MaxInternalAIStyle = 3;
		protected static SpawnChecker DungeonChecker = SpawnChecker.DungeonLike;
		#endregion
		#region Properties
		protected override float CollidingIndex => DamageIndex;
		#endregion
		#region ctor
		public DarkCaster() : base()
		{
			AfraidSun = true;
			SpaceOption |= SpawnSpaceOptins.InScreen;
			RawType = NPCID.DarkCaster;
			DefaultLife = 15000;
			DefaultDefense = 630000;
			Checker = SpawnChecker.RareNight;
			Checker.Task = TaskID.SkeletronEx;
			CollideDamage = 400;
			DamagedIndex = 0.1f;
			AIStyle = None;
			NoGravity = true;
		}
		#endregion
		#region Spawn
		public override void OnSpawn()
		{
			base.OnSpawn();
			InternalAIStyle = Rand.Next(MaxInternalAIStyle);
		}
		#endregion
		#region RealAI
		protected override void RealAI()
		{
			InternalAIs[InternalAIStyle](this);
		}
		#endregion
		#region AIs
		protected static void AI_0(DarkCaster This)
		{
			if(This.Timer % (60 * 3 / 2) == 0)
			{
				This.Vel = (Vector)(This.TargetPlayer.Center - This.Center);
				This.ProjSector(This.Center, 8, 3, This.Vel.Angle, PI / 3, 200, ProjectileID.LostSoulHostile, 3);
			}
		}
		protected static void AI_1(DarkCaster This)
		{
			uint Mod = This.Timer % (60 * 4);
			if (Mod == 0)
			{
				This.ProjCircle(This.TargetPlayer.Center, 16 * 30, 5, ProjectileID.LostSoulHostile, 8, 200);
			}
		}
		protected static void AI_2(DarkCaster This)
		{
			uint Mod = This.Timer % (60 * 4);
			switch(Mod)
			{
				case 0:
				case 20:
				case 40:
				case 60:
				case 80:
					This.Vel = (Vector)(This.TargetPlayer.Center - This.Center);
					This.Vel.Length = 9;
					This.Proj(This.Center, This.Vel, ProjectileID.LostSoulHostile, 200);
					break;
				case 60 * 4 - 1:
					This.Warp();
					break;
			}
		}
		#endregion
		#region OnStrike
		public override void OnStrike(int RealDamage, float KnockBack, StarverPlayer player)
		{
			base.OnStrike(RealDamage, KnockBack, player);
			Warp();
			SendData();
		}
		#endregion
		#region Warp
		protected void Warp()
		{
			Position = CalcSpawnPosInScreen((Vector)TargetPlayer.Center);
		}
		#endregion
		#region Check
		protected override bool CheckSpawn(StarverPlayer player)
		{
			var PlayerChecker = player.GetSpawnChecker();
			return StarverConfig.Config.TaskNow >= Checker.Task && (Checker.Match(PlayerChecker) || DungeonChecker.Match(PlayerChecker)) && SpawnTimer % Checker.SpawnRate == 0 && Rand.NextDouble() < Checker.SpawnChance;
		}
		#endregion
	}
}
