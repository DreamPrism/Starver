using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers.TaskSystem.Branches
{
	public partial class YrtAEvah : BranchLine
	{
		private Task[] tasks;

		public override int Count => 6;

		public override BranchTask this[int index] => tasks[index];

		public YrtAEvah() : base(BLID.YrtAEvah)
		{
			tasks = new Task[Count];
			for (int i = 0; i < tasks.Length; i++)
			{
				tasks[i] = new Task(i);
			}
		}

		public override (bool success, string msg) TryStartTask(StarverPlayer player, int index)
		{
			var result = tasks[index].CanStartTask(player);
			if(result.success)
			{
				var task = new Task(index, player);
				task.SetDefault();
				task.Start();
				player.BranchTask = task;
			}
			return result;
		}
	}
}
