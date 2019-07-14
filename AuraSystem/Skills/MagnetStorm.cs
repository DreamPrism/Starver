using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Starvers.AuraSystem.Skills.Base;
using Terraria.ID;

namespace Starvers.AuraSystem.Skills
{
	public class MagnetStorm : Skill
	{
		public MagnetStorm() : base(SkillID.MagnetStorm)
		{
			CD = 15;
			MP = 120;
			Author = "1413";
			Description = "制造若干个击向周围的磁球";
			Lvl = 155;
			SetText();
		}
		public override void Release(StarverPlayer player, Vector2 vel)
		{
			for (double d = 0; d < 2 * Math.PI; d += Math.PI / 9)
			{
				player.NewProj(player.Center,  player.NewByPolar(d, 9f), ProjectileID.MagnetSphereBolt, 182, 0);
				Thread.Sleep(10);
			}
		}
	}
}
