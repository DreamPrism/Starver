using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace Starvers.TaskSystem
{
	using BossSystem;
	using CheckDelegate = Func<StarverPlayer, bool>;
	using StarverBoss = BossSystem.Bosses.Base.StarverBoss;
	using ItemLists = Dictionary<TaskDifficulty, TaskItem[]>;
	public class StarverTask
	{
		#region Properties
		public static StarverBoss[] Bosses => StarverBossManager.Bosses;
		public StarverBoss Boss { get; protected set; }
		public int ID { get; protected set; }
		public TaskDifficulty Difficulty { get; protected set; }
		public string Description { get; protected set; }
		#region Need
		protected ItemLists NeedEx;
		public TaskItem[] Needs { get; protected set; }
		#endregion
		#region Reward
		protected ItemLists RewardEx;
		public TaskItem[] Rewards { get; protected set; }
		#endregion
		public string Name { get; protected set; }
		public string Story { get; protected set; }
		public int Mark { get; protected set; } = ItemID.MagicMirror;
		public int Level { get; protected set; }
		public int Exp { get; protected set; }
		public bool Normal { get; protected set; } = true;
		public bool CheckLevel { get; protected set; }
		public bool CheckItem { get; protected set; } = true;
		#endregion
		#region Statics
		public readonly static TaskItem[] DefaultReward = new TaskItem[] { (ItemID.PlatinumCoin) };
		public readonly static TaskItem[] DefaultNeed = new TaskItem[] { (ItemID.Gel) };
		public const int MAINLINE = 43;
		#endregion
		#region cctor
		static StarverTask()
		{
			if (WorldGen.oreTier1 == -1)
			{
				WorldGen.oreTier1 = 107;
				if (Starver.Rand.Next(2) == 0)
				{
					WorldGen.oreTier1 = 221;
				}
			}
			if (WorldGen.oreTier2 == -1)
			{
				WorldGen.oreTier2 = 108;
				if (Starver.Rand.Next(2) == 0)
				{
					WorldGen.oreTier2 = 222;
				}
			}
			if (WorldGen.oreTier3 == -1)
			{
				WorldGen.oreTier3 = 111;
				if (Starver.Rand.Next(2) == 0)
				{
					WorldGen.oreTier3 = 223;
				}
			}
		}
		#endregion
		#region ctor
		protected StarverTask() { }
		public StarverTask(int id, TaskDifficulty difficulty = TaskDifficulty.Hard)
		{
			ID = id;
			Level = 10 * ID;
			Difficulty = difficulty;
			switch (ID)
			{
				#region KingSlime
				case 1:
					Name = "粘黏凝胶";
					Story = "美味又易燃";
					NeedEx = new ItemLists
					{
						{ TaskDifficulty.Easy, new TaskItem[]{ (ItemID.Gel, 20) } },
						{ TaskDifficulty.Hard, new TaskItem[]
							{
														(ItemID.Gel, 99)
									}}
					};
					Rewards = new TaskItem[]
					{
												(ItemID.SlimeCrown ,5)
					};
					break;
				#endregion
				#region Eye
				case 2:
					TaskID.Eye = ID;
					Name = "凝视";
					Story = "希望站在我们和克苏鲁之眼间的不是弱小的你";
					NeedEx = new ItemLists
					{
						{ TaskDifficulty.Easy, new TaskItem[]{ (ItemID.Lens, 10) } },
						{ TaskDifficulty.Hard, new TaskItem[]
							{
								 (ItemID.Lens, 36),
								 (ItemID.DemonEyeBanner, 2)
									}}
					};
					Rewards = new TaskItem[]
					{
														(ItemID.SuspiciousLookingEye,5)
					};
					break;
				#endregion
				#region Evil
				case 3:
					int material;
					int mushroom;
					Rewards = new TaskItem[1];
					if (WorldGen.crimson)
					{
						Name = "猩红之手";
						Story = "血肉与脊骨";
						material = ItemID.Vertebrae;
						mushroom = ItemID.ViciousMushroom;
						Rewards[0] = (ItemID.BloodySpine, 5);
					}
					else
					{
						Name = "幽暗裂缝";
						Story = "邪恶意识的结合体";
						material = ItemID.RottenChunk;
						mushroom = ItemID.VileMushroom;
						Rewards[0] = (ItemID.WormFood, 5);
					}
					NeedEx = new ItemLists
					{
						{ TaskDifficulty.Easy, new TaskItem[] { (material, 10), (mushroom, 10) } },
						{ TaskDifficulty.Hard, new TaskItem[]{
												(material,28),
												(mushroom,15)
						}}
					};
					break;
				#endregion
				#region DD2 T1
				case 4:
					Name = "暗黑法师";
					Story = "古老的军团";
					NeedEx = new ItemLists
					{
						{ TaskDifficulty.Easy, new TaskItem[] { (ItemID.FallenStar, 12)} },
						{ TaskDifficulty.Hard, new TaskItem[]{
												(ItemID.FallenStar, 40),
												(ItemID.MolotovCocktail,99)
						}}
					};
					Rewards = new TaskItem[]
					{
												(ItemID.DefendersForge,2)
					};
					break;
				#endregion
				#region Bee
				case 5:
					Name = "蜂巢主妇";
					Story = "我的蜂蜜在哪?";
					NeedEx = new ItemLists
					{
						{ TaskDifficulty.Easy, new TaskItem[]
												{ 
																			(ItemID.Stinger, 9), 
																			(ItemID.Hive, 30) ,
																			(ItemID.BottledHoney, 10)
														}},
						{ TaskDifficulty.Hard, new TaskItem[]{
												(ItemID.Stinger, 32),
												(ItemID.Vine, 16),
												(ItemID.Hive,99),
												(ItemID.BottledHoney,30)
						}}
					};
					Rewards = new TaskItem[]
					{
												(ItemID.Abeemination,5)
					};
					break;
				#endregion
				#region Skeletron
				case 6:
					Name = "深黑地牢";
					Story = "打破诅咒";
					NeedEx = new ItemLists
{
						{ TaskDifficulty.Easy, new TaskItem[] 
												{ 
																			(ItemID.ZombieArm),
																			(ItemID.ZombieArm)
														}},
						{ TaskDifficulty.Hard, new TaskItem[]{
#if false
													(ItemID.Bone, 200),
												(ItemID.Skull),
												(ItemID.BoneSword),
												(ItemID.BoneKey),
#endif
													(ItemID.BoneKey),
						}}
};
					Rewards = new TaskItem[]
					{
												(ItemID.BoneKey),
												(ItemID.ClothierVoodooDoll)
					};
					break;
				#endregion
				#region Wall
				case 7:
					Name = "仍然饥饿";
					Story = "地下世界的主人与核心";
					NeedEx = new ItemLists
					{	{ TaskDifficulty.Easy, new TaskItem[] { (ItemID.HellstoneBar, 70) } },
						{ TaskDifficulty.Hard, new TaskItem[] 
												{ 
																			(ItemID.HellstoneBrick, 100),
																			(ItemID.LavaSlimeBanner, 4),
																			(ItemID.Fireblossom, 20)
														}}
					};
					Rewards = new TaskItem[]
					{
												(ItemID.GuideVoodooDoll),
												(ItemID.GuideVoodooDoll),
												(ItemID.GuideVoodooDoll),
												(ItemID.TrueNightsEdge,1,PrefixID.Legendary)
					};
					break;
				#endregion
				#region Light
				case 8:
					Name = "光与暗(其一)";
					Story = "虚华美好的假象";
					NeedEx = new ItemLists
					{
						{ TaskDifficulty.Easy, new TaskItem[]
												{ 
																			(ItemID.SoulofLight, 20),
																			(ItemID.CrystalShard, 20),
																			(ItemID.LightShard, 3)
														}},
						{ TaskDifficulty.Hard, new TaskItem[]{
												(ItemID.SoulofLight, 170),
												(ItemID.CrystalShard, 60),
												(ItemID.LightShard, 8)
								}}
					};
					Rewards = new TaskItem[]
					{
												(ItemID.LightKey,15)
					};
					break;
				#endregion
				#region Night
				case 9:
					Name = "光与暗(其二)";
					if (WorldGen.crimson)
					{
						Story = "血腥弥漫";
						NeedEx = new ItemLists
						{
							{ TaskDifficulty.Easy, new TaskItem[]
													{
																				(ItemID.SoulofNight, 20),
																				(ItemID.Ichor, 20),
																				(ItemID.DarkShard, 2) 
															}},
							{ TaskDifficulty.Hard, new TaskItem[]
													{
																				(ItemID.SoulofNight, 170),
																				(ItemID.Ichor, 60),
																				(ItemID.DarkShard, 8)
															}}
						};
					}
					else
					{
						Story = "腐臭四散";
						NeedEx = new ItemLists
						{
							{ TaskDifficulty.Easy, new TaskItem[]
													{
																				(ItemID.SoulofNight, 20),
																				(ItemID.CursedFlame, 20),
																				(ItemID.DarkShard, 2)
															}},
							{ TaskDifficulty.Hard, new TaskItem[]
													{
																				(ItemID.SoulofNight, 170),
																				(ItemID.CursedFlame, 60),
																				(ItemID.DarkShard, 8)
															}}
						};
					}
					Rewards = new TaskItem[]
					{
												(ItemID.NightKey,15)
					};
					break;
				#endregion
				#region Twins
				case 10:
					Name = "全知之眼";
					Story = "这将会是一个可怕的夜晚...";
					NeedEx = new ItemLists
					{
						{ TaskDifficulty.Easy, new TaskItem[]
												{
													(ItemID.Lens, 30),
													(ItemID.DemonEyeBanner, 2),
													(ItemID.PalladiumOre, 222)

												} },
						{ TaskDifficulty.Hard, new TaskItem[]{
												(ItemID.Lens,60),
												(ItemID.DemonEyeBanner,6),
												(ItemID.PalladiumOre,444)
						}}
};
					Rewards = new TaskItem[]
					{
						(ItemID.MechanicalEye,5)
					};
					break;
				#endregion
				#region Prime
				case 11:
					Name = "恐惧之主";
					Story = "周围的空气越来越冷...";
					NeedEx = new ItemLists
					{
						{ TaskDifficulty.Easy, new TaskItem[]
												{
													(ItemID.Bone,150),
													(ItemID.AngryBonesBanner,4),
													(ItemID.CursedSkullBanner,1),
													(ItemID.OrichalcumOre,222 * 2 / 3)
												} },
						{ TaskDifficulty.Hard, new TaskItem[]{
												(ItemID.Bone,280),
												(ItemID.AngryBonesBanner,6),
												(ItemID.CursedSkullBanner,2),
												(ItemID.OrichalcumOre,444 * 2 / 3),
						} }
					};
					Rewards = new TaskItem[]
					{
						(ItemID.MechanicalSkull,5)
					};
					break;
				#endregion
				#region Destroyer
				case 12:
					Name = "破坏之王";
					Story = "你感到来自地下深处的震动...";
					if (WorldGen.crimson)
					{
						NeedEx = new ItemLists
					{
						{ TaskDifficulty.Hard, new TaskItem[]{
													(ItemID.Vertebrae, 80),
													(ItemID.HerplingBanner, 8),
													(ItemID.TitaniumOre, 333)
							}}
						};
					}
					else
					{
						NeedEx = new ItemLists
					{
						{ TaskDifficulty.Hard, new TaskItem[]{
													(ItemID.RottenChunk, 80),
													(ItemID.CorruptorBanner, 8),
													(ItemID.TitaniumOre, 222)
							}}
};
					}
					Rewards = new TaskItem[]
					{
												(ItemID.MechanicalWorm,5)
					};
					break;
				#endregion
				#region DD2 T2
				case 13:
					Name = "食人巨魔";
					Story = "异世界的访客";
					NeedEx = new ItemLists
					{
						{ TaskDifficulty.Hard, new TaskItem[]{
												(ItemID.SoulofMight, 270),
												(ItemID.SoulofSight, 270),
												(ItemID.SoulofFright, 270)
						}}
};
					Rewards = new TaskItem[]
					{
												(ItemID.DD2ElderCrystal,20)
					};
					break;
				#endregion
				#region Plantera
				case 14:
					Name = "大南方植物";
					Story = "世纪之花灯泡";
					NeedEx = new ItemLists
					{
						{ TaskDifficulty.Hard, new TaskItem[]{
												(ItemID.ChlorophyteOre,999),
												(ItemID.TortoiseBanner,3),
												(ItemID.AngryTrapperBanner,3),
						}}
};
					Rewards = new TaskItem[]
					{
												(ItemID.TheAxe),
												(ItemID.TheAxe),
												(ItemID.TheAxe)
					};
					break;
				#endregion
				#region Golem
				case 15:
					Name = "蜥蜴信仰";
					Story = "太阳图腾";
					NeedEx = new ItemLists
					{
						{ TaskDifficulty.Hard, new TaskItem[]{
												(ItemID.TempleKey,20),
												(ItemID.LihzahrdBanner,10),
												(ItemID.FlyingSnakeBanner,10),
												(ItemID.LunarTabletFragment,99)
						}}
};
					Rewards = new TaskItem[]
					{
												(ItemID.LihzahrdPowerCell,10)
					};
					break;
				#endregion
				#region DD2 T3
				case 16:
					Name = "Betsy";
					Story = "灾难的化身";
					NeedEx = new ItemLists
					{
						{ TaskDifficulty.Hard, new TaskItem[]{
												(ItemID.MartianConduitPlating,999),
												(ItemID.BeetleHusk,99)
						}}
};
					break;
				#endregion
				#region Cultist
				case 17:
					Name = "狂热教徒";
					Story = "献祭与祈祷";
					NeedEx = new ItemLists
					{
						{ TaskDifficulty.Hard, new TaskItem[]{
												(ItemID.GoldenKey),
												(ItemID.ShadowKey),
												(WorldGen.crimson?ItemID.CrimsonKey:ItemID.CorruptionKey),
												(ItemID.FrozenKey),
												(ItemID.JungleKey),
												(ItemID.HallowedKey),
												(ItemID.CosmicCarKey),
												(ItemID.NightKey),
												(ItemID.LightKey)
						}}
};
					Rewards = new TaskItem[]
					{
												(ItemID.CultistBossBag),
												(ItemID.CultistBossBag),
												(ItemID.CultistBossBag),
												(ItemID.CultistBossBag),
												(ItemID.CultistBossBag),
												(ItemID.CultistBossBag),
												(ItemID.CultistBossBag),
												(ItemID.CultistBossBag)
					};
					break;
				#endregion
				#region Solar
				case 18:
					Name = "宇宙之怒";
					Story = "你的头脑变得麻木...";
					NeedEx = new ItemLists
					{
						{ TaskDifficulty.Hard, new TaskItem[]{
												(ItemID.AncientCultistTrophy),
												(ItemID.BossTrophyBetsy),
												(ItemID.BossTrophyDarkmage),
												(ItemID.BossTrophyOgre),
												(WorldGen.crimson?ItemID.BrainofCthulhuTrophy:ItemID.EaterofWorldsTrophy),
												(ItemID.EverscreamTrophy),
												(ItemID.EyeofCthulhuTrophy),
												(ItemID.GolemTrophy),
												(ItemID.IceQueenTrophy),
												(ItemID.KingSlimeTrophy),
												(ItemID.MartianSaucerTrophy),
												(ItemID.MourningWoodTrophy),
												(ItemID.PlanteraTrophy),
												(ItemID.PumpkingTrophy),
												(ItemID.QueenBeeTrophy),
												(ItemID.RetinazerTrophy),
												(ItemID.SantaNK1Trophy),
												(ItemID.SkeletronPrimeTrophy),
												(ItemID.SpazmatismTrophy),
												(ItemID.WallofFleshTrophy),
												(ItemID.DestroyerTrophy),
												(ItemID.DukeFishronTrophy)
						}}
};
					Rewards = new TaskItem[]
					{
												(ItemID.PlatinumCoin,99)
					};
					break;
				#endregion
				#region Nebula
				case 19:
					Name = "银河之力";
					Story = "你痛苦不堪...";
					NeedEx = new ItemLists
					{
						{ TaskDifficulty.Hard, new TaskItem[]{
												(ItemID.SolarCoriteBanner,10),
												(ItemID.SolarCrawltipedeBanner,1),
												(ItemID.SolarDrakomireBanner,5),
												(ItemID.SolarSolenianBanner,5),
												(ItemID.SolarSrollerBanner,5)
						}}
};
					Rewards = new TaskItem[]
					{
												(ItemID.SolarMonolith,99),
												(ItemID.NebulaMonolith,99)
					};
					break;
				#endregion
				#region Stardust
				case 20:
					Name = "星尘粒子";
					Story = "超自然的声音环绕在你的周围...";
					NeedEx = new ItemLists
					{
						{ TaskDifficulty.Hard, new TaskItem[]{
												(ItemID.StardustJellyfishBanner,5),
												(ItemID.StardustLargeCellBanner,5),
												(ItemID.StardustWormBanner,5),
												(ItemID.StardustSoldierBanner,5)
						}}
};
					Rewards = new TaskItem[]
					{
												(ItemID.StardustMonolith,99)
					};
					break;
				#endregion
				#region Vortex
				case 21:
					TaskID.MoonLord = ID;
					Name = "漩涡能量";
					Story = "月亮末日慢慢靠近...";
					NeedEx = new ItemLists
					{
						{ TaskDifficulty.Hard, new TaskItem[]{
												(ItemID.VortexSoldierBanner,5),
												(ItemID.VortexRiflemanBanner,5),
												(ItemID.VortexHornetQueenBanner,20)
						}}
};
					Rewards = new TaskItem[]
					{
												(ItemID.VortexMonolith,99)
					};
					break;
				#endregion
				#region EyeEx
				case 22:
					Name = "恐惧之眼-The Eye of Phobia";
					Story = "恐惧的本质是什么？未知的事物？死亡的接近？力量总是与诅咒并存，张裂的瞳膜里只剩下恐惧与悲伤";
					NeedEx = new ItemLists
					{
						{ TaskDifficulty.Hard, new TaskItem[]{
												(ItemID.Lens, 50),
												(ItemID.BlackLens, 4),
												(ItemID.SoulofFright, 200),
												(1584)
						}}
};
					Rewards = new TaskItem[]
					{
												(3467, 80),
												(3453)
					};
					break;
				#endregion
				#region BrainEX
				case 23:
					Boss = Bosses[0];
					Name = "混乱思维-Cerebrain";
					Story = "从猩红中被剥离出来，膨胀的血管与不断脱落的肉块使这个生物痛苦不堪。精神的力量会扭曲肉体，最终造就可怖的事实";
					NeedEx = new ItemLists
					{
						{ TaskDifficulty.Hard, new TaskItem[]{
												(ItemID.Ichor, 99),
												(ItemID.CrimsonFishingCrate, 20),
												(ItemID.SoulofNight, 200),
												(ItemID.SoulofLight, 200),
												(ItemID.Vertebrae, 99)
						}}
};
					Rewards = new TaskItem[]
					{
												(3467, 80),
												(3453)
					};
					break;
				#endregion
				#region BeeEx
				case 24:
					Boss = Bosses[1];
					Name = "蜂巢意志-Hive Mind";
					Story = "愚钝的成员构成了群体的智慧。蜂群的聚合体。单一个体无法完成的壮举，依靠群体便可以达成";
					NeedEx = new ItemLists
					{
						{ TaskDifficulty.Hard, new TaskItem[]{
												(ItemID.LifeFruit, 80),
												(ItemID.SoulofFlight, 200),
												(ItemID.Honeyfin, 30),
												(ItemID.Hive, 99)
						}}
};
					Rewards = new TaskItem[]
					{
												(ItemID.RodofDiscord),
												(ItemID.RodofDiscord),
												(ItemID.RodofDiscord),
												(ItemID.RodofDiscord)
					};
					break;
				#endregion
				#region SkeletonEx
				case 25:
					TaskID.SkeletronEx = ID;
					Boss = Bosses[2];
					Name = "失落骨架-Hyperosteogeny";
					Story = "脱胎于某个高等存在的残骸，仍然残存有些许威力。力量不会随着肉体的消亡而蒸发，而是如同跗骨之蛆一般留存";
					NeedEx = new ItemLists
					{
						{ TaskDifficulty.Hard, new TaskItem[]{
												(154, 888),
												(3085, 15),
												(1612)
						}}
};
					Rewards = new TaskItem[]
					{
												(3467, 80)
					};
					break;
				#endregion
				#region DarkMage
				case 26:
					Boss = Bosses[3];
					Name = "流放巫师-The Banished Enchater";
					Story = "远古封印的守卫者，被某种能量所侵蚀。他现在所能做的，仅仅只是维持其中的三道封印";
					NeedEx = new ItemLists
					{
						{ TaskDifficulty.Hard, new TaskItem[]{
												(3817, 500),
												(109, 50),
												(ItemID.DD2EnergyCrystal, 500),
												(3580)
						}}
};
					Rewards = new TaskItem[]
					{
												(3467, 120),
												(3580)
					};
					break;
				#endregion
				#region StarverWander
				case 27:
					Boss = Bosses[4];
					Name = "徘徊者-The StarverWander";
					Story = "饥饿，迷失，徘徊。这具畸变的肉体中寄宿着旋涡的能量";
					NeedEx = new ItemLists
					{
						{ TaskDifficulty.Hard, new TaskItem[]{
												(3456, 400),
												(ItemID.TruffleWorm, 10),
												(1164, 1),
												(1553, 1, 82)
						}}
};
					Rewards = new TaskItem[]
					{
												(2797, 1),
												(2797, 1),
												(2797, 1),
												(2797, 1)
					};
					break;
				#endregion
				#region StarverRedeemer
				case 28:
					Boss = Bosses[Bosses.Length - 6];
					Name = "清赎者-The StarverRedeemer";
					Story = "苦难，炼狱，救赎。这具畸变的肉体中填满了星尘的微粒";
					NeedEx = new ItemLists
					{
						{ TaskDifficulty.Hard, new TaskItem[]{
												(3459, 400),
												(ItemID.TruffleWorm, 10),
												(2676, 200),
												(3571, 1, 83)
						}}
};
					Rewards = new TaskItem[]
					{
												(2422, 1),
												(2422, 1),
												(2294, 1),
												(2294, 1)
					};
					break;
				#endregion
				#region StarverAdjudicator
				case 29:
					Boss = Bosses[Bosses.Length - 5];
					Name = "裁决者-The StarverAdjudicator";
					Story = "仲裁，审判，裁决。这具畸变的肉体中蕴含着星云的奥秘";
					NeedEx = new ItemLists
					{
						{ TaskDifficulty.Hard, new TaskItem[]{
												(3457, 400),
												(ItemID.TruffleWorm, 10),
												(1101, 300),
												(905, 1),
												(3541, 1, 60)
						}}
};
					Rewards = new TaskItem[]
					{
												(2795, 1),
												(2795, 1),
												(2795, 1),
												(2795, 1)
					};
					break;
				#endregion
				#region StarverDestroyer
				case 30:
					Boss = Bosses[Bosses.Length - 4];
					Name = "毁灭者-The StarverDestroyer";
					Story = "毁灭，创造，平衡。宇宙的暴怒内蕴其中。现实总是与理想相违背，努力的结果往往被他人夺去";
					NeedEx = new ItemLists
					{
						{ TaskDifficulty.Hard, new TaskItem[]{
												(3458, 400),
												(ItemID.TruffleWorm, 10),
												(3801, 1),
												(3872, 1),
												(3873, 1),
												(774, 999),
												(3065, 1, 81)
						}}
};
					Rewards = new TaskItem[]
					{
												(2880, 1),
												(2880, 1),
												(2880, 1),
												(2880, 1),
					};
					break;
				#endregion
				#region Sleep
				case 31:
					Boss = Bosses[Bosses.Length - 3];
					Name = "稍作休整";
					Story = "片刻的宁静";
					NeedEx = new ItemLists
					{
						{ TaskDifficulty.Hard, new TaskItem[]{
												(969, 30),
												(2426, 30),
												(357, 30),
												(2266, 30),
												(859, 1),
												(856, 1)
						}}
};
					NeedEx = new ItemLists
					{
						{ TaskDifficulty.Hard, new TaskItem[]{
												(3467, 200),
												(3746, 300),
												(1358, 30)
						}}
};
					break;
				#endregion
				#region RedDevil
				case 32:
					Name = "地狱领主-The Lord of the Underworld";
					Story = "恶魔一族的君主，似乎能够自如地使用地狱的威能。苦痛的亡魂在熔岩中挣扎，他们呼喊着那个早已消逝的名字";
					NeedEx = new ItemLists
					{
						{ TaskDifficulty.Hard, new TaskItem[]{
												(2701, 999),
												(1518, 3),
												(683, 1),
												(1445, 1),
												(2312, 120)
						}}
};
					Rewards = new TaskItem[]
					{
												(3467, 80)
					};
					break;
				#endregion
				#region Pigron
				case 33:
					Boss = Bosses[5];
					Name = "畸变生物-thAt d15ToRt10N";
					Story = "在被污染的冰窟中滋生的畸形生物，无节制地进行着自我增殖。自我循环或外界摄取，这个世界支配着残酷的法则";
					NeedEx = new ItemLists
					{
						{ TaskDifficulty.Hard, new TaskItem[]{
												(3532, 20),
												(2429, 1),
												(2337, 20),
												(2338, 20),
												(2339, 20)
						}}
};
					Rewards = new TaskItem[]
					{
												(1067, 50)
					};
					break;
				#endregion
				#region PrimeEx
				case 34:
					Boss = Bosses[6];
					Name = "骷髅暴徒 - The Skeletrorist";
					Story = "这位恐惧之主早已失去了往日的荣光，变异的躯壳只寄宿着无尽的愤怒。暴虐与残忍，嗜血与屠戮，欲望和执念造就的只有悲剧";
					NeedEx = new ItemLists
					{
						{ TaskDifficulty.Hard, new TaskItem[]{
												(1367, 5),
												(WorldGen.crimson ? 1569 : 1571, 1, 59),
												(1260, 1, 83),
												(1156, 1, 82),
												(1572, 1, 83)
						}}
};
					Rewards = new TaskItem[]
					{
												(3467, 90),
												(3035, 1),
												(3035, 1),
												(3035, 1),
												(3035, 1)
					};
					break;
				#endregion
				#region Worm
				case 35:
					Boss = Bosses[7];
					Name = "地脉吸食者-The Seisminth";
					Story = "吸食了地脉的精华，但仍然保留着蠕虫的姿态。这片大地已经千疮百孔，在一次次的轮回中逐渐褪色";
					NeedEx = new ItemLists
					{
						{ TaskDifficulty.Hard, new TaskItem[]{
												(ItemID.RottenChunk,99),
												(ItemID.CursedFlame,99),
												(3794,30),
												(3380,200)
						}}
};
					Rewards = new TaskItem[]
					{
												(3467, 200),
												(2768)

					};
					break;
				#endregion
				#region Retinazer
				case 36:
					Boss = Bosses[8];
					Name = "预视全知的左眼-The Propheyes";
					Story = "最终的劫难已逐渐逼近，艰难的旅程也终于抵达尾声。未来的图景从未如此朦胧";
					NeedEx = new ItemLists
					{
						{ TaskDifficulty.Hard, new TaskItem[]{
												(ItemID.RetinazerTrophy, 6)
						}}
};
					Rewards = new TaskItem[]
					{
												(3467, 120),
												(678,30)
					};
					break;
				#endregion
				#region Spazmatism
				case 37:
					Boss = Bosses[9];
					Name = "洞悉过往的右眼-The Propheyes";
					Story = "历史的书页遗失了一角，某个过往的存在切断了与这个世界的联系。他是连接一切的钥匙";
					NeedEx = new ItemLists
					{
						{ TaskDifficulty.Hard, new TaskItem[]{
												(ItemID.SpazmatismTrophy, 6)
						}}
};
					Rewards = new TaskItem[]
					{
												(3467, 120),
												(678,30)
					};
					break;
				#endregion
				#region EndTrial1
				case 38:
					Boss = Bosses[10];
					Name = "末世预言--凶兆";
					Story = "T.S.K.S:--==欢迎==--初次联络--stardust的玩家们，你们迄今为止的努力着实令人敬佩，而现在，我们有一项伟大而光荣的委托要交付于你们";
					NeedEx = new ItemLists
					{
						{ TaskDifficulty.Hard, new TaskItem[]{(20, 66),
												(703, 66),
												(22, 66),
												(704, 66),
												(21, 66),
												(705, 66),
												(19, 66),
												(706, 66),
												(57, 66),
												(1257, 66),
												(175, 66),
												(1184, 66),
												(381, 66),
												(382, 66),
												(1191, 66),
												(391, 66),
												(1198, 66)
							}}
};
					Rewards = new TaskItem[]
					{
												(3706, 99),
												(3706, 99),
												(3706, 99),
												(3706, 99)
					};
					Level += 1250;
					break;
				#endregion
				#region EndTrial2
				case 39:
					Name = "末世预言--混乱";
					Story = "T.S.K.S:你们之前已经漂亮地完成了我们的每一次委托，所以我们相信这一件委托这对你们来说绝非难事";
					NeedEx = new ItemLists
					{
						{ TaskDifficulty.Hard, new TaskItem[]{
												(2430, 1),
												(2491, 1),
												(2502, 1),
												(2428, 1),
												(3771, 1),
												(3260, 1),
												(1914, 1),
												(2771, 1),
												(2769, 1)
						}}
};
					Rewards = new TaskItem[]
					{
												(2903, 99),
												(2903, 99),
												(2903, 99),
												(2903, 99)
					};
					Level += 1250;
					break;
				#endregion
				#region EndTrial3
				case 40:
					Name = "末世预言--秩序";
					Story = "T.S.K.S:我们的确掌握着某种限制大型怪物生成的技术，但归根究底其源泉也是我们所共有的AURA能量";
					NeedEx = new ItemLists
					{
						{ TaskDifficulty.Hard, new TaskItem[]{(493, 1),
												(492, 1),
												(1515, 1),
												(749, 1),
												(761, 1),
												(1165, 1),
												(785, 1),
												(821, 1),
												(822, 1),
												(1162, 1)
							}}
};
					Rewards = new TaskItem[]
					{
												(2902, 99),
												(2902, 99),
												(2902, 99),
												(2902, 99)
					};
					Level += 1250;
					break;
				#endregion
				#region EndTrial4
				case 41:
					Name = "末世预言--开端";
					Story = "T.S.K.S:但是，不知道各位是否想过，AURA的源泉又是什么呢？我们解析过这种能量的构造，其波动与你们的某一种欲望十分符合";
					NeedEx = new ItemLists
					{
						{ TaskDifficulty.Hard, new TaskItem[]{(3278, 1),
												(3262, 1),
												(3289, 1),
												(3281, 1),
												(3280, 1),
												(3279, 1),
												(3292, 1),
												(3286, 1),
												(3291, 1),
												(3285, 1),
												(3283, 1),
												(3282, 1),
												(3290, 1),
												(3317, 1),
												(3315, 1),
												(3284, 1),
												(3316, 1)
							}}
};
					Rewards = new TaskItem[]
					{
												(3705, 99),
												(3705, 99),
												(3705, 99),
												(3705, 99)
					};
					Level += 1250;
					break;
				#endregion
				#region EndTrial5
				case 42:
					Name = "末世预言--降临";
					Story = "T.S.K.S:至于委托的内容...啊，谢谢各位，我们已经快要完成了...通过这几次的任务...";
					NeedEx = new ItemLists
					{
						{ TaskDifficulty.Hard, new TaskItem[]{
												(562),
												(1600),
												(564),
												(1601),
												(1596),
												(1603),
												(1604),
												(1597),
												(566),
												(1610),
												(568),
												(569),
												(1598),
												(571),
												(3237),
												(1605),
												(1608),
												(563),
												(1609),
												(3371),
												(3236),
												(3235),
												(1963),
												(1965),
												(3796),
												(3869),
												(3370),
												(567),
												(572),
												(574),
												(1599),
												(1607),
												(1606),
												(3044),
						}}
};
					Rewards = new TaskItem[]
					{
												(3725, 50)
					};
					break;
				#endregion
				#region End
				case 43:
					Name = "末世预言--终末";
					Boss = Bosses[Bosses.Length - 2];
					Story = "THE STARDUST KARMA SACRIFICE--T.S.K.S:终于...我找到了...";
					break;
					#endregion
			}
			if (Normal)
			{
				if (NeedEx!= null && NeedEx.ContainsKey(Difficulty))
					Needs = NeedEx[Difficulty];
				if (Needs == null || StarverConfig.Config.TaskNeedNoItem)
				{
					Needs = DefaultNeed;
				}
				if (Rewards == null)
				{
					Rewards = DefaultReward;
				}

				SetDefault();
			}
		}
		#endregion
		#region SetDefault
		protected virtual void SetDefault()
		{
			checker = DefaultCheck;
			StringBuilder SB = new StringBuilder("");
			SB.AppendFormat("主线任务#{0}--({1}){2}", ID, Name, Story == null ? "" : ("\n\"" + Story + "\""));
			if (CheckItem)
			{
				SB.AppendFormat("\n物品要求:\n[i:{0}](标识物,不消耗)", Mark);
				foreach(object obj in Needs)
				{
					SB.Append(obj);
				}
				SB.Append("\n物品奖励:\n");
				foreach (object obj in Rewards)
				{
					SB.Append(obj);
				}
			}
			Description = SB.ToString();
		}
		#endregion
		#region ToString
		public override string ToString()
		{
			return Description;
		}
		#endregion
		#region Checks
		#region Check
		protected CheckDelegate checker;
		public bool Check(StarverPlayer player)
		{
			return checker(player);
		}
		protected bool DefaultCheck(StarverPlayer player)
		{
			bool flag = true;
			if (Normal)
			{
				if(!(Boss is null))
				{
					if(!Boss.Downed)
					{
						StarverPlayer.All.SendMessage($"这个任务被{Boss.Name}诅咒了...", Color.Red);
						return false;
					}
				}
				if (CheckLevel)
				{
					flag = flag && Utils.AverageLevel >= Level;
					if(flag == false)
					{
						StarverPlayer.All.SendMessage($"全员平均等级未达标({Level})",Color.Red);
					}
				}
				if(CheckItem)
				{
					flag = flag && CheckChests();
				}
			}
			else
			{

			}
			return flag;
		}
		#endregion
		#region CheckChests
		protected bool CheckChests()
		{
			foreach (Chest chest in Main.chest) 
			{
				if (chest == null || !CheckItems(chest.item))
				{
					goto Continue;
				}
				EatChestItem(chest.item);
				RewardChestItem(chest);
				return true;
			Continue:
				continue;
			}
			return false;
		}
		#endregion
		#region CheckItem
		protected bool CheckItems(Item[] items)
		{
			if (items.Length <= Needs.Length)
			{
				return false;
			}
			if (items[0].type != Mark)
			{
				return false;
			}
			for (int i = 0; i < Needs.Length; i++)
			{
				if (!Needs[i].Match(items[i + 1]))
				{
					StarverPlayer.All.SendMessage($"所需物品{Needs[i]} 与检测到物品[i/s{items[1 + i].stack}:{items[1 + i].type}]不符", Color.Blue);
					return false;
				}
				else
				{
					StarverPlayer.All.SendMessage($"所需物品{Needs[i]} 与检测到物品[i/s{items[1 + i].stack}:{items[1 + i].type}]符合", Color.Aqua);
				}
			}
			return true;
		}
		#endregion
		#region Check_20
		private static bool Check_20()
		{
			foreach (Chest chest in Main.chest)
			{
				if (chest == null || chest.item[0].type != 50)
					continue;
				for (int i = 1; i < 21; i++)
				{
					if (chest.item[i].type > 3042 || chest.item[i].type < 2869)
					{
						goto loops;
					}
				}
				for (int i = 1; i < chest.item.Length; i++)
				{
					chest.item[i] = new Item();
				}
				return true;
			loops:;
			}
			return false;
		}
		#endregion
		#region EatItems
		protected void EatChestItem(Item[] items)
		{
			for (int i = 1; i < items.Length; i++)
			{
				items[i] = new Item();
			}
		}
		#endregion
		#region Reward
		protected void RewardChestItem(Chest chest)
		{
			for (int i = 0; i < Rewards.Length && i < chest.item.Length; i++)
			{
				int num = Utils.NewItem(Rewards[i].ID, Rewards[i].Stack, Rewards[i].Prefix);
				chest.AddShop(Main.item[num]);
			}
		}
		#endregion
		#endregion
	}
}
