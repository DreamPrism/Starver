using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers.AuraSystem.Realms.Generics
{
	using Interfaces;
    using Terraria.ID;

    public class GateOfEvil<T> : StarverRealm<T>
		where T : IBorderConditioner,new()
	{
		public GateOfEvil() : base(false)
		{
			conditioner = new T()
			{
				ProjID = ProjectileID.VortexVortexPortal
			};
		}
		protected override void InternalUpdate()
		{
			base.InternalUpdate();
			foreach (var player in Starver.Players)
			{
				if (player == null || !player.Active || !IsCross(player))
					continue;
				if (Starver.Config.EvilWorld)
				{
					Starver.BackToHard(player);
				}
				else
				{
					Starver.SendToEvil(player);
				}
				Kill();
			}
		}


	}
}
