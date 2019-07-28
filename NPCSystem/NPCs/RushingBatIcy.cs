using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers.NPCSystem.NPCs
{
	public class RushingBatIcy : RushingBat
	{
		#region ctor
		public RushingBatIcy()
		{
			Checker.Biome |= BiomeType.Icy;
		}
		#endregion
	}
}
