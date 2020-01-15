using System;



namespace Starvers.TaskSystem.Branches
{
	using Color = Microsoft.Xna.Framework.Color;
	public partial class YrtAEvah
	{
		private class Task : BranchTask
		{
			private string[] startMsgs;
			private int msgInterval;
			private int t;
			public int? ID { get; }
			public override BLID BLID => BLID.YrtAEvah;

			public Task(int? id, StarverPlayer player = null) : base(player)
			{
				ID = id;
			}

			public void SetDefault()
			{
				msgInterval = 60 * 3 / 2;
				switch(ID)
				{
					case 0:
						{
							name = "证明自己";
							startMsgs = new[]
							{
								"你需要先向我证明你自己的实力",
								"先去干掉5只蓝色史莱姆",
								"然后再来找我"
							};
							break;
						}
					case 1:
						{
							name = "睡个好觉";
							startMsgs = new[]
							{
								"这几天晚上总是有僵尸来打扰我睡觉",
								"你去替我好好收拾下它们",
								"[c/008800:(你需要消灭10只僵尸)]"
							};
							break;
						}
					case 2:
						{
							name = "看星星";
							startMsgs = new[]
							{
								"你知道, 对于一个法师来说, ⭐是必需品",
								"⭐非常有用, 它可以用来制作魔力药水, 魔能药水, 魔力腰带...",
								"今晚的天气很晴朗, 一定会有很多落星",
								"帮我收集尽可能多星星"
							};
							break;
						}
					case 3:
					case 4:
					case 5:
					default:
						throw new InvalidOperationException("空任务");
				}
			}

			public (bool success, string msg) CanStartTask(StarverPlayer player)
			{
				throw new NotImplementedException();
			}

			public override void Updating(int Timer)
			{
				base.Updating(Timer);
				if(t < startMsgs.Length && Timer % msgInterval == 0)
				{
					TargetPlayer.SendMessage(startMsgs[t++], new Color(255, 233, 233));
				}
			}
		}
	}
}
