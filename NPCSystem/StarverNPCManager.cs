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
		#region Interfeces
		public bool Enabled => StarverConfig.Config.EnableNPC;
		public void Load()
		{
			ServerApi.Hooks.GameUpdate.Register(Starver.Instance, StarverNPC.DoUpDate);
			//ServerApi.Hooks.NpcKilled.Register(Starver.Instance, StarverNPC.OnNPCKilled);
		}
		public void UnLoad()
		{
			ServerApi.Hooks.GameUpdate.Deregister(Starver.Instance, StarverNPC.DoUpDate);
			//ServerApi.Hooks.NpcKilled.Deregister(Starver.Instance, StarverNPC.OnNPCKilled);
		}
		#endregion
	}
}
