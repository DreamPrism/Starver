using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Starvers.AuraSystem.Realms.Conditioners
{
	using Interfaces;
	using Vector = TOFOUT.Terraria.Server.Vector2;
	public class EllipseReflector : EllipseConditioner, IRealmReflector
	{

		public void Reflect(Entity entity)
		{
			entity.velocity = CalcReflectedVelocity(entity);
		}

		public EllipseReflector()
		{
			ProjID = ProjectileID.Bat;
			ShaftA = 16 * 20;
			ShaftB = 16 * 30;
			angleVel = Math.PI / 120; 
		}

		private Vector CalcReflectedVelocity(Entity entity)
		{
			Vector relativePos = (Vector)(entity.Center - Center);
			Vector vel = (Vector)entity.velocity;

			vel.Angle -= Rotation;
			relativePos.Angle -= Rotation;

			Vector r = (relativePos.Y, -relativePos.X);

			vel -= r * (float)angleVel;

			double alpha = Math.Atan2(relativePos.X / ShaftA, relativePos.Y / ShaftB);
			Vector u = (ShaftA * Math.Cos(alpha), ShaftB * Math.Sin(alpha));

			vel -= 2 * Vector.Dot(vel, u) * u / u.LengthSquared;

			vel += r * (float)angleVel;
			vel.Angle += Rotation;
			return vel;
		}

	}
}
