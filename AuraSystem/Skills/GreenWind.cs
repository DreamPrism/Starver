﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers.AuraSystem.Skills
{
	using Base;
	using Microsoft.Xna.Framework;
	using Terraria;
	using Terraria.ID;
	using Vector = TOFOUT.Terraria.Server.Vector2;
	public class GreenWind : Skill
	{
		private const float Radium = 16 * 90;
		public GreenWind() : base(SkillIDs.GreenWind)
		{
			CD = 60 * 20;
			MP = 150;
			Level = 750;
			Author = "zhou_Qi";
			Description = @"向视野中的敌人发射孢子子弹
""子弹拥有着思维！这很奇怪，不是吗？""
""我们需要去同情子弹在击中目标时所感受到的粉身碎骨的痛苦吗""？";
			SetText();
		}
		public override void Release(StarverPlayer player, Vector2 vel)
		{
			foreach (var npc in Main.npc)
			{
				if (npc.active && Vector2.Distance(player.Center, npc.Center) < Radium)
				{
					LaunchTo(player, npc);
				}
			}
		}
		private void LaunchTo(StarverPlayer player,NPC target)
		{
			Vector velocity = (Vector)(target.Center - player.Center);
			velocity.Length = 17.5f;
			Vector vertical = velocity.Vertical();
			vertical.Length = 16 * 2.5f;
			int damage = (int)(100 * (1 + Math.Log(player.Level)));
			player.ProjLine(player.Center + vertical, player.Center - vertical, velocity, 5, damage, ProjectileID.ChlorophyteBullet);
		}
	}
}
