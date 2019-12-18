using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers.TaskSystem
{
	public class MainLineTaskData
	{
		public Dictionary<TaskDifficulty, TaskItem[]> Needs { get; set; }
		public Dictionary<TaskDifficulty, TaskItem[]> Rewards { get; set; }
		public string Name { get; set; }
		public string Story { get; set; }
		public int LevelReward { get; set; }
	}
}
