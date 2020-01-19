using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers.TaskSystem
{
	public static class Ext
	{
		public static BLFlags ToBLFLags(this BLID id)
		{
			if(id == BLID.None)
			{
				return BLFlags.None;
			}
			if(id >= BLID.Max)
			{
				throw new ArgumentException("Invalid BLID", nameof(id));
			}
			return (BLFlags)(1 << (int)(id - 1));
		}
	}
}
