using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace Starvers.AuraSystem.Realms.Generics
{
	public interface IRealmReflector : IRealmConditioner
	{
		public bool AtBorder(Entity entity);
		public void Reflect(Entity entity);
	}
}
