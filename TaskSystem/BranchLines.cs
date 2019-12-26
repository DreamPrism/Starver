using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers.TaskSystem
{
	/// <summary>
	/// 表示是支线当中的哪一部分
	/// </summary>
	[Flags]
	public enum BranchLines
	{
		None,
		/// <summary>
		/// 测试: 指定武器击杀NPC
		/// </summary>
		TestLine1
	}
}
