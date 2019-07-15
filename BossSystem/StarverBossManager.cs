using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;

namespace Starvers.BossSystem
{
	using Bosses;
	using Bosses.Base;
	using Microsoft.Xna.Framework;
	using Starvers.BossSystem.Bosses.Clover;
	using Terraria.ID;

	public class StarverBossManager : IStarverPlugin
	{
		#region Fields
		private int EndBossDelay = 0;
		private bool TriedSpawn = false;
		/// <summary>
		/// 生成那个boss?
		/// </summary>
		private int SpawnIndex;
		private int SpawnDelay;
		#endregion
		#region ctor
		static StarverBossManager()
		{
			foreach(var boss in Bosses)
			{
				BossAIs.Add(boss.AI);
			}
		}
		#endregion
		#region Interfaces
		public delegate void AIDelegate(object args);
		public StarverConfig Config => StarverConfig.Config;
		public bool Enabled => Config.EnableBoss && Config.EnableAura;
		public void Load()
		{
			ServerApi.Hooks.NpcKilled.Register(Starver.Instance, OnKilled);
			ServerApi.Hooks.NpcLootDrop.Register(Starver.Instance, OnDrop);
			Commands.ChatCommands.Add(new Command(Perms.Boss.Spawn, BossCommand, "stboss"));
			ServerApi.Hooks.GameUpdate.Register(Starver.Instance, OnUpdate);
		}
		public void UnLoad()
		{
			ServerApi.Hooks.NpcKilled.Deregister(Starver.Instance, OnKilled);
			ServerApi.Hooks.NpcLootDrop.Deregister(Starver.Instance, OnDrop);
			ServerApi.Hooks.GameUpdate.Deregister(Starver.Instance, OnUpdate);
		}
		#endregion
		#region Properties
		public bool EndTrial
		{
			get => StarverBoss.EndTrial;
			set => StarverBoss.EndTrial = value;
		}
		public int EndTrialProcess
		{
			get => StarverBoss.EndTrialProcess;
			set => StarverBoss.EndTrialProcess = value;
		}
		public static StarverBoss[] Bosses { get; private set; } = new StarverBoss[]
		{
			new EyeEx(),
			new BrainEx(),
			new QueenBeeEx(),
			new SkeletronEx(),
			new DarkMageEx(),
			new RedDevilEx(),
			new PigronEx(),
			new PrimeEx(),
			new DestroyerEx(),
			new RetinazerEx(),
			new SpazmatismEx(),
			new StarverWander(),
			new StarverRedeemer(),
			new StarverAdjudicator(),
			new StarverDestroyer(),
			new StarverManager(),
			new CultistEx()
		};
		public static StarverManager Clover => Bosses[Bosses.Length - 2] as StarverManager;
		public static Random Rand => Starver.Rand;
		public static List<AIDelegate> BossAIs = new List<AIDelegate>();
		#endregion
		#region Events
		#region OnUpdate
		private void OnUpdate(EventArgs args)
		{
			#region SpawnBoss
			if (Main.dayTime)
			{
				TriedSpawn = false;
				SpawnDelay = 0;
			}
			else
			{
				#region SelectWhichToSpawn
				if (!TriedSpawn)
				{
					TriedSpawn = true;
					bool CanSpawn = false;
					foreach (var ply in Starver.Players)
					{
						if (ply is null)
						{
							continue;
						}
						CanSpawn |= ply.Active;
					}
					if (EndTrial == false && CanSpawn && Rand.Next(100) >= 10) 
					{
						SpawnDelay = 60 * 30;
						SpawnIndex = Rand.Next(Bosses.Length - 2);
						if (!Bosses[SpawnIndex].CanSpawn)
						{
							do
							{
								SpawnIndex = Rand.Next(Bosses.Length - 2);
							}
							while (!Bosses[SpawnIndex].CanSpawn);
						}
						StarverPlayer.All.SendMessage(Bosses[SpawnIndex].CommingMessage, Bosses[SpawnIndex].CommingMessageColor);
					}
				}
				#endregion
				#region Spawning
				if(SpawnDelay > 0)
				{
					SpawnDelay--;
					if(SpawnDelay == 0)
					{
						foreach(var ply in Starver.Players)
						{
							if(!ply.Active)
							{
								continue;
							}
							Bosses[SpawnIndex].Spawn(ply.Center + Rand.NextVector2(16 * 20));
						}
					}
				}
				#endregion
			}
			#endregion
			#region BossAI
			foreach (var AI in BossAIs)
			{
				try
				{
					AI(null);
				}
				catch(Exception e)
				{
					StarverPlayer.Server.SendInfoMessage(e.ToString());
				}
			}
			#endregion
			#region EndTrial
			if(EndTrial)
			{
				switch(EndTrialProcess)
				{
					#region 0(Starting)
					case 0:
						KillTower();
						WorldGen.TriggerLunarApocalypse();
						SummonTower(NPCID.LunarTowerVortex);
						StarverPlayer.All.SendMessage("这将是你们最后一战...", Color.DarkGreen);
						EndTrialProcess++;
						break;
					#endregion
					#region 1(Vortex Invading)
					case 1:
						if(NPC.ShieldStrengthTowerVortex < 1)
						{
							TowerTakeDamage();
						}
						if(!NPC.TowerActiveVortex)
						{
							KillTower();
							EndBossDelay = 60 * 30;
							EndTrialProcess++;
						}
						break;
					#endregion
					#region 2(CrazyWang)
					case 2:
						if(EndBossDelay-- == 60 * 30)
						{
							StarverPlayer.All.SendMessage("你们可曾记得", Color.BlueViolet);
						}
						else if(EndBossDelay == 60 * 28)
						{
							StarverPlayer.All.SendMessage("在那个世界里", Color.BlueViolet);
						}
						else if(EndBossDelay == 60 * 26)
						{
							StarverPlayer.All.SendMessage("你们的噩梦吗", Color.BlueViolet);
						}
						else if(EndBossDelay == 60 * 23)
						{
							StarverPlayer.All.SendMessage("是的", Color.BlueViolet);
						}
						else if(EndBossDelay == 60 * 20)
						{
							StarverPlayer.All.SendMessage("这次", Color.BlueViolet);
						}
						else if(EndBossDelay == 60 * 18)
						{
							StarverPlayer.All.SendMessage("你们要对付的第一个", Color.BlueViolet);
						}
						else if(EndBossDelay == 60 * 13)
						{
							StarverPlayer.All.SendMessage("就是他", Color.BlueViolet);
						}
						else if(EndBossDelay == 60 * 10)
						{
							StarverPlayer.All.SendMessage("越来越近了", Color.BlueViolet);
						}
						else if(EndBossDelay == 60 * 7)
						{
							StarverPlayer.All.SendMessage("是他来了", Color.BlueViolet);
						}
						else if(EndBossDelay == 60 * 3)
						{
							StarverPlayer.All.SendMessage("祝你们好运", Color.BlueViolet);
						}
						else if(EndBossDelay == 60 * 1)
						{
							Bosses[Bosses.Length - 6].Spawn(SelectLuckyPlayer() + Rand.NextVector2(16 * 80), 3000);
							EndBossDelay = -1;
						}
						break;
					#endregion
					#region 3(Stardust Invading)
					case 3:
						if(EndBossDelay < 0)
						{
							EndBossDelay = 0;
							WorldGen.TriggerLunarApocalypse();
							SummonTower(NPCID.LunarTowerStardust);
							StarverPlayer.All.SendMessage("战斗还在继续...", Color.DarkGreen);
						}
						if(!NPC.TowerActiveStardust)
						{
							KillTower();
							EndBossDelay = 60 * 30;
							EndTrialProcess++;
						}
						break;
					#endregion
					#region 4(Deaths)
					case 4:
						if (EndBossDelay-- == 60 * 30)
						{
							StarverPlayer.All.SendMessage("他似乎永远是最平静的那个", Color.BlueViolet);
						}
						else if (EndBossDelay == 60 * 28)
						{
							StarverPlayer.All.SendMessage("平时什么都不做", Color.BlueViolet);
						}
						else if (EndBossDelay == 60 * 26)
						{
							StarverPlayer.All.SendMessage("就是拿着根鱼竿", Color.BlueViolet);
						}
						else if (EndBossDelay == 60 * 23)
						{
							StarverPlayer.All.SendMessage("坐在岸边", Color.BlueViolet);
						}
						else if (EndBossDelay == 60 * 20)
						{
							StarverPlayer.All.SendMessage("安心钓鱼", Color.BlueViolet);
						}
						else if (EndBossDelay == 60 * 18)
						{
							StarverPlayer.All.SendMessage("但是这样是躲不掉的", Color.BlueViolet);
						}
						else if (EndBossDelay == 60 * 13)
						{
							StarverPlayer.All.SendMessage("该打的时候还是得去打", Color.BlueViolet);
						}
						else if (EndBossDelay == 60 * 10)
						{
							StarverPlayer.All.SendMessage("越来越近了", Color.BlueViolet);
						}
						else if (EndBossDelay == 60 * 7)
						{
							StarverPlayer.All.SendMessage("到了该动手的时候了", Color.BlueViolet);
						}
						else if (EndBossDelay == 60 * 3)
						{
							StarverPlayer.All.SendMessage("祝你们好运", Color.BlueViolet);
						}
						else if (EndBossDelay == 60 * 1)
						{
							Bosses[Bosses.Length - 5].Spawn(SelectLuckyPlayer() + Rand.NextVector2(16 * 80), 3000);
							EndBossDelay = -1;
						}
						break;
					#endregion
					#region 5(Nebula Invading)
					case 5:
						if (EndBossDelay < 0)
						{
							EndBossDelay = 0;
							SummonTower(NPCID.LunarTowerNebula);
							StarverPlayer.All.SendMessage("战斗还将延续...", Color.DarkGreen);
						}
						if(!NPC.TowerActiveNebula)
						{
							KillTower();
							EndBossDelay = 60 * 30;
							EndTrialProcess++;
						}
						break;
					#endregion
					#region 6(Wither)
					case 6:
						if (EndBossDelay-- == 60 * 30)
						{
							StarverPlayer.All.SendMessage("你们可曾记得", Color.BlueViolet);
						}
						else if (EndBossDelay == 60 * 28)
						{
							StarverPlayer.All.SendMessage("在那个世界里", Color.BlueViolet);
						}
						else if (EndBossDelay == 60 * 26)
						{
							StarverPlayer.All.SendMessage("你们的噩梦吗", Color.BlueViolet);
						}
						else if (EndBossDelay == 60 * 23)
						{
							StarverPlayer.All.SendMessage("是的", Color.BlueViolet);
						}
						else if (EndBossDelay == 60 * 20)
						{
							StarverPlayer.All.SendMessage("这次", Color.BlueViolet);
						}
						else if (EndBossDelay == 60 * 18)
						{
							StarverPlayer.All.SendMessage("你们要对付的第一个", Color.BlueViolet);
						}
						else if (EndBossDelay == 60 * 13)
						{
							StarverPlayer.All.SendMessage("就是他", Color.BlueViolet);
						}
						else if (EndBossDelay == 60 * 10)
						{
							StarverPlayer.All.SendMessage("越来越近了", Color.BlueViolet);
						}
						else if (EndBossDelay == 60 * 7)
						{
							StarverPlayer.All.SendMessage("是他来了", Color.BlueViolet);
						}
						else if (EndBossDelay == 60 * 3)
						{
							StarverPlayer.All.SendMessage("祝你们好运", Color.BlueViolet);
						}
						else if (EndBossDelay == 60 * 1)
						{
							Bosses[Bosses.Length - 6].Spawn(SelectLuckyPlayer() + Rand.NextVector2(16 * 80), 3000);
							EndBossDelay = -1;
						}
						break;
						#endregion
				}
			}
			#endregion
		}
		#endregion
		#region OnKilled
		internal static void OnKilled(NpcKilledEventArgs args)
		{
			if (StarverBoss.AliveBoss < 0)
			{
				return;
			}
			else
			{
				foreach(StarverBoss boss in Bosses)
				{
					if (boss.Active && args.npc.whoAmI == boss.Index)
					{
						boss.LifeDown();
						return;
					}
				}
			}
		}
		#endregion
		#region OnDrop
		internal static void OnDrop(NpcLootDropEventArgs args)
		{
			if (StarverBoss.AliveBoss < 0)
			{
				return;
			}
			else
			{
				foreach (StarverBoss boss in Bosses)
				{
					if (boss.Active || args.NpcArrayIndex == boss.Index)
					{
						args.Handled = true;
						return;
					}
				}
			}
		}
		#endregion
		#endregion
		#region Command
		internal static void BossCommand(CommandArgs args)
		{
			if(args.Parameters.Count < 1)
			{
				int i = 0;
				args.Player.SendInfoMessage("临界等级: {0}", StarverBoss.Criticallevel);
				foreach(StarverBoss boss in Bosses)
				{
					args.Player.SendInfoMessage("{0}: {1}", ++i, boss.GetType().Name);
				}
			}
			else
			{
				
				int idx;
				int lvl;
				if (args.Parameters[0] == "*")
				{
					idx = -1;
				}
				else
				{
					try
					{
						idx = int.Parse(args.Parameters[0]);
					}
					catch
					{
						idx = 0;
					}
				}
					try
					{
						lvl = int.Parse(args.Parameters[1]);
					}
					catch
					{
						lvl = StarverBoss.Criticallevel;
					}
				if (idx == 0)
				{
					foreach (StarverBoss boss in Bosses)
					{
						boss.KillMe();
					}
					StarverBoss.AliveBoss = 0;
				}
				else if(idx == -1)
				{
					foreach(var boss in Bosses)
					{
						try
						{
							boss.Spawn(Rand.NextVector2(16 * 15) + args.TPlayer.Center, lvl);
						}
						catch(Exception e)
						{
							args.Player.SendErrorMessage(e.ToString());
						}
					}
				}
				else
				{
					idx--;
					if (idx >= Bosses.Length)
					{
						args.Player.SendErrorMessage("参数错误");
					}
					try
					{
						Bosses[idx].Spawn(Rand.NextVector2(16 * 15) + args.TPlayer.Center, lvl);
					}
					catch (Exception e)
					{
						args.Player.SendErrorMessage(e.ToString());
					}
				}

			}
		}
		#endregion
		#region EndTrial
		#region SummonTower
		private void SummonTower(int MainTower)
		{
			#region TransformTower
			foreach (var NPC in Main.npc)
			{
				if (!NPC.active)
				{
					continue;
				}
				if (NPC.type == NPCID.LunarTowerNebula ||
					NPC.type == NPCID.LunarTowerSolar ||
					NPC.type == NPCID.LunarTowerStardust ||
					NPC.type == NPCID.LunarTowerVortex)
				{
					NPC.SetDefaults(MainTower);
					NetMessage.SendData((int)PacketTypes.NpcUpdate, -1, -1, null, NPC.whoAmI);
				}
				if (NPC.type == MainTower)
				{
					NPC.dontTakeDamage = true;
				}
			}
			#endregion
			#region Shield
			switch (MainTower)
			{
				case NPCID.LunarTowerVortex:
					NPC.ShieldStrengthTowerVortex = 600;
					break;
				case NPCID.LunarTowerStardust:
					NPC.ShieldStrengthTowerStardust = 600;
					break;
				case NPCID.LunarTowerNebula:
					NPC.ShieldStrengthTowerNebula = 600;
					break;
				case NPCID.LunarTowerSolar:
					NPC.ShieldStrengthTowerSolar = 600;
					break;
			}
			#endregion
		}
		#endregion
		#region TowerCanTakeDamage
		private void TowerTakeDamage()
		{
			foreach(var NPC in Main.npc)
			{
				if (!NPC.active)
				{
					continue;
				}
				if (NPC.type == NPCID.LunarTowerNebula ||
					NPC.type == NPCID.LunarTowerSolar || 
					NPC.type == NPCID.LunarTowerStardust || 
					NPC.type == NPCID.LunarTowerVortex)
				{
					NPC.dontTakeDamage = false;
				}
			}
		}
		#endregion
		#region KillTower
		private void KillTower()
		{
			foreach (var NPC in Main.npc)
			{
				if (!NPC.active)
				{
					continue;
				}
				if (NPC.type == NPCID.LunarTowerNebula ||
					NPC.type == NPCID.LunarTowerSolar ||
					NPC.type == NPCID.LunarTowerStardust ||
					NPC.type == NPCID.LunarTowerVortex)
				{
					NPC.active = false;
				}
			}
			NPC.MoonLordCountdown = 0;
		}
		#endregion
		#region SelectLuckyPlayer
		private Vector2 SelectLuckyPlayer()
		{
			Vector2 pos = default;
			foreach(var ply in Starver.Players)
			{
				if(!ply.Active)
				{
					continue;
				}
				pos = ply.Center;
				break;
			}
			return pos;
		}
		#endregion
		#endregion
	}
}
