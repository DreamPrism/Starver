using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers.TaskSystem
{
	public abstract class BranchLine
	{
		protected BranchLine(BranchLines whichLine)
		{
			WhichLine = whichLine;
		}

		/// <summary>
		/// 是那条支线?
		/// </summary>
		public BranchLines WhichLine { get; }
		/// <summary>
		/// 该支线的任务数量
		/// </summary>
		public abstract int Count { get; }
		/// <summary>
		/// 获取该支线的第index个任务
		/// </summary>
		/// <param name="index">任务序号 [0, Count) </param>
		/// <returns></returns>
		public abstract BranchTask this[int index] { get; }
		/// <summary>
		/// 尝试开始该支线的第index个任务
		/// </summary>
		/// <param name="player">试图开始任务的玩家</param>
		/// <param name="index">任务序号 [0, <see cref="Count"/>) </param>
		/// <returns>result.Started: 是否成功开始任务 Message: 有关消息(可能包含任务不能开启的原因)</returns>
		public abstract (bool Started, string Message) TryStartTask(StarverPlayer player, int index);
	}
}
