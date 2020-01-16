using System;
using Terraria.ID;



namespace Starvers.TaskSystem.Branches
{
	using Events;
    using Terraria;
    using Color = Microsoft.Xna.Framework.Color;
	public partial class YrtAEvah
	{
		private class Task : BranchTask
		{
			private string[] startMsgs;
			private Data16 data;
			private short MsgInterval
			{
				get => data.ShortValue0;
				set => data.ShortValue0 = value;
			}
			private short MsgCurrent
			{
				get => data.ShortValue1;
				set => data.ShortValue1 = value;
			}
			private short KillCount
			{
				get => data.ShortValue2;
				set => data.ShortValue2 = value;
			}
			// 上下两个不能同时使用
			private short CollectCount
			{
				get => data.ShortValue2;
				set => data.ShortValue2 = value;
			}
			private short CountRequire
			{
				get => data.ShortValue3;
				set => data.ShortValue3 = value;
			}
			private short TargetID
			{
				get => data.ShortValue4;
				set => data.ShortValue4 = value;
			}
			public int? ID { get; }
			public override BLID BLID => BLID.YrtAEvah;

			public Task(int? id, StarverPlayer player = null) : base(player)
			{
				ID = id;
			}

			public void SetDefault()
			{
				MsgInterval = 60 * 3 / 2;
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
							TargetID = NPCID.BlueSlime;
							CountRequire = 5;
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
							TargetID = NPCID.ZombieEskimo;
							CountRequire = 10;
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
								"帮我收集尽可能多星星"
							};
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

			public (bool success, string msg) CanStartTask(StarverPlayer player)
			{
				throw new NotImplementedException();
			}

			public override void Updating(int Timer)
			{
				base.Updating(Timer);
				if (MsgCurrent < startMsgs.Length && Timer % MsgInterval == 0)
				{
					TargetPlayer.SendMessage(startMsgs[MsgCurrent++], new Color(255, 233, 233));
				}
				switch (ID)
				{
					case 2:
						{
							if (Main.dayTime)
							{
								Fail();
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
								if (args.NPC.type == TargetID)
								{
									KillCount++;
									TargetPlayer.SendCombatMSsg($"猎杀数: {KillCount} / {CountRequire}", Color.Maroon);
									if (KillCount >= CountRequire)
									{
										Success();
									}
								}
							}
							break;
						}
				}
			}

			public override void OnPickItem(int idx)
			{
				base.OnPickItem(idx);
				switch (ID)
				{
					case 2:
						{
							if (Main.item[idx].type == ItemID.FallenStar)
							{
								Main.item[idx].netDefaults(0);
								CollectCount++;
								TargetPlayer.SendCombatMSsg($"{CollectCount}颗落星已收集", Color.Green);
								if (CollectCount >= CountRequire)
								{
									Success();
								}
								else
								{
									TargetPlayer.SendMessage($"还需{CountRequire - CollectCount}颗", Color.Green);
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
					case 2:
						{
							TargetPlayer.SendFailMessage("太慢了, 太慢了");
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
		}
	}
}
