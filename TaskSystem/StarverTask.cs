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
	public class StarverTask
	{
		#region Properties
		public int ID { get; protected set; }
		public string Description { get; protected set; }
		public TaskItem[] Needs { get; protected set; }
		public TaskItem[] Rewards { get; protected set; }
		public string Name { get; protected set; }
		public string Story { get; protected set; }
		public int Mark { get; protected set; } = ItemID.MagicMirror;
		public int Level { get; protected set; }
		public int Exp { get; protected set; }
		public bool Normal { get; protected set; } = true;
		public bool CheckLevel { get; protected set; } = false;
		public bool CheckItem { get; protected set; } = true;
		#endregion
		#region Statics
		public readonly static TaskItem[] Default = new TaskItem[] { new TaskItem(ItemID.PlatinumCoin) };
		public const int MAINLINE = 43;
		#endregion
		#region cctor
		static StarverTask()
		{
			if (WorldGen.oreTier1 == -1)
			{
				WorldGen.oreTier1 = 107;
				if (WorldGen.genRand.Next(2) == 0)
				{
					WorldGen.oreTier1 = 221;
				}
			}
			if (WorldGen.oreTier2 == -1)
			{
				WorldGen.oreTier2 = 108;
				if (WorldGen.genRand.Next(2) == 0)
				{
					WorldGen.oreTier2 = 222;
				}
			}
			if (WorldGen.oreTier3 == -1)
			{
				WorldGen.oreTier3 = 111;
				if (WorldGen.genRand.Next(2) == 0)
				{
					WorldGen.oreTier3 = 223;
				}
			}
		}
		#endregion
		#region ctor
		protected StarverTask() { }
		public StarverTask(int id)
		{
			ID = id;
			Level = 10 * ID;
			switch (ID)
			{
				#region KingSlime
				case 1:
					Name = "美味凝胶";
					Story = "美味又易燃";
					Needs = new TaskItem[]
					{
						new TaskItem(ItemID.Gel, 99)
					};
					Rewards = new TaskItem[]
					{
						new TaskItem(ItemID.SlimeCrown,20)
					};
					break;
				#endregion
				#region Eye
				case 2:
					Name = "盯着你";
					Story = "希望站在我们和克苏鲁之眼间的不是弱小的你";
					Needs = new TaskItem[]
					{
						new TaskItem(ItemID.Lens, 28),
						new TaskItem(ItemID.DemonBanner, 2)
					};
					Rewards = new TaskItem[]
					{
						new TaskItem(ItemID.SuspiciousLookingEye,20)
					};
					break;
				#endregion
				#region Evil
				case 3:
					int meterial;
					int mushroom;
					Rewards = new TaskItem[1];
					if (WorldGen.crimson)
					{
						Name = "幽暗裂缝";
						Story = "让世界变得血腥......";
						meterial = ItemID.Vertebrae;
						mushroom = ItemID.ViciousMushroom;
						Rewards[0] = new TaskItem(ItemID.WormFood, 20);
					}
					else
					{
						Name = "活死人之地";
						Story = "让世界变得邪恶......";
						meterial = ItemID.RottenChunk;
						mushroom = ItemID.VileMushroom;
						Rewards[0] = new TaskItem(ItemID.BloodySpine, 20);
					}
					Needs = new TaskItem[]
					{
						new TaskItem(meterial,25),
						new TaskItem(mushroom,5)
					};
					break;
				#endregion
				#region DD2 T1
				case 4:
					Name = "暗黑法师的降临";
					Story = "他来了...";
					Needs = new TaskItem[]
					{
						new TaskItem(ItemID.FallenStar, 20),
						new TaskItem(ItemID.MolotovCocktail,99)
					};
					Rewards = new TaskItem[]
					{
						new TaskItem(ItemID.DefendersForge,2)
					};
					break;
				#endregion
				#region Bee
				case 5:
					Name = "蜂巢的主任";
					Story = "我的蜂蜜在哪?";
					Needs = new TaskItem[]
					{
						new TaskItem(ItemID.Stinger, 20),
						new TaskItem(ItemID.Vine, 30),
						new TaskItem(ItemID.Hive,99),
						new TaskItem(ItemID.BottledHoney,30)
					};
					Rewards = new TaskItem[]
					{
						new TaskItem(ItemID.Abeemination,20)
					};
					break;
				#endregion
				#region Skeletron
				case 6:
					Name = "诅咒";
					Story = "你的能力也只是刚刚好把我从诅咒中解救出来...";
					Needs = new TaskItem[]
					{
						new TaskItem(ItemID.Bone, 99),
						new TaskItem(ItemID.Skull),
						new TaskItem(ItemID.BoneSword),
						new TaskItem(ItemID.BoneKey),
						new TaskItem(ItemID.BoneKey),
					};
					Rewards = new TaskItem[]
					{
						new TaskItem(ItemID.BoneKey),
						new TaskItem(ItemID.ClothierVoodooDoll)
					};
					break;
				#endregion
				#region Wall
				case 7:
					Name = "仍然饥饿";
					Story = "地下世界的主人和核心";
					Needs = new TaskItem[]
					{
						new TaskItem(ItemID.StrangePlant1, 5),
						new TaskItem(ItemID.StrangePlant2, 5),
						new TaskItem(ItemID.StrangePlant3, 5),
						new TaskItem(ItemID.StrangePlant4, 5),
						new TaskItem(ItemID.HellstoneBar,30)
					};
					Rewards = new TaskItem[]
					{
						new TaskItem(ItemID.GuideVoodooDoll),
						new TaskItem(ItemID.GuideVoodooDoll),
						new TaskItem(ItemID.GuideVoodooDoll),
						new TaskItem(ItemID.TrueNightsEdge,1,PrefixID.Legendary)
					};
					break;
				#endregion
				#region Light
				case 8:
					Name = "光与暗(其一)";
					Story = "天堂般的地狱";
					Needs = new TaskItem[]
					{
						new TaskItem(ItemID.SoulofLight, 250),
						new TaskItem(ItemID.CrystalShard, 99),
						new TaskItem(ItemID.LightShard, 8)
					};
					Rewards = new TaskItem[]
					{
						new TaskItem(ItemID.LightKey,20)
					};
					break;
				#endregion
				#region Night
				case 9:
					Name = "光与暗(其二)";
					if (WorldGen.crimson)
					{
						Story = "空气中弥漫着血腥味";
						Needs = new TaskItem[]
						{
							new TaskItem(ItemID.SoulofNight, 250),
							new TaskItem(ItemID.Ichor, 99),
							new TaskItem(ItemID.DarkShard, 8)
						};
					}
					else
					{
						Story = "空气中充斥着腐朽的气息";
						Needs = new TaskItem[]
						{
							new TaskItem(ItemID.SoulofNight, 250),
							new TaskItem(ItemID.CursedFlame, 99),
							new TaskItem(ItemID.DarkShard, 8)
						};
					}
					Rewards = new TaskItem[]
					{
						new TaskItem(ItemID.NightKey,20)
					};
					break;
				#endregion
				#region Twins
				case 10:
					Name = "全知魔眼";
					Story = "这将是个可怕的夜晚...";
					Needs = new TaskItem[]
					{
						new TaskItem(ItemID.Lens,40),
						new TaskItem(ItemID.DemonBanner,15),
						new TaskItem(ItemID.PalladiumOre,666),
						new TaskItem(ItemID.EyeofCthulhuTrophy,3)
					};
					Rewards = new TaskItem[]
					{
						new TaskItem(ItemID.MechanicalEye,20)
					};
					break;
				#endregion
				#region Prime
				case 11:
					Name = "恐惧之王";
					Story = "周围的空气越来越冷...";
					Needs = new TaskItem[]
					{
						new TaskItem(ItemID.Bone,280),
						new TaskItem(ItemID.AngryBonesBanner,15),
						new TaskItem(ItemID.CursedSkullBanner,3),
						new TaskItem(ItemID.OrichalcumOre,444),
					};
					Rewards = new TaskItem[]
					{
						new TaskItem(ItemID.MechanicalSkull,20)
					};
					break;
				#endregion
				#region Destroyer
				case 12:
					Name = "破坏之王";
					Story = "你感到来自地下深处的震动...";
					if (WorldGen.crimson)
					{
						Needs = new TaskItem[]
						{
							new TaskItem(ItemID.Vertebrae, 99),
							new TaskItem(ItemID.HerplingBanner, 10),
							new TaskItem(ItemID.TitaniumOre, 222)
						};
					}
					else
					{
						Needs = new TaskItem[]
						{
							new TaskItem(ItemID.RottenChunk, 99),
							new TaskItem(ItemID.CorruptorBanner, 10),
							new TaskItem(ItemID.TitaniumOre, 222)
						};
					}
					Rewards = new TaskItem[]
					{
						new TaskItem(ItemID.MechanicalWorm,20)
					};
					break;
				#endregion
				#region DD2 T2
				case 13:
					Name = "食人魔的降临";
					Story = "他来了...";
					Needs = new TaskItem[]
					{
						new TaskItem(ItemID.SoulofMight, 222),
						new TaskItem(ItemID.SoulofSight, 222),
						new TaskItem(ItemID.SoulofFright, 222)
					};
					Rewards = new TaskItem[]
					{
						new TaskItem(ItemID.DD2ElderCrystal,20)
					};
					break;
				#endregion
				#region Plantera
				case 14:
					Name = "大南方植物";
					Story = "世纪之花灯泡";
					Needs = new TaskItem[]
					{
						new TaskItem(ItemID.ChlorophyteOre,999),
						new TaskItem(ItemID.TortoiseBanner,3),
						new TaskItem(ItemID.AngryTrapperBanner,3),
					};
					Rewards = new TaskItem[]
					{
						new TaskItem(ItemID.TheAxe),
						new TaskItem(ItemID.TheAxe),
						new TaskItem(ItemID.TheAxe),
						new TaskItem(ItemID.TheAxe)
					};
					break;
				#endregion
				#region Golem
				case 15:
					Name = "蜥蜴人们的信仰";
					Needs = new TaskItem[]
					{
						new TaskItem(ItemID.TempleKey,20),
						new TaskItem(ItemID.LihzahrdBanner,10),
						new TaskItem(ItemID.FlyingSnakeBanner,10),
						new TaskItem(ItemID.LunarTabletFragment,99)
					};
					Rewards = new TaskItem[]
					{
						new TaskItem(ItemID.LihzahrdPowerCell,99)
					};
					break;
				#endregion
				#region DD2 T3
				case 16:
					Name = "Betsy降临";
					Story = "他来了...";
					Needs = new TaskItem[]
					{
						new TaskItem(ItemID.MartianConduitPlating,999),
						new TaskItem(ItemID.BeetleHusk,99)
					};
					break;
				#endregion
				#region Cultist
				case 17:
					Name = "邪教组织";
					Story = "我们是邪教组织";
					Needs = new TaskItem[]
					{
						new TaskItem(ItemID.GoldenKey),
						new TaskItem(ItemID.ShadowKey),
						new TaskItem(WorldGen.crimson?ItemID.CrimsonKey:ItemID.CorruptionKey),
						new TaskItem(ItemID.FrozenKey),
						new TaskItem(ItemID.JungleKey),
						new TaskItem(ItemID.HallowedKey),
						new TaskItem(ItemID.CosmicCarKey),
						new TaskItem(ItemID.NightKey),
						new TaskItem(ItemID.LightKey),
						new TaskItem(ItemID.BoneKey)
					};
					Rewards = new TaskItem[]
					{
						new TaskItem(ItemID.CultistBossBag),
						new TaskItem(ItemID.CultistBossBag),
						new TaskItem(ItemID.CultistBossBag),
						new TaskItem(ItemID.CultistBossBag),
						new TaskItem(ItemID.CultistBossBag),
						new TaskItem(ItemID.CultistBossBag),
						new TaskItem(ItemID.CultistBossBag),
						new TaskItem(ItemID.CultistBossBag),
						new TaskItem(ItemID.CultistBossBag),
						new TaskItem(ItemID.CultistBossBag),
						new TaskItem(ItemID.CultistBossBag)
					};
					break;
				#endregion
				#region Solar
				case 18:
					Name = "宇宙之怒";
					Story = "你的头脑变得麻木...";
					Needs = new TaskItem[]
					{
						new TaskItem(ItemID.AncientCultistTrophy),
						new TaskItem(ItemID.BossTrophyBetsy),
						new TaskItem(ItemID.BossTrophyDarkmage),
						new TaskItem(ItemID.BossTrophyOgre),
						new TaskItem(WorldGen.crimson?ItemID.BrainofCthulhuTrophy:ItemID.EaterofWorldsTrophy),
						new TaskItem(ItemID.EverscreamTrophy),
						new TaskItem(ItemID.EyeofCthulhuTrophy),
						new TaskItem(ItemID.GolemTrophy),
						new TaskItem(ItemID.IceQueenTrophy),
						new TaskItem(ItemID.KingSlimeTrophy),
						new TaskItem(ItemID.MartianSaucerTrophy),
						new TaskItem(ItemID.MourningWoodTrophy),
						new TaskItem(ItemID.PlanteraTrophy),
						new TaskItem(ItemID.PumpkingTrophy),
						new TaskItem(ItemID.QueenBeeTrophy),
						new TaskItem(ItemID.RetinazerTrophy),
						new TaskItem(ItemID.SantaNK1Trophy),
						new TaskItem(ItemID.SkeletronPrimeTrophy),
						new TaskItem(ItemID.SpazmatismTrophy),
						new TaskItem(ItemID.WallofFleshTrophy),
						new TaskItem(ItemID.DestroyerTrophy),
						new TaskItem(ItemID.DukeFishronTrophy)
					};
					Rewards = new TaskItem[]
					{
						new TaskItem(ItemID.PlatinumCoin,999)
					};
					break;
				#endregion
				#region Nebula
				case 19:
					Name = "银河之力";
					Story = "你痛苦不堪...";
					Needs = new TaskItem[]
					{
						new TaskItem(ItemID.SolarCoriteBanner,10),
						new TaskItem(ItemID.SolarCrawltipedeBanner,1),
						new TaskItem(ItemID.SolarDrakomireBanner,5),
						new TaskItem(ItemID.SolarSolenianBanner,5),
						new TaskItem(ItemID.SolarSrollerBanner,5)
					};
					Rewards = new TaskItem[]
					{
						new TaskItem(ItemID.SolarMonolith,999)
					};
					break;
				#endregion
				#region Stardust
				case 20:
					Name = "星尘之粒";
					Story = "超自然的声音环绕在你的周围...";
					Needs = new TaskItem[]
					{
						new TaskItem(ItemID.StardustJellyfishBanner,5),
						new TaskItem(ItemID.StardustLargeCellBanner,5),
						new TaskItem(ItemID.StardustWormBanner,5),
						new TaskItem(ItemID.StardustSoldierBanner,5)
					};
					Rewards = new TaskItem[]
					{
						new TaskItem(ItemID.StardustMonolith,999)
					};
					break;
				#endregion
				#region Vortex
				case 21:
					Name = "漩涡能量";
					Story = "月亮末日慢慢靠近...";
					Needs = new TaskItem[]
					{
						new TaskItem(ItemID.VortexSoldierBanner,5),
						new TaskItem(ItemID.VortexRiflemanBanner,5),
						new TaskItem(ItemID.VortexHornetQueenBanner,20)
					};
					Rewards = new TaskItem[]
					{
						new TaskItem(ItemID.VortexMonolith,999)
					};
					break;
				#endregion
				#region EyeEx
				case 22:
					Name = "The Eye of Phobia";
					Needs = new TaskItem[]
					{
						new TaskItem(38, 99),
						new TaskItem(236, 4),
						new TaskItem(891),
						new TaskItem(2112)
					};
					Rewards = new TaskItem[]
					{
						new TaskItem(3467, 40),
						new TaskItem(3453)
					};
					break;
				#endregion
				#region BeeEx
				case 23:
					Name = "Hive Mind";
					Story = "Phobia   n.恐惧症";
					Needs = new TaskItem[]
					{
						new TaskItem(1133, 20),
						new TaskItem(887),
						new TaskItem(2108),
						new TaskItem(1291, 9)
					};
					Rewards = new TaskItem[4]
					{
						new TaskItem(1326),
						new TaskItem(1326),
						new TaskItem(1326),
						new TaskItem(1326)
					};
					break;
				#endregion
				#region SkeletonEx
				case 24:
					Name = "Hyperosteogeny";
					Needs = new TaskItem[]
					{
						new TaskItem(154, 500, 0),
						new TaskItem(3085, 18, 0),
						new TaskItem(1281, 1, 0)
					};
					Rewards = new TaskItem[1]
					{
						new TaskItem(3467, 80, 0)
					};
					break;
				#endregion
				#region DarkMage
				case 25:
					Name = "流放巫师-The Banished Enchater";
					Needs = new TaskItem[]
					{
						new TaskItem(3817, 999, 0),
						new TaskItem(109, 99, 0),
						new TaskItem(3864, 1, 0)
					};
					Rewards = new TaskItem[]
					{
						new TaskItem(3467, 120, 0)
					};
					break;
				#endregion
				#region StarverWander
				case 26:
					Name = "徘徊者-The StarverWander";
					Needs = new TaskItem[]
					{
						new TaskItem(3456, 400, 0),
						new TaskItem(1164, 1, 0),
						new TaskItem(1553, 1, 82)
					};
					Rewards = new TaskItem[]
					{
						new TaskItem(2797, 1, 0)
					};
					break;
				#endregion
				#region StarverRedeemer
				case 27:
					Name = "清赎者-The StarverRedeemer";
					Story = "我就是想要你们的肝";
					Needs = new TaskItem[]
					{
						new TaskItem(3459, 400, 0),
						new TaskItem(2676, 200, 0),
						new TaskItem(3571, 1, 83)
					};
					Rewards = new TaskItem[]
					{
						new TaskItem(2294, 1, 0),
						new TaskItem(2294, 1, 0),
						new TaskItem(2294, 1, 0),
						new TaskItem(2294, 1, 0)
					};
					break;
				#endregion
				#region StarverAdjudicator
				case 28:
					Name = "裁决者-The StarverAdjudicator";
					Needs = new TaskItem[]
					{
						new TaskItem(3457, 400, 0),
						new TaskItem(1101, 300, 0),
						new TaskItem(905, 1, 0),
						new TaskItem(3541, 1, 60)
					};
					Rewards = new TaskItem[]
					{
						new TaskItem(2795, 1, 0),
						new TaskItem(2795, 1, 0),
						new TaskItem(2795, 1, 0),
						new TaskItem(2795, 1, 0)
					};
					break;
				#endregion
				#region StarverDestroyer
				case 29:
					Name = "破灭者-The StarverDestroyer";
					Story = "相信我,穿上它你就无敌了";
					Needs = new TaskItem[]
					{
						new TaskItem(3458, 400, 0),
						new TaskItem(3801, 1, 0),
						new TaskItem(3872, 1, 0),
						new TaskItem(3873, 1, 0),
						new TaskItem(774, 999, 0),
						new TaskItem(3065, 1, 81)
					};
					Rewards = new TaskItem[]
					{
						new TaskItem(2880, 1, 0),
						new TaskItem(2880, 1, 0),
						new TaskItem(2880, 1, 0),
						new TaskItem(2880, 1, 0)
					};
					break;
				#endregion
				#region Sleep
				case 30:
					Name = "稍作休整";
					Story = "这并不代表着你可以去睡大觉了";
					Needs = new TaskItem[]
					{
						new TaskItem(969, 30, 0),
						new TaskItem(2426, 30, 0),
						new TaskItem(357, 30, 0),
						new TaskItem(2266, 30, 0),
						new TaskItem(859, 1, 0),
						new TaskItem(856, 1, 0)
					};
					Needs = new TaskItem[]
					{
						new TaskItem(3467, 200, 0),
						new TaskItem(3746, 300, 0),
						new TaskItem(1358, 30, 0)
					};
					break;
				#endregion
				#region RedDevil
				case 31:
					Name = "地狱领主-The Lord of the Underworld";
					Needs = new TaskItem[]
					{
						new TaskItem(2701, 999, 0),
						new TaskItem(1518, 3, 0),
						new TaskItem(683, 1, 0),
						new TaskItem(1445, 1, 0)
					};
					Rewards = new TaskItem[]
					{
						new TaskItem(3467, 80, 0)
					};
					break;
				#endregion
				#region Pigron
				case 32:
					Name = "畸变生物-thAt d15ToRt10N";
					Needs = new TaskItem[]
					{
						new TaskItem(3532, 30, 0),
						new TaskItem(2429, 1, 0)
					};
					Rewards = new TaskItem[]
					{
						new TaskItem(1067, 50, 0)
					};
					break;
				#endregion
				#region IceQueen
				case 33:
					Name = "薄雾之冰-The Vapoureeze";
					Needs = new TaskItem[]
					{
						new TaskItem(2161, 25, 0),
						new TaskItem(1958, 20, 0),
						new TaskItem(593, 999, 0)
					};
					Rewards = new TaskItem[]
					{
						new TaskItem(3467, 85, 0),
						new TaskItem(1253, 6, 0)
					};
					break;
				#endregion
				#region PrimeEx
				case 34:
					Name = "骷髅暴徒-The Skeletrorist";
					Needs = new TaskItem[]
					{
						new TaskItem(1367, 5, 0),
						new TaskItem(WorldGen.crimson ? 1569 : 1571, 1, 59),
						new TaskItem(1260, 1, 83),
						new TaskItem(1156, 1, 82),
						new TaskItem(1572, 1, 83)
					};
					Rewards = new TaskItem[]
					{
						new TaskItem(3467, 90, 0),
						new TaskItem(3035, 1, 0),
						new TaskItem(3035, 1, 0),
						new TaskItem(3035, 1, 0),
						new TaskItem(3035, 1, 0)
					};
					break;
				#endregion
				#region Retinazer
				case 35:
					Name = "全知魔眼-The Propheyes";
					Needs = new TaskItem[]
					{
						new TaskItem(ItemID.RetinazerTrophy, 6, 0)
					};
					Rewards = new TaskItem[]
					{
						new TaskItem(3467, 56, 0)
					};
					break;
				#endregion
				#region Spazmatism
				case 36:
					Name = "全知魔眼-The Propheyes";
					Needs = new TaskItem[]
					{
						new TaskItem(ItemID.SpazmatismTrophy, 6, 0)
					};
					Rewards = new TaskItem[]
					{
						new TaskItem(3467, 54, 0)
					};
					break;
				#endregion
				#region PlanteraEx
				case 37:
					Name = "拟态孢囊-The Sporemulate";
					Needs = new TaskItem[]
					{
						new TaskItem(183, 999, 0),
						new TaskItem(195, 99, 0),
						new TaskItem(2109, 1, 0)
					};
					Rewards = new TaskItem[]
					{
						new TaskItem(3467, 125, 0)
					};
					break;
				#endregion
				#region EndTrial1
				case 38:
					Name = "末世预言--凶兆";
					Needs = new TaskItem[17]
					{
						new TaskItem(20, 99, 0),
						new TaskItem(703, 99, 0),
						new TaskItem(22, 88, 0),
						new TaskItem(704, 88, 0),
						new TaskItem(21, 77, 0),
						new TaskItem(705, 77, 0),
						new TaskItem(19, 66, 0),
						new TaskItem(706, 66, 0),
						new TaskItem(57, 55, 0),
						new TaskItem(1257, 55, 0),
						new TaskItem(175, 44, 0),
						new TaskItem(1184, 33, 0),
						new TaskItem(381, 33, 0),
						new TaskItem(382, 22, 0),
						new TaskItem(1191, 22, 0),
						new TaskItem(391, 11, 0),
						new TaskItem(1198, 11, 0)
					};
					Rewards = new TaskItem[]
					{
						new TaskItem(3706, 99, 0),
						new TaskItem(3706, 99, 0),
						new TaskItem(3706, 99, 0),
						new TaskItem(3706, 99, 0)
					};
					Level += 1250;
					break;
				#endregion
				#region EndTrial2
				case 39:
					Name = "末世预言--混乱";
					Needs = new TaskItem[]
					{
						new TaskItem(2430, 1, 0),
						new TaskItem(2491, 1, 0),
						new TaskItem(2502, 1, 0),
						new TaskItem(2428, 1, 0),
						new TaskItem(3771, 1, 0),
						new TaskItem(3260, 1, 0),
						new TaskItem(1914, 1, 0),
						new TaskItem(2771, 1, 0),
						new TaskItem(2769, 1, 0)
					};
					Rewards = new TaskItem[]
					{
						new TaskItem(2903, 99, 0),
						new TaskItem(2903, 99, 0),
						new TaskItem(2903, 99, 0),
						new TaskItem(2903, 99, 0)
					};
					Level += 1250;
					break;
				#endregion
				#region EndTrial3
				case 40:
					Name = "末世预言--秩序";
					Needs = new TaskItem[10]
					{
						new TaskItem(493, 1, 0),
						new TaskItem(492, 1, 0),
						new TaskItem(1515, 1, 0),
						new TaskItem(749, 1, 0),
						new TaskItem(761, 1, 0),
						new TaskItem(1165, 1, 0),
						new TaskItem(785, 1, 0),
						new TaskItem(821, 1, 0),
						new TaskItem(822, 1, 0),
						new TaskItem(1162, 1, 0)
					};
					Rewards = new TaskItem[]
					{
						new TaskItem(2902, 99, 0),
						new TaskItem(2902, 99, 0),
						new TaskItem(2902, 99, 0),
						new TaskItem(2902, 99, 0)
					};
					Level += 1250;
					break;
				#endregion
				#region EndTrial4
				case 41:
					Name = "末世预言--开端";
					Needs = new TaskItem[17]
					{
						new TaskItem(3278, 1, 0),
						new TaskItem(3262, 1, 0),
						new TaskItem(3289, 1, 0),
						new TaskItem(3281, 1, 0),
						new TaskItem(3280, 1, 0),
						new TaskItem(3279, 1, 0),
						new TaskItem(3292, 1, 0),
						new TaskItem(3286, 1, 0),
						new TaskItem(3291, 1, 0),
						new TaskItem(3285, 1, 0),
						new TaskItem(3283, 1, 0),
						new TaskItem(3282, 1, 0),
						new TaskItem(3290, 1, 0),
						new TaskItem(3317, 1, 0),
						new TaskItem(3315, 1, 0),
						new TaskItem(3284, 1, 0),
						new TaskItem(3316, 1, 0)
					};
					Rewards = new TaskItem[]
					{
						new TaskItem(3705, 99, 0),
						new TaskItem(3705, 99, 0),
						new TaskItem(3705, 99, 0),
						new TaskItem(3705, 99, 0)
					};
					Level += 1250;
					break;
				#endregion
				#region EndTrial5
				case 42:
					Name = "末世预言--降临";
					Needs = new TaskItem[]
					{
						new TaskItem(2901, 10, 0),
						new TaskItem(3372, 1, 0),
						new TaskItem(3357, 5, 0)
					};
					Rewards = new TaskItem[]
					{
						new TaskItem(3725, 50, 0)
					};
					break;
				case 43:
					Name = "末世预言--终末";
					break;
					#endregion
			}
			if (Normal)
			{
				if (Needs == null)
				{
					Needs = Default;
				}
				if (Rewards == null)
				{
					Rewards = Default;
				}
				SetDefaut();
			}
		}
		#endregion
		#region SetDefault
		protected virtual void SetDefaut()
		{
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
		#region Checks
		#region Check
		public virtual bool Check(StarverPlayer player)
		{
			bool flag = true;
			if (Normal)
			{
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
		private void EatChestItem(Item[] items)
		{
			for (int i = 1; i < items.Length; i++)
			{
				items[i] = new Item();
			}
		}
		#endregion
		#region Reward
		private void RewardChestItem(Chest chest)
		{
			for (int i = 0; i < Rewards.Length && i < chest.item.Length; i++)
			{
				int num = Item.NewItem(new Vector2(0, 0), new Vector2(0, 0), Rewards[i].ID, Rewards[i].Stack, false, Rewards[i].Prefix);
				chest.AddShop(Main.item[num]);
			}
		}
		#endregion
		#endregion
	}
}
