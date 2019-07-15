using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers.BossSystem.Bosses
{
	using Base;
	using Microsoft.Xna.Framework;
	using Starvers.WeaponSystem;
	using Terraria.ID;
	using Vector = TOFOUT.Terraria.Server.Vector2;
	public class RedDevilEx : StarverBoss
	{
		#region Fields
		/// <summary>
		/// (0, -16 * 24)
		/// </summary>
		private Vector UnitY = new Vector(0, -16 * 24);
		#endregion
		#region ctor
		public RedDevilEx():base(1)
		{
			TaskNeed = 32;
			Name = "红魔王";
			RawType = NPCID.RedDevil;
			DefaultDefense = 18;
			DefaultLife = 30000;
			DefaultLifes = 70;
			DamagedIndex = 0.02f;
			LifeperPlayerType = ByLifes;
			Drops = new DropItem[]
			{
				new DropItem(new int[] { Currency.Minion}, 2, 6,  0.4f),
				new DropItem(new int[] { Currency.Ranged}, 4, 9,  0.4f),
				new DropItem(new int[] { Currency.Melee}, 7, 10,  0.4f),
			};
		}
		#endregion
		#region Spawn
		public override void Spawn(Vector2 where, int lvl = 2000)
		{
			base.Spawn(where, lvl);
			lastMode = BossMode.SummonFollows;
		}
		#endregion
		#region RealAI
		public override void RealAI()
		{
			#region Modes
			switch (Mode)
			{
				#region SelectMode
				case BossMode.WaitForMode:
					SelectMode();
					break;
				#endregion
				#region Flame
				case BossMode.RedDevilFlame:
					if(modetime > 60 * 6)
					{
						ResetMode();
						break;
					}
					if(Timer % 5 == 0)
					{
						Flame();
					}
					break;
				#endregion
				#region Laser
				case BossMode.RedDevilLaser:
					if(modetime > 60 * 8)
					{
						ResetMode();
						break;
					}
					if(Timer % 2 == 0)
					{
						Laser();
					}
					break;
				#endregion
				#region Trident
				case BossMode.RedDevilTrident:
					if(modetime > 60 * 9)
					{
						ResetMode();
						break;
					}
					if(Timer % 5 == 0)
					{
						Trident();
					}
					break;
				#endregion
				#region SummonFollows
				case BossMode.SummonFollows:
					if(modetime > 60 * 6)
					{
						ResetMode();
						break;
					}
					if(Timer % 58 == 0)
					{
						SummonFollows();
					}
					break;
					#endregion
			}
			#endregion
			#region Common
			FakeVelocity = (Vector)(TargetPlayer.Center + UnitY - Center) / 14;
			#endregion
		}
		#endregion
		#region AIs
		#region SummonFollows
		private new void SummonFollows()
		{
			vector = new Vector(0,17);
			NewNPC((Vector)Center, vector, NPCID.Demon, 30000, 34500);
			vector.Angle += PI / 11;
			NewNPC((Vector)Center, vector, NPCID.Demon, 30000, 34500);
			vector.Angle -= 2 * PI / 11;
			NewNPC((Vector)Center, vector, NPCID.Demon, 30000, 34500);
		}
		#endregion
		#region Trident
		private void Trident()
		{
			vector = (Vector)Rand.NextVector2(16 * 30);
			Vel = -vector;
			Vel.Length = 28;
			Proj(TargetPlayer.Center + vector, Vel, ProjectileID.UnholyTridentHostile, 155);
		}
		#endregion
		#region Laser
		private void Laser()
		{
			vector = new Vector(0, 16 * 28);
			Vel = NewByPolar(PI / 2, 23);
			Vel.Angle += (Rand.NextAngle() - PI) / 6;
			Proj(TargetPlayer.Center + vector, Vel.Deflect(PI), ProjectileID.EyeLaser, 149);
			Proj(TargetPlayer.Center - vector, Vel, ProjectileID.EyeLaser, 149);
		}
		#endregion
		#region Flame
		private void Flame()
		{
			vector = (Vector)Rand.NextVector2(16 * 19);
			Vel = -vector;
			Vel.Length = 10;
			Proj(TargetPlayer.Center + vector, Vel, ProjectileID.DesertDjinnCurse, 144);
		}
		#endregion
		#region SelectMode
		private void SelectMode()
		{
			modetime = 0;
			switch(lastMode)
			{
				#region Flame
				case BossMode.RedDevilFlame:
					Mode = BossMode.RedDevilLaser;
					break;
				#endregion
				#region Laser
				case BossMode.RedDevilLaser:
					Mode = BossMode.RedDevilTrident;
					break;
				#endregion
				#region Trident
				case BossMode.RedDevilTrident:
					Mode = BossMode.SummonFollows;
					break;
				#endregion
				#region SummonFollows
				case BossMode.SummonFollows:
					Mode = BossMode.RedDevilFlame;
					break;
					#endregion
			}
		}
		#endregion
		#endregion
	}
}
