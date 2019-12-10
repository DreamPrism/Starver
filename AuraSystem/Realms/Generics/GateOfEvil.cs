using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers.AuraSystem.Realms.Generics
{
	using Interfaces;
    using Microsoft.Xna.Framework;
    using Terraria;
    using Terraria.ID;

    public class GateOfEvil<T> : StarverRealm<T>
		where T : IBorderConditioner,new()
	{
		public bool NewBySystem { get; set; }
		public GateOfEvil() : base(false)
		{
			conditioner = new T()
			{
				ProjID = ProjectileID.VortexVortexPortal
			};
		}
		protected override void InternalUpdate()
		{
			if (Starver.Instance.Aura.Stellaria == null)
			{
#if DEBUG
				StarverPlayer.Server.SendErrorMessage(@"未检测到 ""Stellaria"" 
魔界之门已自动关闭");
#endif
				Kill();
				return;
			}
				base.InternalUpdate();
			foreach (var player in Starver.Players)
			{
				if (player == null || !player.Active || !InRange(player))
					continue;
				if (NewBySystem)
				{
					StarverAuraManager.EvilGateSpawnCountDown = 60 * 5;
				}
				if (Starver.Config.EvilWorld)
				{
					Starver.BackToHard(player);
				}
				else
				{
					if (NewBySystem)
					{
						Starver.Instance.Aura.LastPos[player] = new Vector2
						{
							X = Main.spawnTileX * 16,
							Y = Main.spawnTileY * 16
						};
					}
					Starver.SendToEvil(player);
				}
				Kill();
			}
		}


	}
}
