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
	public class Avalon : Skill
	{
		public Avalon() : base(SkillID.Avalon)
		{
			MP = 30;
			CD = 30;
			Description = "幻想乡，这个技能可以给予你5s的伪无敌,\n随后附加多种回血buff,苟命专用";
			Author = "三叶草";
			Lvl = 10;
			SetText();
		}
		public override void Release(StarverPlayer player, Vector2 vel)
		{
			player.GodMode = true;
			player.Heal();
			AsyncRelease(player);
		}
		private async void AsyncRelease(StarverPlayer player)
		{
			await Task.Run(() =>
			{
				Thread.Sleep(5000);
				player.GodMode = false;
				player.SetBuff(62, 10 * 60);
				player.SetBuff(58, 10 * 60);
				player.SetBuff(176, 10 * 60);
				player.SetBuff(173, 10 * 60);
			});
		}
	}
}
