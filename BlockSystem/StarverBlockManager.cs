using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers.BlockSystem
{
	public class StarverBlockManager : IStarverPlugin
	{
		#region Variables
		public StarverConfig Config => StarverConfig.Config;
		#endregion
		#region Interface
		public bool Enabled => false;
		public void Load()
		{

		}
		public void UnLoad()
		{

		}
		#endregion
	}
}
