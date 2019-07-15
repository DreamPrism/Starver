using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers.BossSystem.Bosses.Clover
{
	using Base;
	using Microsoft.Xna.Framework;
	using Starvers.WeaponSystem;
	using Terraria.ID;
	using Vector = TOFOUT.Terraria.Server.Vector2;
	public partial class StarverManager : StarverBoss
	{
		#region Fields
		private Rectangle MyRect;
		private FinalWander CrazyWang = new FinalWander();
		private FinalRedeemer Deaths = new FinalRedeemer();
		private FinalAdjudicator Wither = new FinalAdjudicator();
		private FinalDestroyer TOFOUT = new FinalDestroyer();
		#endregion
		#region ctor
		public StarverManager() : base(3)
		{
			TaskNeed = 42;
			FullName = "Revolc The Starver Manager";
			Name = "The Starver Manager";
			RawType = NPCID.MoonLordCore;
			DefaultDefense = 100;
			DefaultLife = 6000000;
			DefaultLifes = 1000;
			Drops = new DropItem[] 
			{
				new DropItem(new int[] { ItemID.CultistBossBag }, 20, 21),
				new DropItem(new int[] { Currency.Melee }, 99 / 4, 80),
				new DropItem(new int[] { Currency.Ranged }, 99 / 4, 80),
				new DropItem(new int[] { Currency.Magic }, 99 / 4, 80),
				new DropItem(new int[] { Currency.Minion }, 99 / 4, 80),
			};
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
			#region Self
			#region Common
			#region TrackingTarget
			FakeVelocity = (Vector)(TargetPlayer.Center - Center) / 13;
			#endregion
			#region DetectDamage
			MyRect = new Rectangle((int)Center.X - 16 * 20, (int)Center.Y - 16 * 20, 16 * 40, 16 * 40);
			foreach(var proj in Terraria.Main.projectile)
			{
				if (proj.friendly == false || proj.active == false)
				{
					continue;
				}
				if(Vector2.Distance(proj.Center,Center) < 16 * 20)
				{
					RealNPC.StrikeNPC(proj.damage, 0, 0, false, true, true, Terraria.Main.player[proj.owner]);
					if ((proj.penetrate -= 1) < 1)
					{
						proj.Kill();
					}
					//proj.Damage();
				}
			}
			#endregion
			#endregion
			#region Mode
			#endregion
			#endregion
		}
		#endregion
	}
}
