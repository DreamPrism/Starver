using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers.AuraSystem.Skills
{
	using Base;
	using Microsoft.Xna.Framework;
    using Terraria.ID;
    using Vector = TOFOUT.Terraria.Server.Vector2;
	public class UltimateSlash : Skill
	{
		//打算让UltimateBlast直接继承这个,省点事,到时候直接重写AsyncRelease
		public UltimateSlash(SkillIDs ID = SkillIDs.UltimateSlash) : base(ID)
		{
			CD = 10 * 60;
			MP = 6000;
			Level = 40000;
			ForceCD = true;
			Author = "zhou_Qi";
			Description = @"""我们对此一无所知""
""蕴含着最终的力量""";
		}
		public override void Release(StarverPlayer player, Vector2 vel)
		{

		}
		protected virtual async void AsyncRelease(StarverPlayer player)
		{
			await Task.Run(() =>
			{
				player.SetBuff(BuffID.ShadowDodge, 60 * 3);
				player.SetBuff(BuffID.Webbed, 60 * 3);
			});
		}
	}
}
