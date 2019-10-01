using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Starvers.AuraSystem.Skills.Base;
using TShockAPI;

namespace Starvers.AuraSystem.Skills
{
	public class EnderWand:Skill
	{
		public EnderWand():base(SkillIDs.EnderWand)
		{
			MP = 20;
			CD = 1;
			Level = 100;
			Author = "三叶草";
			Description = "跑路专用,往常长矛的方向高速移动\n还可以拿来闯7778机关";
			SetText();
		}
		public override void Release(StarverPlayer player, Vector2 vel)
		{
			player.Velocity += vel * 4.25f;
			TSPlayer.All.SendData(PacketTypes.PlayerUpdate, "", player.Index);
		}
	}
}
