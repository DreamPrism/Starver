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
	public class TrackingMissile : Skill
	{
		protected static int Target;
		protected const float dis = 16 * 100;
		protected Vector2 Pos;
		public TrackingMissile() : base(SkillIDs.TrackingMissile)
		{
			Level = 30;
			MP = 20;
			CD = 60 * 20;
			Description = "制造若干个射向最近敌人位置的导弹";
			Author = "Deaths";
			SetText();
		}
		public override void Release(StarverPlayer player, Vector2 vel)
		{
			Target = -1;
			foreach (NPC npc in Main.npc)
			{
				if (npc.friendly)
				{
					continue;
				}
				if ((npc.Center - player.TPlayer.Center).Length() < dis)
				{
					Target = npc.whoAmI;
					break;
				}
			}
			new Thread(RealSkill).Start(player);
		}
		protected static void RealSkill(object ply)
		{
			StarverPlayer player = ply as StarverPlayer;
			int t = 0;
			while (t++ < 6)
			{
				Thread.Sleep(1000);
				if (Target != -1 && Main.npc[Target].active)
				{
					player.ProjCircle(Main.npc[Target].Center, 16 * 25, 9, ProjectileID.VortexBeaterRocket, player.Level > 200 ? 8 : 16, 80);
				}
				else
				{
					player.ProjCircle(player.Center, 16 * 25, 9, ProjectileID.VortexBeaterRocket, player.Level > 200 ? 8 : 16, 80);
				}
			}
		}
	}
}
