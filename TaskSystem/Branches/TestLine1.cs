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
	public class TestLine1 : BranchLine
	{
		private const int taskCount = 3;

		private const int WeaponRequire = ItemID.PaladinsHammer;

		private Task[] tasks;

		public TestLine1() : base(BranchLines.TestLine1)
		{
			tasks = new Task[taskCount];
			for (int i = 0; i < tasks.Length; i++)
			{
				tasks[i] = new Task(i);
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

		public override (bool Started, string Message) TryStartTask(StarverPlayer player, int index)
		{
			string msg = tasks[index].CanStartTask(player);
			if (msg is null)
			{
				var task = new Task(index, player);
				task.SetDefault();
				task.Start();
				player.BranchTask = task;
				return (true, msg);
			}
			return (false, msg);
		}

		private class Task : BranchTask
		{
			private Action<NPCStrikeEventArgs> NPCStriked;
			private TaskData data;

			public int? ID { get; }
			public override BranchLines WhichLine => BranchLines.TestLine1;
			public Task(int? id, StarverPlayer player = null) : base(player)
			{
				ID = id;
			}

			public override void StrikedNPC(NPCStrikeEventArgs args)
			{
				base.StrikedNPC(args);
				NPCStriked(args);
			}

			public string CanStartTask(StarverPlayer player)
			{
				switch (ID)
				{
					case 0:
					case 1:
					case 2:
						{
							if (!player.HasItem(WeaponRequire))
							{
								return StartFailResons.HaveNoPaladinsHammer;
							}
							return null;
						}
					default:
						throw new InvalidOperationException("空任务");
				}
			}

			public void SetDefault()
			{
				switch (ID)
				{
					case 0:
						{
							NPCStriked = StrikedNPC_0;
							data.RequireCount = 5;
							break;
						}
					case 1:
						{
							NPCStriked = StrikedNPC_1;
							data.RequireCount = 10;
							break;
						}
					case 2:
						{
							NPCStriked = StrikedNPC_2;
							data.RequireCount = 15;
							break;
						}
					default:
						throw new InvalidOperationException("空任务");
				}
			}

			public override string ToString()
			{
				return $"TestLine1--{ID + 1}";
			}

			#region NPCStrikeds
			private void StrikedNPC_0(NPCStrikeEventArgs args)
			{
				if (TargetPlayer.HeldItem.type == WeaponRequire && args.KilledNPC)
				{
					if (data.KillCount++ >= data.RequireCount)
					{
						TargetPlayer.BranchTaskEnd(true);
					}
					else
					{
						TargetPlayer.SendCombatMSsg($"击杀量: {data.KillCount} / {data.RequireCount}", Color.Beige);
					}
				}
			}
			private void StrikedNPC_1(NPCStrikeEventArgs args)
			{
				if (TargetPlayer.HeldItem.type == WeaponRequire && args.KilledNPC)
				{
					if (data.KillCount++ >= data.RequireCount)
					{
						TargetPlayer.BranchTaskEnd(true);
					}
					else
					{
						TargetPlayer.SendCombatMSsg($"击杀量: {data.KillCount} / {data.RequireCount}", Color.Beige);
					}
				}
			}
			private void StrikedNPC_2(NPCStrikeEventArgs args)
			{
				if (TargetPlayer.HeldItem.type == WeaponRequire && args.KilledNPC)
				{
					if (data.KillCount++ >= data.RequireCount)
					{
						TargetPlayer.BranchTaskEnd(true);
					}
					else
					{
						TargetPlayer.SendCombatMSsg($"击杀量: {data.KillCount} / {data.RequireCount}", Color.Beige);
					}
				}
			}
			#endregion
			#region Fails
			private static class StartFailResons
			{
				public const string HaveNoPaladinsHammer = "你需要一个圣骑士之锤";
			}
			#endregion
			#region TaskData
			private struct TaskData
			{
				private int IntValue0;
				private int IntValue1;
				private int IntValue2;
				private int IntValue3;
				public int KillCount
				{
					get => IntValue0;
					set => IntValue0 = value;
				}
				public int RequireCount
				{
					get => IntValue1;
					set => IntValue1 = value;
				}
			}
			#endregion
		}
	}
}
