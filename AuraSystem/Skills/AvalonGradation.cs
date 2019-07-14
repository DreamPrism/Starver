using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Starvers.AuraSystem.Skills.Base;
using Terraria;
using TShockAPI;

namespace Starvers.AuraSystem.Skills
{
	public class AvalonGradation:Skill
	{
		public const float R = 16 * 80;
		public AvalonGradation():base(SkillID.AvalonGradation)
		{
			CD = 60;
			MP = 180;
			Lvl = 1000;
			Author = "1413";
			Description = "消除你身边的所有敌对弹幕,持续10s";
			SetText();
		}
		public override void Release(StarverPlayer player, Vector2 vel)
		{
			player.SendMessage("你得到了来自幻想乡的庇护", Color.Aqua);
			new Thread(() =>
			{
				DateTime Start = DateTime.Now;
				Start:
				foreach (Projectile proj in Main.projectile)
				{
					if (proj == null || !proj.active)
					{
						continue;
					}
					if(proj.friendly == false && Vector2.Distance(player.Center,proj.Center) < R)
					{
						proj.active = false;
						TSPlayer.All.SendData(PacketTypes.ProjectileNew, "", proj.whoAmI);
					}
				}
				if((DateTime.Now-Start).TotalSeconds > 10)
				{
					return;
				}
				Thread.Sleep(100);
				goto Start;
			}).Start();
		}
	}
}
