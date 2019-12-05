using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace Starvers.AuraSystem.Realms.Interfaces
{
	public interface IRealmReflector : IBorderConditioner
	{
		public void Reflect(Entity entity);
	}
}
