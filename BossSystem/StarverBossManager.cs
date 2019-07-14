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
	public class StarverBossManager : IStarverPlugin
	{
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
			new CultistEx()
		};
		public static Random Rand => Starver.Rand;
		public static List<AIDelegate> BossAIs = new List<AIDelegate>();
		#endregion
		#region Events
		#region OnUpdate
		private void OnUpdate(EventArgs args)
		{
			foreach(var AI in BossAIs)
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
	}
}
