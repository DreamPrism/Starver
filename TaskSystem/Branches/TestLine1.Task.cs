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
	public partial class TestLine1
	{
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

			public override void Start()
			{
				base.Start();
				TargetPlayer.SendInfoMessage(Description);
			}

			public (bool Success, string Message) CanStartTask(StarverPlayer player)
			{
				switch (ID)
				{
					case 0:
					case 1:
					case 2:
						{
							if (!player.HasItem(WeaponRequire))
							{
								return (false, StartFailResons.HaveNoPaladinsHammer);
							}
							return (true, null);
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
							Description = @$"使用圣骑士之锤击杀{data.RequireCount}个敌人";
							break;
						}
					case 1:
						{
							NPCStriked = StrikedNPC_1;
							data.RequireCount = 10;
							Description = @$"使用圣骑士之锤击杀{data.RequireCount}个敌人";
							break;
						}
					case 2:
						{
							NPCStriked = StrikedNPC_2;
							data.RequireCount = 15;
							Description = @$"使用圣骑士之锤击杀{data.RequireCount}个敌人";
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
					TargetPlayer.SendCombatMSsg($"击杀量: {++data.KillCount} / {data.RequireCount}", Color.Beige);
					if (data.KillCount >= data.RequireCount)
					{
						TargetPlayer.BranchTaskEnd(true);
					}
				}
			}
			private void StrikedNPC_1(NPCStrikeEventArgs args)
			{
				if (TargetPlayer.HeldItem.type == WeaponRequire && args.KilledNPC)
				{
					TargetPlayer.SendCombatMSsg($"击杀量: {++data.KillCount} / {data.RequireCount}", Color.Beige);
					if (data.KillCount >= data.RequireCount)
					{
						TargetPlayer.BranchTaskEnd(true);
					}
				}
			}
			private void StrikedNPC_2(NPCStrikeEventArgs args)
			{
				if (TargetPlayer.HeldItem.type == WeaponRequire && args.KilledNPC)
				{
					TargetPlayer.SendCombatMSsg($"击杀量: {++data.KillCount} / {data.RequireCount}", Color.Beige);
					if (data.KillCount >= data.RequireCount)
					{
						TargetPlayer.BranchTaskEnd(true);
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
			[type: StructLayout(LayoutKind.Explicit, Size = 16)]
			private struct TaskData
			{
				#region ByteValues
				[field: FieldOffset(sizeof(byte) * 0)]
				private byte ByteValue0;
				[field: FieldOffset(sizeof(byte) * 1)]
				private byte ByteValue1;
				[field: FieldOffset(sizeof(byte) * 2)]
				private byte ByteValue2;
				[field: FieldOffset(sizeof(byte) * 3)]
				private byte ByteValue3;
				[field: FieldOffset(sizeof(byte) * 4)]
				private byte ByteValue4;
				[field: FieldOffset(sizeof(byte) * 5)]
				private byte ByteValue5;
				[field: FieldOffset(sizeof(byte) * 6)]
				private byte ByteValue6;
				[field: FieldOffset(sizeof(byte) * 7)]
				private byte ByteValue7;
				[field: FieldOffset(sizeof(byte) * 8)]
				private byte ByteValue8;
				[field: FieldOffset(sizeof(byte) * 9)]
				private byte ByteValue9;
				[field: FieldOffset(sizeof(byte) * 10)]
				private byte ByteValue10;
				[field: FieldOffset(sizeof(byte) * 11)]
				private byte ByteValue11;
				[field: FieldOffset(sizeof(byte) * 12)]
				private byte ByteValue12;
				[field: FieldOffset(sizeof(byte) * 13)]
				private byte ByteValue13;
				[field: FieldOffset(sizeof(byte) * 14)]
				private byte ByteValue14;
				[field: FieldOffset(sizeof(byte) * 15)]
				private byte ByteValue15;
				#endregion
				#region IntValues
				[field: FieldOffset(sizeof(int) * 0)]
				private int IntValue0;
				[field: FieldOffset(sizeof(int) * 1)]
				private int IntValue1;
				[field: FieldOffset(sizeof(int) * 2)]
				private int IntValue2;
				[field: FieldOffset(sizeof(int) * 3)]
				private int IntValue3;
				#endregion
				#region FloatValues
				[field: FieldOffset(sizeof(float) * 0)]
				private float FloatValue0;
				[field: FieldOffset(sizeof(float) * 1)]
				private float FloatValue1;
				[field: FieldOffset(sizeof(float) * 2)]
				private float FloatValue2;
				[field: FieldOffset(sizeof(float) * 3)]
				private float FloatValue3;
				#endregion
				#region DoubleValues
				[field: FieldOffset(sizeof(double) * 0)]
				private double DoubleValue0;
				[field: FieldOffset(sizeof(double) * 1)]
				private double DoubleValue1;
				#endregion
				#region DecimalValues
				[field: FieldOffset(sizeof(decimal) * 0)]
				private decimal DecimalValue0;
				#endregion

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
