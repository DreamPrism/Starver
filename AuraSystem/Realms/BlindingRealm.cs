using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace Starvers.AuraSystem.Realms
{
	using Vector = TOFOUT.Terraria.Server.Vector2;
	public class BlindingRealm : CircleRealm
	{
		private const int Blind = BuffID.Obstructed;
		private const int MaxTimeLeft = 60 * 20;
		private int[] Blasters;
		public BlindingRealm() : base(true)
		{
			DefaultTimeLeft = MaxTimeLeft;
		}

		public override void Kill()
		{
			base.Kill();
			foreach(var idx in Blasters)
			{
				Main.projectile[idx].type = 0;
				Main.projectile[idx].active = false;
				StarverPlayer.All.SendData(PacketTypes.ProjectileNew, "", idx);
			}
		}

		public override void Start()
		{
			base.Start();
			Blasters = new int[60];
			for (int i = 0; i < Blasters.Length; i++)
			{
				Blasters[i] = Utils.NewProj
					(
					position: Center + Vector.FromPolar(Math.PI * 2 / 60 * i, Radium),
					velocity: Vector2.Zero,
					Type: ProjectileID.NebulaSphere,
					Damage: 0,
					KnockBack: 0,
					Owner: 255
					);
				Main.projectile[Blasters[i]].aiStyle = -1;
			}
		}

		protected override void SetDefault()
		{
			Radium = 16 * 30;
		}
		protected override void InternalUpdate()
		{
			if (TimeLeft % 60 == 0)
			{
				foreach (var player in Starver.Players)
				{
					if (player == null || !player.Active)
						continue;
					if (InRange(player))
					{
						player.SetBuff(Blind, 60);
					}
				}
			}
			if(TimeLeft % 2 == 0)
			{
				UpdateBorders();
			}
		}
		protected void UpdateBorders()
		{
			for (int i = 0; i < Blasters.Length; i++)
			{
				StarverPlayer.All.SendData(PacketTypes.ProjectileNew, "", Blasters[i]);
			}
		}
	}
}
