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
	using System.Threading;
	using Terraria;
	using Terraria.ID;
	using Vector = TOFOUT.Terraria.Server.Vector2;
	public class EyeEx : StarverBoss
	{
		#region Fields
		private Vector _unit = (0, -16 * 24);
		private const int StartCollideDamage = 210;
		#endregion
		#region Properties
		private Vector Unit
		{
			get
			{
				return _unit + (0, 16 * 10 * Math.Sin(Timer * Math.PI / 45));
			}
		}
		#endregion
		#region Ctor
		public EyeEx() : base(3)
		{
			ComingMessage = "你感到一种令人恐惧的注视...";
			ComingMessageColor = Color.LightGreen;
			TaskNeed = 22;
			RawType = NPCID.EyeofCthulhu;
			Name = "克苏鲁之眼";
			DefaultDefense = 450;
			DefaultLife = 630000;
			DefaultLifes = 50;
			vector.X = 16 * 20;
			Drops = new DropItem[]
			{
				new DropItem(new []{ Currency.Melee }, 1, 5, 0.4f)
			};
		}
		#endregion
		#region Spawn
		public override void Spawn(Vector2 where, int lvl = CriticalLevel)
		{
			base.Spawn(where, lvl);
			Mode = BossMode.Explosive;
			RealNPC.damage = StartCollideDamage;
		}
		#endregion
		#region LifeDown
		public override void LifeDown()
		{
			base.LifeDown();
			RealNPC.damage = (int)(StartCollideDamage * DamageIndex);
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
					modetime = 0;
					break;
				#endregion
				#region Rush
				case BossMode.Rush:
					if (modetime > 60 * 5)
					{
						Mode = BossMode.WaitForMode;
						double rad = (Center - TargetPlayer.Center).Angle();
						vector = Vector.FromPolar(rad, 16 * 30f);
						Center = TargetPlayer.Center + vector;
						FakeVelocity = default;
						break;
					}
					if (Timer % 3 == 0)
					{
						if (Vector2.Distance(Center, TargetPlayer.Center) > 16 * 44)
						{
							Rush();
						}
					}
					break;
				#endregion
				#region Sharknado
				case BossMode.Sharknado:
					if(modetime > 60 * 6)
					{
						StarverAI[1] = 0;
						RushBegin();
					}
					if(Timer % 45 == 0)
					{
						Sharknode();
					}
					break;
				#endregion
				#region SummonFollows
				case BossMode.SummonFollows:
					if(StarverAI[1] > 5)
					{
						StarverAI[1] = 0;
						RushBegin();
					}
					if(Timer % 60 == 0)
					{
						SummonEyeFollows();
					}
					break;
				#endregion
				#region Trident
				case BossMode.RedDevilTrident:
					if(modetime > 60 * 8)
					{
						RushBegin();
					}
					if (Timer % 12 == 0)
					{
						EvilTrident(90);
					}
					break;
				#endregion
				#region Explosive
				case BossMode.Explosive:
					if(StarverAI[1] > 9)
					{
						StarverAI[1] = 0;
						RushBegin();
					}
					if (Timer % 60 == 0)
					{
						Explosive();
						++StarverAI[1];
					}
					break;
					#endregion
			}
			#endregion
			#region Common
			if (Mode != BossMode.Rush)
			{
				Vel = (Vector)(TargetPlayer.Center + Unit - Center) / 30;
				FakeVelocity /= 50;
				FakeVelocity += Vel;
				FakeVelocity.Length = Math.Min(24, FakeVelocity.Length);
			}
			#endregion
		}
		#endregion
		#region AIs
		#region Explosive
		private void Explosive()
		{
			vector = (Vector)TargetPlayer.Center;
			ThreadPool.QueueUserWorkItem(obj =>
			{
				try
				{
					Thread.Sleep(700);
					int idx;
					for (int i = 0; i < 6; i++)
					{
						idx = Proj(vector + FromPolar(PI / 3 * i, 16 * 20), Vector.Zero, ProjectileID.Explosives, 1000);
						Main.projectile[idx].owner = Target;
					}
				}
				catch(Exception e)
				{
					StarverPlayer.All.SendDeBugMessage(e.ToString());
				}
			});
		}
		#endregion
		#region SummonFollows
		private void SummonEyeFollows()
		{
			if (Main.npc.Count(npc => npc.active && npc.type == NPCID.WanderingEye && npc.lifeMax > 6000) >= 3 * 6)
			{
				StarverAI[1] = 6;
				return;
			}
			++StarverAI[1];
			NewNPC((Vector)Center, FromPolar(PI / 2, 9), NPCID.WanderingEye, 6235, 98);
			NewNPC((Vector)Center, FromPolar(PI / 2 + PI / 6, 9), NPCID.WanderingEye, 6235, 98);
			NewNPC((Vector)Center, FromPolar(PI / 2 - PI / 6, 9), NPCID.WanderingEye, 6235, 98);
		}
		#endregion
		#region Sharknado
		private unsafe void Sharknode()
		{
			StarverAI[1] += PI / 14;
			vector = -(Vector)RelativePos;
			vector.Length = 16;
			Vel = vector;
			Vel.Angle += StarverAI[1];
			vector.Angle -= StarverAI[1];
			Proj(Center, vector, ProjectileID.SharknadoBolt, 83);
			Proj(Center, Vel, ProjectileID.SharknadoBolt, 80);
		}
		#endregion
		#region SelectMode
		private void SelectMode()
		{
			switch(lastMode)
			{
				#region Sharknado
				case BossMode.Sharknado:
					Mode = BossMode.SummonFollows;
					break;
				#endregion
				#region SummonFollows
				case BossMode.SummonFollows:
					Mode = BossMode.RedDevilTrident;
					break;
				#endregion
				#region Trident
				case BossMode.RedDevilTrident:
					Mode = BossMode.Explosive;
					break;
				#endregion
				#region Explosive
				default://case BossMode.Explosive:
					Mode = BossMode.Sharknado;
					break;
					#endregion
			}
		}
		#endregion
		#endregion
	}
}
