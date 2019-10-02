using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Starvers.AuraSystem.Skills;
using Starvers.AuraSystem.Skills.Base;
using TShockAPI;

namespace Starvers.AuraSystem
{
	public class SkillManager
	{
		#region Skills
		public static Skill[] Skills { get; private set; } = new Skill[]
		{
			new Avalon(),
			new ExCalibur(),
			new FireBall(),
			new Musket(),
			new GaeBolg(),
			new EnderWand(),
			new WindRealm(),
			new Whirlwind(),
			new Shuriken(),
			new SpiritStrike(),
			new AvalonGradation(),
			new LawAias(),
			new LimitlessSpark(),
			new MagnetStorm(),
			new FlameBurning(),
			new JusticeFromSky(),
			new TrackingMissile(),
			new PosionFog(),
			new CDLess(),
			new NStrike(),
			new Sacrifice(),
			new TheWorld(),
			new MirrorMana(),
			new Chaos(),
			new Cosmos(),
			new ChordMana(),
			new FromHell(),
			new StarEruption(),
			new NatureRage(),
			new NatureGuard(),
			new FishingRod(),
			new AlcoholFeast(),
			new GreenWind(),
			new FrozenCraze(),
			new LimitBreak(),
			new MiracleMana()
		};
		#endregion
		#region Handle
		public unsafe void Handle(StarverPlayer player,Vector2 vel,int slot)
		{
			try
			{
				slot -= 1;
				Skill skill = Skills[player.Skills[slot]];
				//flag = Boss.AnyActive;
				if (skill.BossBan && BossSystem.Bosses.Base.StarverBoss.AliveBoss > 0)
				{
					player.SendCombatMSsg("该技能已被禁用", Color.Pink);
				}
				else if (skill.MP > player.MP)
				{
					player.SendCombatMSsg("MP不足", Color.Pink);
				}
				else if ((player.IgnoreCD == false || skill.ForceCD) && player.CDs[slot] > 0)
				{
					player.SendCombatMSsg("技能冷却中", Color.Pink);
				}
				else
				{
					skill.Release(player, vel);
					if (skill.ForceCD || !player.IgnoreCD)
					{
						player.CDs[slot] += skill.CD;
					}
					player.MP -= skill.MP;
					player.LastSkill = skill.Index;
				}
				player.MP = Math.Max(0, Math.Min(player.MP, player.MaxMP));
			}
			catch(Exception e)
			{
				TSPlayer.Server.SendErrorMessage(e.ToString());
				player.SendMessage($"技能使用失败, 原因请查询日志", Color.Red);
				TShock.Log.Info(e.ToString());
			}
		}
		#endregion
		#region GetSlot
		public static int GetSlotByItemID(int type)
		{
			for (int i = 0; i < StarverAuraManager.SkillSlot.Length; i++)
			{
				if (type == StarverAuraManager.SkillSlot[i].Item)
				{
					return i + 1;
				}
			}
			return 0;
		}
		public static int GetSlot(int type)
		{
			for (int i = 0; i < StarverAuraManager.SkillSlot.Length; i++)
			{
				if (type == StarverAuraManager.SkillSlot[i].Proj)
				{
					return i + 1;
				}
			}
			return 0;
		}
		#endregion
	}
}
