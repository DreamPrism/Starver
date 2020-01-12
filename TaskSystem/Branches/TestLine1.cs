using Microsoft.Xna.Framework;
using Starvers.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Terraria.ID;

namespace Starvers.TaskSystem.Branches
{
	public partial class TestLine1 : BranchLine
	{
		private const int taskCount = 3;

		private const int WeaponRequire = ItemID.PaladinsHammer;

		private Task[] tasks;

		public TestLine1() : base(BLFlags.TestLine1)
		{
			tasks = new Task[taskCount];
			for (int i = 0; i < tasks.Length; i++)
			{
				tasks[i] = new Task(i);
				tasks[i].SetDefault();
			}
		}

		public override int Count
		{
			get
			{
				return tasks.Length;
			}
		}
		public override BranchTask this[int index]
		{
			get
			{
				return tasks[index];
			}
		}

		public override (bool Success, string Message) TryStartTask(StarverPlayer player, int index)
		{
			var result = tasks[index].CanStartTask(player);
			if (result.Success)
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
