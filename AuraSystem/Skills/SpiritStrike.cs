using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Starvers.AuraSystem.Skills.Base;
using Terraria;
using Terraria.ID;

namespace Starvers.AuraSystem.Skills
{
	public class SpiritStrike:Skill
	{
		protected static int rec = 1200;
		protected static Vector2 DefaultVector = Vector2.Zero;
		protected double d = 0;
		public SpiritStrike():base(SkillIDs.SpiritStrike)
		{
			MP = 35;
			CD = 60 * 20;
			Level = 80;
			Author = "wither";
			Description = "对一定范围内的敌对生物发动攻击";
			SetText();
		}
		public override void Release(StarverPlayer player, Vector2 vel)
		{
			player.NewProj(player.Center, DefaultVector, ProjectileID.StardustGuardianExplosion, 190, 20);
			Thread.Sleep(100);
			for (d = 0; d < 2 * Math.PI; d += Math.PI / 4)
			{
				player.NewProj(player.Center + player.FromPolar(d, 160), DefaultVector, ProjectileID.StardustGuardianExplosion, 150, 5);
			}
			foreach(var npc in Main.npc)
			{
				if(npc.friendly||!npc.active)
				{
					continue;
				}
				if(Vector2.Distance(npc.Center,player.Center) < 16 * 10)
				{
					npc.StrikeNPC(140, 20f, 0, true, false, true, player.TPlayer);
				}
			}
			player.SetBuff(BuffID.ShadowDodge);
		}
	}
}
