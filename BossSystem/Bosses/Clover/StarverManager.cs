using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers.BossSystem.Bosses.Clover
{
	using Base;
	using Microsoft.Xna.Framework;
	using Terraria.ID;
	using Vector = TOFOUT.Terraria.Server.Vector2;
	public partial class StarverManager : StarverBoss
	{
		#region Fields
		private FinalWander CrazyWang = new FinalWander();
		private FinalRedeemer Deaths = new FinalRedeemer();
		private FinalAdjudicator Wither = new FinalAdjudicator();
		private FinalDestroyer TOFOUT = new FinalDestroyer();
		#endregion
		#region ctor
		public StarverManager() : base(3)
		{
			FullName = "Revolc The Starver Manager";
			Name = "The Starver Manager";
			RawType = NPCID.MoonLordCore;
			DefaultDefense = 40;
			DefaultLife = 2000000;
			DefaultLifes = 250;
		}
		#endregion
		#region KillMe
		public override void KillMe()
		{
			base.KillMe();
			CrazyWang.KillMe();
			Deaths.KillMe();
			Wither.KillMe();
			TOFOUT.KillMe();
		}
		#endregion
		#region Spawn
		public override void Spawn(Vector2 where, int lvl = 5000)
		{
			base.Spawn(where, lvl);
			_active = true;
			RealNPC.dontTakeDamage = false;
			SendData();
			#region TheFollows
			CrazyWang.Spawn(where, 40000, this);
			Deaths.Spawn(where, 40000, this);
			Wither.Spawn(where, 40000, this);
			TOFOUT.Spawn(where, 40000, this);
			#endregion
		}
		#endregion
		#region RealAI
		public override void RealAI()
		{
			#region TheFollows
			#region CheckActive
			if (!CrazyWang.Active)
			{
				CrazyWang.Respawn();
			}
			if(!Deaths.Active)
			{
				Deaths.Respawn();
			}
			if(!Wither.Active)
			{
				Wither.Respawn();
			}
			if(!TOFOUT.Active)
			{
				TOFOUT.Respawn();
			}
			#endregion
			#region AIUpdate
			CrazyWang.AI();
			Deaths.AI();
			Wither.AI();
			TOFOUT.AI();
			#endregion
			#endregion
		}
		#endregion
	}
}
