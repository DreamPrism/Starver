using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers.NPCSystem
{
	using TerrariaApi.Server;
	public class StarverNPCManager : IStarverPlugin
	{
		#region Properties
		public StarverConfig Config => StarverConfig.Config;
		#endregion
		#region Interfeces
		public bool Enabled
		{
			get
			{
				if(!Config.EnableAura)
				{
					StarverPlayer.Server.SendInfoMessage("使用Starver的NPC组件需要先开启Aura以及Task组件");
					return false;
				}
				return Config.EnableNPC;
			}
		}
		public void Load()
		{
			StarverNPC.Load();
			ServerApi.Hooks.GameUpdate.Register(Starver.Instance, StarverNPC.DoUpDate);
			ServerApi.Hooks.NpcLootDrop.Register(Starver.Instance, OnDrop);
			//ServerApi.Hooks.NpcKilled.Register(Starver.Instance, StarverNPC.OnNPCKilled);
		}
		public void UnLoad()
		{
			StarverNPC.Release();
			ServerApi.Hooks.GameUpdate.Deregister(Starver.Instance, StarverNPC.DoUpDate);
			ServerApi.Hooks.NpcLootDrop.Deregister(Starver.Instance, OnDrop);
			//ServerApi.Hooks.NpcKilled.Deregister(Starver.Instance, StarverNPC.OnNPCKilled);
		}
		#endregion
		#region OnDrop
		private static void OnDrop(NpcLootDropEventArgs args)
		{
			if(StarverNPC.NPCs[args.NpcArrayIndex] != null && StarverNPC.NPCs[args.NpcArrayIndex].Active)
			{
				args.Handled = StarverNPC.NPCs[args.NpcArrayIndex].OverrideRawDrop;
				StarverNPC.NPCs[args.NpcArrayIndex].Drop(args.NpcArrayIndex);
			}
		}
		#endregion
	}
}
