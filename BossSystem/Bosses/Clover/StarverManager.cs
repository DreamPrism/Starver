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
		private Vector2 LeftUp = default;// new Vector2(-16 * 20, -16 * 20);
		private Rectangle MyRect;
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
			#region Self
			base.Spawn(where, lvl);
			RealNPC.width = 16 * 40;
			RealNPC.height = 16 * 40;
			RealNPC.dontTakeDamage = false;
			SendData();
			#endregion
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
			#region TrackingTarget
			FakeVelocity = (Vector)(TargetPlayer.Center - Center) / 13;
			#endregion
			#region DetectDamage
			MyRect = new Rectangle((int)Center.X, (int)Center.Y, 16 * 40, 16 * 40);
			foreach(var proj in Terraria.Main.projectile)
			{
				if (proj.friendly == false || proj.active == false)
				{
					continue;
				}
				if(Vector2.Distance(proj.Center,Center) < 16 * 20)
				{
					RealNPC.StrikeNPC(proj.damage, 0, 0, false, true, true, Terraria.Main.player[proj.owner]);
					proj.Colliding(proj.getRect(), MyRect);
					//proj.Damage();
				}
			}
			#endregion
		}
		#endregion
	}
}
