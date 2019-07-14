using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Starvers.AuraSystem.Skills.Base;
using Terraria.ID;

namespace Starvers.AuraSystem.Skills
{
	public class LawAias : Skill
	{
		public LawAias() : base(SkillID.LawAias)
		{
			MP = 300;
			CD = 120;
			BossBan = true;
			Lvl = 100;
			Author = "三叶草";
			Description = "这是三叶草没做好的技能,\n但是1413完成这个技能的同时偏离了三叶草的原版设计意图\n以释放者为圆心制造一个由弹幕组成的逐渐缩小的圆";
			SetText();
		}
		public override void Release(StarverPlayer player, Vector2 vel)
		{
			int proj = ProjectileID.DemonScythe;
			if(player.Level > 1000)
			{
				proj = ProjectileID.Typhoon;
			}
			if(player.Level > 2000)
			{
				proj = ProjectileID.NebulaArcanum;
			}
			player.ProjCircle(player.Center, 16 * 40, 15, proj, 25,200);
		}
	}
}
