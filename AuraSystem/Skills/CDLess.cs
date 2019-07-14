using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Starvers.AuraSystem.Skills.Base;

namespace Starvers.AuraSystem.Skills
{
	public class CDLess : Skill
	{
		public CDLess() : base(SkillID.CDLess)
		{
			Author = "1413";
			Description = "技能CD太长了？来试试他吧!\n5s内其他技能无CD";
			CD = 360;
			MP = 20;
			Lvl = 500;
			SetText();
		}
		public override void Release(StarverPlayer player, Vector2 vel)
		{
			player.IgnoreCD = true;
			new Thread(() =>
			{
				Thread.Sleep(5000);
				player.IgnoreCD = false;
			}).Start();
		}
	}
}
