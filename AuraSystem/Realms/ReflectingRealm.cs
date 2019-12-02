using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace Starvers.AuraSystem.Realms
{
	using Vector = TOFOUT.Terraria.Server.Vector2;
	public class ReflectingRealm : CircleRealm
	{
		private int Owner;
		private StarverPlayer OwnerPlayer => Starver.Players[Owner];
		public ReflectingRealm() : base(true)
		{
			Owner = Main.myPlayer;
		}

		public ReflectingRealm(int owner) : this()
		{
			if (0 <= owner && owner <= 255)
			{
				Owner = owner;
			}
		}

		protected override void SetDefault()
		{
			Radium = 16 * 15;
			TimeLeft = 60 * 10;
		}
		protected override void InternalUpdate()
		{
			if (Owner == Main.myPlayer)
			{
				foreach (var proj in Main.projectile)
				{
					if (proj.active == false || proj.hostile)
					{
						continue;
					}
					if (AtBorder(proj))
					{
						Reflect(proj);
					}
				}
				foreach (var player in Starver.Players)
				{
					if (player == null || player.Dead)
					{
						continue;
					}
					if (AtBorder(player))
					{
						Reflect(player);
					}
				}
			}
			else if (OwnerPlayer == null)
			{
				Kill();
			}
			else
			{

			}
		}
		protected bool AtBorder(Entity entity)
		{
			return Math.Abs(Vector2.Distance(Center, entity.Center) - Radium) <= 16;
		}
		protected void Reflect(Projectile proj)
		{
			Vector2 Distance = proj.Center - Center;
			proj.velocity -= 2 * Distance * Vector2.Dot(Distance, proj.velocity) / (Radium * Radium);
			proj.SendData();
		}
		protected void Reflect(StarverPlayer player)
		{
			Vector2 Distance = player.Center - Center;
			player.Velocity -= 2 * Distance * Vector2.Dot(Distance, player.Velocity) / (Radium * Radium);
		}
	}
}
