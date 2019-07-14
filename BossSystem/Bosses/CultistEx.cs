using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers.BossSystem.Bosses
{
	using Base;
	using Microsoft.Xna.Framework;
	using Terraria;
	using Terraria.ID;
	using Vector = TOFOUT.Terraria.Server.Vector2;
	public class CultistEx : StarverBoss
	{
		#region Fields
		/// <summary>
		/// 计算出生多久(确保邪教有图像)
		/// </summary>
		private int SpawnCount;
		private IProjSet FireBalls = new ProjDelay();
		#endregion
		#region ctor
		public CultistEx() : base(3)
		{
			RawType = NPCID.CultistBoss;
			Name = Lang.GetNPCNameValue(NPCID.CultistBoss);
			DefaultLife = 505000;
			DefaultLifes = 150;
			DefaultDefense = 28;
		}
		#endregion
		#region Spawn
		public override void Spawn(Vector2 where, int lvl = 2000)
		{
			base.Spawn(where, lvl);
			Mode = BossMode.CultistFireBall;
			SpawnCount = 0;
			RealNPC.aiStyle = 84;
			RealNPC.dontTakeDamage = true;
		}
		#endregion
		#region RealAI
		public unsafe override void RealAI()
		{
			#region Common
			#region StartAnimation
			if (SpawnCount < 60 * 10)
			{
				SpawnCount++;
				return;
			}
			else
			{
				RealNPC.dontTakeDamage = false;
				RealNPC.aiStyle = -1;
			}
			#endregion
			#region TrackingPlyer
			if (Timer % 60 == 0)
			{
				WhereToGo = (Vector)(TargetPlayer.Center);
				WhereToGo.Y -= 16 * 15;
			}
			FakeVelocity = (Vector)(WhereToGo - Center) / 20;
			#endregion
			#endregion
			#region Mode
			switch (Mode)
			{
				#region SelectMode
				case BossMode.WaitForMode:
					SelectMode();
					break;
				#endregion
				#region FireBall
				case BossMode.CultistFireBall:
					if (StarverAI[0] > 4)
					{
						StarverAI[0] = 0;
						ResetMode();
						break;
					}
					if (Timer % 62 == 1)
					{
						PushFireBall();
					}
					if (Timer % 62 == 44)
					{
						FireBalls.Launch();
						StarverAI[0]++;
					}
					break;
				#endregion
				#region Lightning
				case BossMode.CultistLightning:
					if(StarverAI[0] > PI * 2)
					{
						StarverAI[0] = 0;
						ProjCircle(TargetPlayer.Center, 16 * 30, 0, ProjectileID.CultistBossLightningOrb, 8, 253, 0, Index);
						ResetMode();
						break;
					}
					if(Timer % 60 == 0)
					{
						Lightning();
					}
					break;
				#endregion
				#region ShadowFireBall
				case BossMode.CultistShadowFireball:
					break;
				#endregion
				#region Mist
				case BossMode.Mist:
					break;
				#endregion
				#region SummonFollows
				case BossMode.SummonFollows:
					break;
					#endregion
			}
			#endregion
		}
		#endregion
		#region AIS
		#region Lightning
		private unsafe void Lightning()
		{
			StarverAI[0] += PI / 5;
			vector = NewByPolar(StarverAI[0], 16 * 20);
			Main.projectile[Proj(TargetPlayer.Center + vector, Vector2.Zero, ProjectileID.CultistBossLightningOrb, 263)].ai[0] = Index;
		}
		#endregion
		#region FireBalls
		#region PushFireBall
		private void PushFireBall()
		{
			int[] result = ProjCircleWithReturn(Center, 1, 2, ProjectileID.CultistBossFireBall, 10, 243, 2);
			vector = (Vector)(TargetPlayer.Center - Center);
			vector.Length = 18;
			foreach(var idx in result)
			{
				FireBalls.Push(idx, vector);
			}
		}
		#endregion
		#endregion
		#region SelectMode
		private void SelectMode()
		{
			modetime = 0;
			switch(lastMode)
			{
				#region FireBall
				case BossMode.CultistFireBall:
					Mode = BossMode.CultistLightning;
					break;
				#endregion
				#region Lightning
				case BossMode.CultistLightning:
					Mode = BossMode.CultistShadowFireball;
					break;
				#endregion
				#region ShadowFireBall
				case BossMode.CultistShadowFireball:
					Mode = BossMode.Mist;
					break;
				#endregion
				#region Mist
				case BossMode.Mist:
					Mode = BossMode.SummonFollows;
					break;
				#endregion
				#region SummonFollows
				case BossMode.SummonFollows:
					Mode = BossMode.CultistFireBall;
					break;
					#endregion
			}
		}
		#endregion
		#endregion
	}
}
