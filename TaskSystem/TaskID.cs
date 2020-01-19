using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers.TaskSystem
{
	/// <summary>
	/// 记录某个boss的任务ID(刚好正在做这个任务时)
	/// </summary>
	public static class TaskID
	{
		public static int KingSlime { get; } = 1;
		public static int Eye { get; } = 2;
		public static int SkeletronEx { get; } = 25;
		public static int MoonLord { get; } = 21;
	}
}
