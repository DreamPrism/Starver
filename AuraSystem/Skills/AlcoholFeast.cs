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

	public class AlcoholFeast : Skill
	{
		private int[] Projs =
		{
			ProjectileID.MolotovCocktail,
			ProjectileID.MolotovCocktail,
			ProjectileID.MolotovCocktail,
			ProjectileID.MolotovCocktail,
			ProjectileID.Ale,
			ProjectileID.Ale,
			ProjectileID.Ale,
			ProjectileID.Ale,
			ProjectileID.Ale,
		};
		public AlcoholFeast() : base(SkillIDs.AlcoholFeast)
		{
			CD = 60 * 20;
			MP = 35;
			Level = 5;
			Author = "zhou_Qi";
			Description = @"抛射麦芽酒与鸡尾酒，获得醉酒与饱腹效果
""即使是怪物的体魄也无法承受如此强烈的酒精冲击""";
			SetText();
		}
		public override void Release(StarverPlayer player, Vector2 vel)
		{
			int idx;
			for (int i = 0; i < 20; i++)
			{
				idx = player.NewProj(player.Center + Rand.NextVector2(16 * 3, 16 * 3), vel * 3.5f, Projs.Next(), 25 + (int)(80 * Math.Log(player.Level)), 10f);
			}
		}
	}
}
