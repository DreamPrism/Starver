using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers.AuraSystem.Skills
{
	using Base;
	using Microsoft.Xna.Framework;

	public class LimitBreak : Skill
	{
		public LimitBreak() : base(SkillIDs.LimitBreak)
		{
			CD = 60 * 30;
			Level = 20;
			MP = 80;
			Author = "zhou_Qi";
			Description = @"获得一个向上的足以直达太空的速度
""七点九""
""与传送门搭配效果更佳""";
			SetText();
		}
		public override void Release(StarverPlayer player, Vector2 vel)
		{
			player.Velocity += new Vector2(0, -170);
		}
	}
}
