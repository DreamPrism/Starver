using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers.AuraSystem.Realms.Conditioners
{
	using Interfaces;
	using Microsoft.Xna.Framework;
	using Terraria;
	using Vector = TOFOUT.Terraria.Server.Vector2;
	public class CircleReflector : CircleConditioner, IRealmReflector
	{
		public void Reflect(Entity entity)
		{
			Vector2 Distance = entity.Center - Center;
			entity.velocity -= 2 * Distance * Vector2.Dot(Distance, entity.velocity) / (Radium * Radium);
		}
	}
}
