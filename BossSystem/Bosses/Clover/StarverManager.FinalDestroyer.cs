using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers.BossSystem.Bosses.Clover
{
	using Base;
	using Vector = TOFOUT.Terraria.Server.Vector2;
	public partial class StarverManager
	{
		private class FinalDestroyer : StarverDestroyer
		{
			#region Fields
			private Vector ForRounding;
			private StarverManager Manager;
			#endregion
			#region DamageIndex
			public override float DamageIndex => Manager.DamageIndex;
			#endregion
			#region Spawn
			public void Spawn(Vector2 where, int lvl = 2000, StarverManager manager = null)
			{
				base.Spawn(where, lvl);
				Manager = manager;
				ForRounding.X = 0;
				ForRounding.Y = 16 * 27;
				ForRounding.Angle = PI * 2 * 4 / 2;
			}
			#endregion
			#region RealAI
			public override void RealAI()
			{
				#region Common
				if(!Manager.Active)
				{
					KillMe();
					return;
				}
				Center = TargetPlayer.Center + ForRounding;
				ForRounding.Angle += PI / 120;
				TargetPlayer.TPlayer.ZoneTowerSolar = true;
				TargetPlayer.SendData(PacketTypes.Zones, "", Target);
				#endregion
				#region Mode
				switch (Mode)
				{
					#region SelectMode
					case BossMode.WaitForMode:
						SelectMode();
						break;
					#endregion
					#region DemonSickle
					case BossMode.DemonSickle:
						if (modetime > 60 * 15)
						{
							RushBegin();
						}
						if (Timer % 2 == 0)
						{
							DemonSickle();
						}
						break;
					#endregion
					#region Fire
					case BossMode.Fire:
						if (modetime > 60 * 12)
						{
							RushBegin();
						}
						if (Timer % 2 == 0)
						{
							Fire();
						}
						break;
					#endregion
					#region FlamingScythe
					case BossMode.FlamingScythe:
						unsafe
						{
							if (StarverAI[0] > 8)
							{
								StarverAI[0] = 0;
								RushBegin();
							}
							if (Timer % 90 == 0)
							{
								FlamingScythe((int)StarverAI[0]++);
							}
						}
						break;
					#endregion
					#region Present
					case BossMode.Present:
						if (modetime > 60 * 12)
						{
							RushBegin();
						}
						if (Timer % 75 == 0)
						{
							Present();
						}
						break;
					#endregion
				}
				#endregion
			}
			#endregion
			#region SelectMode
			private new void SelectMode()
			{
				modetime = 0;
				LastCenter = Center;
				switch (lastMode)
				{
					#region DemonSickle
					case BossMode.DemonSickle:
						Mode = BossMode.Fire;
						break;
					#endregion
					#region Fire
					case BossMode.Fire:
						Mode = BossMode.FlamingScythe;
						break;
					#endregion
					#region FlamingScythe
					case BossMode.FlamingScythe:
						Mode = BossMode.Present;
						break;
					#endregion
					#region Present
					case BossMode.Present:
						Mode = BossMode.DemonSickle;
						break;
						#endregion
				}
			}
			#endregion
		}
	}
}
