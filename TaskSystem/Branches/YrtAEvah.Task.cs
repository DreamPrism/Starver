using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;



namespace Starvers.TaskSystem.Branches
{
	using Events;
	using Vector = TOFOUT.Terraria.Server.Vector2;
	public partial class YrtAEvah
	{
		private class Task : BranchTask
		{
			private const int ShardRegionRadium = 125;

			private string[] startMsgs;
			private short msgInterval;
			private short msgCurrent;
			private short count;
			private short countRequire;
			private short targetID;
			private Vector2 startPosition;
			private Vector2 targetPosition;
			private short taskProcess;
			private short spawnedCount;

			private static Random Rand => Starver.Rand;

			public int? ID { get; }
			public override BLID BLID => BLID.YrtAEvah;

			public Task(int? id, StarverPlayer player = null) : base(player)
			{
				ID = id;
			}

			public void SetDefault()
			{
				msgInterval = 60 * 3 / 2;
				switch(ID)
				{
					case 0:
						{
							name = "证明自己";
							startMsgs = new[]
							{
								"你需要先向我证明你自己的实力",
								"先去干掉5只蓝色史莱姆",
								"然后再来找我"
							};
							targetID = NPCID.BlueSlime;
							countRequire = 5;
							break;
						}
					case 1:
						{
							name = "睡个好觉";
							startMsgs = new[]
							{
								"这几天晚上总是有几只爱斯基摩僵尸来打扰我睡觉",
								"你去替我好好收拾下它们",
								"[c/008800:你需要消灭10只爱斯基摩僵尸]"
							};
							targetID = NPCID.ZombieEskimo;
							countRequire = 10;
							break;
						}
					case 2:
						{
							name = "看星星";
							startMsgs = new[]
							{
								"你知道, 对于一个法师来说, ⭐是必需品",
								"⭐非常有用, 它可以用来制作魔力药水, 魔能药水, 魔力腰带...",
								"今晚的天气很晴朗, 一定会有很多落星",
								"帮我收集尽可能多星星",
								"[c/008800:至少收集45科星星]"
							};
							targetID = ItemID.FallenStar;
							countRequire = 45;
							break;
						}
					case 3:
						{
							name = "美妙碎片";
							startMsgs = new[]
							{
								"看看这个漂浮在空中的碎片",
								"很美丽, 对吧?",
								"更重要的是, 它们蕴涵着巨大的能量",
								"今晚它们还会在一个地点大量出现",
								"在其他人捷足先登之前替我把它们带回来"
							};
							targetID = ItemID.FragmentStardust;
							countRequire = 120;
							break;
						}
					case 4:
						{
							name = "追踪者";
							startMsgs = new[]
							{
								"感谢你替我收集这些碎片",
								"但它们只是个诱饵",
								"为了引我现身...",
								"不过我还是靠这些诱饵定位到了他们",
								"解决他们, 把真正的碎片带回来"
							};
							break;
						}
					case 5:
						{
							name = "寸步不离";
							startMsgs = new[]
							{
								"可恶...",
								"碎片已经被他们动过手脚了",
								"他们施加在碎片上的东西使我失去了力量",
								"但这只是暂时的",
								"他们不会给我恢复的机会, 一定会来攻击这里",
								"我需要你护送我去\"那个地方\"",
								"那里能让我在最短时间内恢复"
							};
							break;
						}
					default:
						throw new InvalidOperationException("空任务");
				}
			}

			public override void Start()
			{
				base.Start();
				switch(ID)
				{
					case 3:
						{
							targetPosition = FindPos1();
#if DEBUG
							TargetPlayer.SendDeBugMessage($"targetPos: {targetPosition}");
							TargetPlayer.SendDeBugMessage($"myPos: {TargetPlayer.Center}");
#endif
							break;
						}
				}
				startPosition = TargetPlayer.Center;
			}

			public (bool success, string msg) CanStartTask(StarverPlayer player)
			{
				switch (ID)
				{
					case 0:
					case 3:
					case 4:
					case 5:
						{
							return (true, null);
						}
					case 1:
					case 2:
						{
							if (Main.dayTime)
							{
								return (false, "你只能在晚上接受这个任务");
							}
							return (true, null);
						}
					default:
						throw new InvalidOperationException("空任务");
				}
			}

			public override void Updating(int Timer)
			{
				base.Updating(Timer);
				if (msgCurrent < startMsgs.Length && Timer % msgInterval == 0)
				{
					TargetPlayer.SendMessage(startMsgs[msgCurrent++], new Color(255, 233, 233));
				}
				switch (ID)
				{
					case 1:
						{
							if (Main.dayTime)
							{
								Fail();
							}
							break;
						}
					case 2:
						{
							if (Main.dayTime)
							{
								if (count >= countRequire)
								{
									Success();
								}
								else
								{
									Fail();
								}
							}
							else
							{
								if (Timer % 30 == 0)
								{
									if (Rand.Next(100) > 80)
									{
										Vector2 where = TargetPlayer.Center;
										where.Y -= 16 * 50;
										where.X += Rand.Next(-16 * 90, 16 * 80);
										var item = new AuraSystem.Realms.AnalogItem(ItemID.FallenStar);
										item.Center = where;
										item.TimeLeft = 60 * 15;
										Starver.Instance.Aura.AddRealm(item);
									}
								}
							}
							break;
						}
					case 3:
						{
							if (taskProcess == 0)
							{
								if (msgCurrent >= startMsgs.Length && Timer % 90 == 0)
								{
									var distance = targetPosition - TargetPlayer.Center;
									var len = distance.Length();
									if (len > ShardRegionRadium * 16)
									{
										string tip = $"还有{len / 16}块方格距离";
										Utils.SendCombatMsg(TargetPlayer.Center + distance.ToLenOf(16 * 30), tip, Color.GreenYellow);
									}
									else
									{
										taskProcess++;
										TargetPlayer.SetBuff(BuffID.UFOMount, 60 * 60 * 60);
										Utils.SendCombatMsg(targetPosition - distance.ToLenOf(ShardRegionRadium * 8), "就在这里", Color.GreenYellow);
									}
								}
							}
							else if (taskProcess == 1)
							{
								TargetPlayer.SetBuff(BuffID.UFOMount);
								if (Timer % 40 == 0)
								{
									if (spawnedCount < countRequire)
									{
										float length = Rand.Next(ShardRegionRadium * 16);
										var realm = new AuraSystem.Realms.AnalogItem(ItemID.FragmentStardust);
										realm.Center = targetPosition + Rand.NextVector2(length);
										Starver.Instance.Aura.AddRealm(realm);
										/*
										int idx = Utils.NewItem(targetPosition + Rand.NextVector2(length), ItemID.FragmentStardust);
										Main.item[idx].keepTime = int.MaxValue;
										*/
										spawnedCount++;
									}
								}
							}
							else if (taskProcess == 2 && Timer % 90 == 0)
							{
								var distance = startPosition - TargetPlayer.Center;
								var len = distance.Length();
								if (len > 10 * 16)
								{
									string tip = $"还有{len / 16}块方格距离";
									Utils.SendCombatMsg(TargetPlayer.Center + distance.ToLenOf(16 * 30), tip, Color.GreenYellow);
								}
								else
								{
									Success();
								}
							}
							if (taskProcess < 2 && Timer % 150 == 0)
							{
								const int limit = ShardRegionRadium * 2 * 3 / 4;
								for (int i = 0; i < limit; i++)
								{
									double angle = Math.PI * 2 * i / limit;
									int idx = Utils.NewProj(targetPosition + Utils.FromPolar(angle, ShardRegionRadium * 16), Vector2.Zero, ProjectileID.VortexVortexPortal);
									Main.projectile[idx].timeLeft = 150;
									Main.projectile[idx].aiStyle = -1;
								}
							}
							break;
						}
				}
			}

			public override void StrikedNPC(NPCStrikeEventArgs args)
			{
				base.StrikedNPC(args);
				switch (ID)
				{
					case 0:
					case 1:
						{
							if (!args.NPC.active)
							{
								if (args.NPC.type == targetID)
								{
									count++;
									TargetPlayer.SendCombatMSsg($"猎杀数: {count} / {countRequire}", Color.Maroon);
									if (count >= countRequire)
									{
										Success();
									}
								}
							}
							break;
						}
				}
			}

			public override void OnDeath()
			{
				base.OnDeath();
				TargetPlayer.SendFailMessage("任务由于死亡而失败");
				Fail();
			}

			public override void OnPickAnalogItem(AuraSystem.Realms.AnalogItem item)
			{
				base.OnPickAnalogItem(item);
				switch (ID)
				{
					case 2:
						{
							if (item.ID == ItemID.FallenStar)
							{
								count++;
								item.Kill();
								TargetPlayer.SendCombatMSsg($"{count}颗落星已收集", Color.Green);
								if (count == countRequire)
								{
									TargetPlayer.SendMessage($"目标已达成", Color.YellowGreen);
								}
								else if (count < countRequire)
								{
									TargetPlayer.SendMessage($"还需{countRequire - count}颗", Color.Green);
								}
							}
							break;
						}
					case 3:
						{
							if (taskProcess == 1)
							{
								if (item.ID == ItemID.FragmentStardust)
								{
									count = (short)(count + item.Stack);
									item.Kill();
									TargetPlayer.SendCombatMSsg($"已收集碎片: {count} / {countRequire}", Color.LimeGreen);
									if (count >= countRequire)
									{
										TargetPlayer.RemoveBuff(BuffID.UFOMount);
										TargetPlayer.SendCombatMSsg("收集完成", Color.MintCream);
										TargetPlayer.SendInfoMessage("快把碎片带回去");
										taskProcess++;
									}
								}
							}
							break;
						}
				}
			}

			private void Fail()
			{
				switch (ID)
				{
					case 1:
						{
							TargetPlayer.SendFailMessage("天亮了, 僵尸已经逃走了");
							break;
						}
					case 2:
						{
							TargetPlayer.SendFailMessage("太少了, 太少了");
							break;
						}
				}
				TargetPlayer.BranchTaskEnd(false);
			}

			private void Success()
			{
				RewardPlayer();
				TargetPlayer.BranchTaskEnd(true);
			}

			private void RewardPlayer()
			{
				
			}

			public override string ToString()
			{
				return "YrtAEvah--" + ID ?? "??";
			}

			#region Utils
			private Vector2 FindPos1()
			{
				const int radium = ShardRegionRadium;
				Vector2 result = default;
				int X = (int)(TargetPlayer.Center.X / 16);
				int Y = (int)(TargetPlayer.Center.Y / 16);
#if DEBUG
				TargetPlayer.SendDeBugMessage($"{{X: {X}, Y: {Y}}}");
#endif
				int signX = 1;
				if (Rand.Next(2) == 1)
				{
					signX = -1;
				}
				int t = 0;
				while (radium + 200 < X && X < Main.maxTilesX - radium - 200)
				{
					X += signX * Rand.Next(100, 200);
					if (t++ > 10 && Rand.Next(100) > 80)
					{
						break;
					}
				}
#if DEBUG
				TargetPlayer.SendDeBugMessage($" t: {t}");
				TargetPlayer.SendDeBugMessage($" X: {X}");
#endif
				t = 0;
				int signY = 1;
				if (Rand.Next(2) == 1)
				{
					signY = -1;
				}
				while (radium * 2 + 20 < Y && Y < Main.maxTilesY - 2 * radium - 20)
				{
					Y += signY * Rand.Next(10, 30);
					if (t++ > 4 && Rand.Next(100) > 40)
					{
						break;
					}
				}
#if DEBUG
				TargetPlayer.SendDeBugMessage($" t: {t}");
				TargetPlayer.SendDeBugMessage($" Y: {Y}");
#endif
				#region Local Check
				static bool CheckPoint(int X, int Y)
				{
					static bool CheckTile(int X, int Y)
					{
						var tile = Main.tile[X, Y];
						System.Diagnostics.Debug.Assert(tile != null, "tile is null!");
						return !tile.active();
					}
					try
					{
						for (int i = 0; i < radium; i++)
							for (int j = 0; j < radium; j++)
							{
								if (i * i + j * j < radium * radium)
								{
									bool flag =
										CheckTile(X + j, Y + i) &&
										CheckTile(X + j, Y - i) &&
										CheckTile(X - j, Y + i) &&
										CheckTile(X - j, Y - i);
									if (!flag)
									{
										return false;
									}
								}
							}
					}
					catch (IndexOutOfRangeException)
					{
						return false;
					}
					return true;
				}
				#endregion
				for (int i = 0; i < radium * 2; i++)
				{
					for (int j = 0; j < radium * 2; j++)
					{
						if (CheckPoint(X, Y))
						{
							goto Out;
						}
						X += 1 * signX;
					}
					X -= radium * 2 * signX;
					if (X + signX * radium * 2 <= radium || Main.maxTilesX - radium <= X + signX * radium * 2)
					{
						signX *= -1;
					}
					if (Y + signY * radium * 2 <= radium * 2 || Main.maxTilesY - radium * 2 <= X + signY * radium * 2)
					{
						signY *= -1;
					}
					Y += 1 * signY;
				}
				Out:
#if DEBUG
				TargetPlayer.SendDeBugMessage($"{{X: {X}, Y: {Y}}}");
#endif
				result = new Vector2(X * 16, Y * 16);
				return result;
			}
			#endregion
		}
	}
}
