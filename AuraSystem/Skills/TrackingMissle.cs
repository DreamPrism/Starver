using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Starvers.AuraSystem.Skills.Base;
using Terraria;
using Terraria.ID;

namespace Starvers.AuraSystem.Skills
{
	public class TrackingMissile : Skill
	{
		protected Vector2 Target;
		protected const float dis = 640;
		protected Vector2 Pos;
		public TrackingMissile() : base(SkillID.TrackingMissile)
		{
			Lvl = 30;
			MP = 20;
			CD = 30;
			Description = "制造若干个射向最近敌人位置的导弹";
			Author = "Deaths";
			SetText();
		}
		public override void Release(StarverPlayer player, Vector2 vel)
		{
			Target = player.Center + vel;
			foreach (NPC npc in Main.npc)
			{
				if (npc.friendly)
				{
					continue;
				}
				if ((npc.Center - player.TPlayer.Center).Length() < dis)
				{
					Target = npc.Center;
					break;
				}
			}
			for (int i = 0; i < 10; i++)
			{
				Pos = Target +  player.NewByPolar(Rand.NextAngle(), dis);
				player.NewProj(Pos, 3 * Vector2.Normalize(Target - Pos), ProjectileID.VortexBeaterRocket, player.Level / 10 + 10, 0);
			}
		}
	}
}
