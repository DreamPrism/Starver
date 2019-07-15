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
	public class StarverRedeemer : StarverBoss
	{
		#region Fields
		protected List<Tuple<Vector2, int, int>> tupleList = new List<Tuple<Vector2, int, int>>();
		protected List<Vector2> vector2List = new List<Vector2>();
		protected List<int> SummonList = new List<int>();
		protected int[] param1 = new int[2] { -1, 1 };
		protected short inter;
		#endregion
		#region ctor
		public StarverRedeemer():base(4)
		{
			TaskNeed = 28;
			Name = "The Starver Redeemer";
			FullName = "Shtaed The Starver Redeemer";
			DefaultLife = 3650000;
			DefaultLifes = 150;
			DefaultDefense = 40;
			vector.X = 16 * 39;
			vector.Y = 0;
			RawType = NPCID.DukeFishron;
		}
		#endregion
		#region Spawn
		public override void Spawn(Vector2 where, int lvl = 2000)
		{
			base.Spawn(where, lvl);
			inter = ExVersion ? (short)20 : (short)30;
			Mode = BossMode.DeathsTwinkle;
			RealNPC.type = NPCID.LunarTowerStardust;
			vector.Y = 0;
			vector.X = ExVersion ? 16 * 10 : 16 * 16;
		}
		protected void Spawn(Vector2 where, int lvl, double AngleStart, float radium = -1)
		{
			Spawn(where, lvl);
			if (radium > 0)
			{
				vector.Y = 0;
				vector.X = radium;
			}
			vector.Angle = AngleStart;
		}
		#endregion
		#region RealAI
		public unsafe override void RealAI()
		{
			switch (Mode)
			{
				#region SelectMode
				case BossMode.WaitForMode:
					switch (lastMode)
					{
						#region Twinkle
						case BossMode.DeathsTwinkle:
							Mode = BossMode.DeathsSummonFollows;
							break;
						#endregion
						#region SummonFollows
						case BossMode.DeathsSummonFollows:
							Mode = BossMode.DeathsFlowInvaderShot;
							inter = ExVersion ? (short)30 : (short)60;
							break;
						#endregion
						#region FlowInvaderShot
						case BossMode.DeathsFlowInvaderShot:
							Mode = BossMode.DeathsLaser;
							inter = ExVersion ? (short)1 : (short)3;
							break;
						#endregion
						#region Laser
						case BossMode.DeathsLaser:
							Mode = BossMode.DeathsTwinkle;
							inter = ExVersion ? (short)20 : (short)30;
							break;
							#endregion
					}
					modetime = 0u;
					LastCenter = Center;
					break;
				#endregion
				#region SummonFollows
				case BossMode.DeathsSummonFollows:
					if(modetime > 60 * 9)
					{
						ResetMode();
						break;
					}
					if(Timer % 25 == 0)
					{
						AddFollows();
					}
					break;
				#endregion
				#region Twinkle
				case BossMode.DeathsTwinkle:
					if (modetime > 60 * 9)
					{
						ResetMode();
						break;
					}
					Twinkle();
					break;
				#endregion
				#region FlowInvaderShot
				case BossMode.DeathsFlowInvaderShot:
					if (modetime > 60 * 16)
					{
						ResetMode();
						break;
					}
					FlowInvaderShot();
					break;
				#endregion
				#region Laser
				case BossMode.DeathsLaser:
					if (modetime > 60 * 9)
					{
						ResetMode();
						break;
					}
					Laser();
					break;
					#endregion
			}
			SummonFollows();
			vector.Angle += PI / 120;
			Center = TargetPlayer.Center +  vector;
		}
		#endregion
		#region ModeAIs
		#region AddFollows
		protected void AddFollows()
		{
			int add;
			switch (Rand.Next(5))
			{
				case 0:
					add = NPCID.StardustCellBig;
					break;
				case 1:
					add = NPCID.StardustJellyfishBig;
					break;
				case 2:
					add = NPCID.StardustWormHead;
					break;
				case 3:
					add = NPCID.StardustSoldier;
					break;
				case 4:
					add = NPCID.StardustSpiderBig;
					break;
				default:
					add = NPCID.StardustCellSmall;
					break;
			}
			SummonList.Add(add);
		}
		#endregion
		#region SummonFollows
		protected new unsafe void SummonFollows()
		{
			if (Timer % 45 == 0 && SummonList.Count > 0)
			{
				fixed (float* ai = this.ai)
				{
					int num1 = Utils.SelectRandom(Main.rand, SummonList.ToArray());
					ai[1] = 30 * Main.rand.Next(5, 16);
					int num2 = Main.rand.Next(3, 6);
					int num3 = Main.rand.Next(0, 4);
					int index1 = 0;
					tupleList.Add(item: Tuple.Create(RealNPC.Top - (Vector2.UnitY * 120f), num2, 0));
					int num4 = 0;
					while (tupleList.Count > 0)
					{
						Vector2  vector2_1 = tupleList[0].Item1;
						int num6 = 1;
						int num7 = 1;
						if (num4 > 0 && num3 > 0 && (Main.rand.Next(3) != 0 || num4 == 1))
						{
							num7 = Main.rand.Next(Math.Max(1, tupleList[0].Item2));
							++num6;
							--num3;
						}
						for (int index2 = 0; index2 < num6; ++index2)
						{
							int num8 = tupleList[0].Item3;
							if (num4 == 0)
								num8 =Utils.SelectRandom(Main.rand, param1);
							else if (index2 == 1)
								num8 *= -1;
							float num9 = (float)((num4 % 2 == 0 ? 0.0 : 3.14159274101257) + (0.5 - Main.rand.NextFloat()) * 0.785398185253143 + num8 * 0.785398185253143 * (num4 % 2 == 0).ToDirectionInt());
							float num10 = (float)(100.0 + 50.0 * Main.rand.NextFloat());
							int num11 = tupleList[0].Item2;
							if (index2 != 0)
								num11 = num7;
							if (num4 == 0)
							{
								num9 = (float)((0.5 - Main.rand.NextFloat()) * 0.785398185253143);
								num10 = (float)(100.0 + 100.0 * Main.rand.NextFloat());
							}
							Vector2  vector2_2 = (-Vector2.UnitY).RotatedBy(num9, new Vector2()) * num10;
							if (num11 - 1 < 0)
								vector2_2 = Vector2.Zero;
							index1 = Proj(vector2_1,  vector2_2, ProjectileID.StardustTowerMark, 180, 0.0f, -num4 * 10f, (float)(0.5 + Main.rand.NextFloat() * 0.5));
							if (ExVersion)
							{
								for (int i = 0; i < 35; i++)
								{
									if (Starver.Players[i] is null)
									{
										continue;
									}
									if (i == Target || Main.player[i] == null || Starver.Players[i].Active)
									{
										continue;
									}
									index1 = Proj(Starver.Players[i].Center,  vector2_2, ProjectileID.StardustTowerMark, 180, 0.0f, -num4 * 10f, (float)(0.5 + Main.rand.NextFloat() * 0.5));
								}
							}
							else
							{
								index1 = Proj(vector2_1,  vector2_2, ProjectileID.StardustTowerMark, 180, 0.0f, -num4 * 10f, (float)(0.5 + Main.rand.NextFloat() * 0.5));
							}
							vector2List.Add(vector2_1 +  vector2_2);
							if (num4 < num2 && tupleList[0].Item2 > 0)
							{
								tupleList.Add(Tuple.Create(vector2_1 +  vector2_2, num11 - 1, num8));
							}
						}
						tupleList.Remove(tupleList[0]);
					}
					Main.projectile[index1].localAI[0] = num1;
					SummonList.Clear();
				}
			}

		}
		#endregion
		#region Twinkle
		protected unsafe void Twinkle()
		{
			if (Timer % inter == 0)
			{
				int follow;
				if (ExVersion)
				{
					foreach (var player in Starver.Players)
					{
						if(player is null)
						{
							continue;
						}
						Vel = (Vector)(player.Center +  vector);
						follow = NPC.NewNPC((int)Vel.X, (int)Vel.Y, NPCID.StardustSpiderSmall);
						NetMessage.SendData((int)PacketTypes.NpcUpdate, -1, -1, null, follow);
						Vel = (Vector)(player.Center +  vector);
						follow = NPC.NewNPC((int)Vel.X, (int)Vel.Y, NPCID.StardustSpiderSmall);
						NetMessage.SendData((int)PacketTypes.NpcUpdate, -1, -1, null, follow);
					}
				}
				else
				{
					Vel = (Vector)(TargetPlayer.Center +  vector);
					follow = NPC.NewNPC((int)Vel.X, (int)Vel.Y, NPCID.StardustSpiderSmall);
					NetMessage.SendData((int)PacketTypes.NpcUpdate, -1, -1, null, follow);
					follow = NPC.NewNPC((int)Vel.X, (int)Vel.Y, NPCID.StardustSpiderSmall);
					NetMessage.SendData((int)PacketTypes.NpcUpdate, -1, -1, null, follow);
				}
			}
		}
		#endregion
		#region FlowInvaderShot
		protected unsafe void FlowInvaderShot()
		{
			if (Timer % inter == 0)
			{
				if (ExVersion)
				{
					foreach (var player in Starver.Players)
					{
						if (player is null)
						{
							continue;
						}
						ProjSector(player.Center + RelativePos, 17, 3, RelativePos.Angle() + PI, PI * 2 / 3, 240, ProjectileID.StardustJellyfishSmall, 5);
					}
				}
				else
				{
					ProjSector(Center, 17, 3,  vector.Angle+ PI, PI * 2 / 3, 180, ProjectileID.StardustJellyfishSmall, 5);
				}
			}
		}
		#endregion
		#region Laser
		protected unsafe void Laser()
		{
			if (Timer % inter == 0)
			{
				if (ExVersion)
				{
					foreach (var player in Starver.Players)
					{
						if (player == null || !player.Active)
						{
							continue;
						}
						Vel = (Vector)Rand.NextVector2(16 * 29f);
						Proj(player.Center + Vel, -Vel / 20 + Rand.NextVector2(5f, 5f), ProjectileID.DeathLaser, 220, 9f);
					}
				}
				else
				{
					Vel = (Vector)Rand.NextVector2(16 * 29f);
					Proj(TargetPlayer.Center + Vel, -Vel / 25 + Rand.NextVector2(5f, 5f), ProjectileID.DeathLaser, 170, 9f);
				}
			}
		}
		#endregion
		#endregion
	}
}
