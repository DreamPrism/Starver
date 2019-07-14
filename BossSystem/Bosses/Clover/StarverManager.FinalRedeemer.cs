using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers.BossSystem.Bosses.Clover
{
	using Base;
	using Microsoft.Xna.Framework;
	using Vector = TOFOUT.Terraria.Server.Vector2;
	public partial class StarverManager
	{
		private class FinalRedeemer : StarverRedeemer
		{
			#region Fields
			private StarverManager Manager;
			#endregion
			#region Spawn
			public void Spawn(Vector2 where, int lvl = 2000,StarverManager manager = null)
			{
				Manager = manager;
				Spawn(where, lvl, PI * 2 * 1 / 2,16 * 27);
			}
			#endregion
			#region RealAI
			public override void RealAI()
			{
				if(!Manager.Active)
				{
					KillMe();
					return;
				}
				base.RealAI();
				RealNPC.dontTakeDamage = true;
			}
			#endregion
		}
	}
}
