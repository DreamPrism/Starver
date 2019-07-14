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
	public class FireBall : Skill
	{
		public FireBall() : base(SkillID.FireBall)
		{
			MP = 10;
			CD = 2;
			Author = "三叶草";
			Lvl = 5;
			Description = "发射若干个小火球";
			SetText();
		}
		public override void Release(StarverPlayer player, Vector2 vel)
		{
			for (int i = 0; i < 5; i++)
			{
				player.NewProj(player.Center,  player.NewByPolar(vel.Angle() + Rand.NextDouble(0, Math.PI / 4), 19f), ProjectileID.MolotovFire, (int)(20 * Math.Log(player.Level)), 2f);
			}
		}
	}
}
