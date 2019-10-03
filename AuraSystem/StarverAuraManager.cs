using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using TerrariaApi.Server;
using Terraria.ID;
using TShockAPI;

namespace Starvers.AuraSystem
{
	public class StarverAuraManager : IStarverPlugin
	{
		#region Properties
		public bool Enabled => Config.EnableAura;
		public static StarverConfig Config => StarverConfig.Config;
		public static DateTime Last { get; protected set; } = DateTime.Now;
		public static SkillManager Skill { get; private set; } = new SkillManager();
		public static AuraSkillWeapon[] SkillSlot { get; private set; } = new AuraSkillWeapon[Skills.Base.Skill.MaxSlots]
		{
			new AuraSkillWeapon(ItemID.Spear,ProjectileID.Spear,100),
			new AuraSkillWeapon(ItemID.Trident,ProjectileID.Trident,800),
			new AuraSkillWeapon(ItemID.Swordfish,ProjectileID.Swordfish,4000),
			new AuraSkillWeapon(ItemID.DarkLance,ProjectileID.DarkLance,8000),
			new AuraSkillWeapon(ItemID.ObsidianSwordfish,ProjectileID.ObsidianSwordfish,30000)
		};
		public static string SkillList = "技能列表:";
		#endregion
		#region I & D
		public void Load()
		{
			GetDataHandlers.NewProjectile += OnProj;
			Commands.ChatCommands.Add(new Command(Perms.Aura.Normal, Command, "au", "aura"));
			bool flag;
			SkillList += '\n';
			for (int i = 1; i <= SkillManager.Skills.Length; i++)
			{
				flag = i % 3 == 0;
				SkillList += SkillManager.Skills[i - 1].Name;
				if (flag)
				{
					SkillList += '\n';
				}
				else
				{
					SkillList += "    ";
				}
			}
		}
		public void UnLoad()
		{
			GetDataHandlers.NewProjectile -= OnProj;
		}
		#endregion
		#region OnProj
		private void OnProj(object sender, GetDataHandlers.NewProjectileEventArgs args)
		{
			if (args.Type == ProjectileID.RocketII || args.Type == ProjectileID.RocketIV || args.Type == ProjectileID.RocketSnowmanII || args.Type == ProjectileID.RocketSnowmanIV)
			{
				Main.projectile[args.Index].active = false;
				return;
			}
			if ((!Main.projectile[args.Index].friendly) && Main.projectile[args.Index].damage <= 300)
			{
				Main.projectile[args.Index].damage = (int)(Config.TaskNow / 4D);
				NetMessage.SendData((int)PacketTypes.ProjectileNew, -1, -1, null, args.Index);
				return;
			}
			int slot = SkillManager.GetSlot(args.Type);
			if (slot > 0 && slot < 6 && (DateTime.Now - Starver.Players[args.Owner].LastHandle).TotalSeconds > 1) 
			{
				Starver.Players[args.Owner].LastHandle = DateTime.Now;
				Skill.Handle(Starver.Players[args.Owner], args.Velocity, slot);
			}
		}
		#endregion
		#region Command
		private void Command(CommandArgs args)
		{
			string p = args.Parameters.Count < 1 ? "None" : args.Parameters[0];
			StarverPlayer player = args.SPlayer();
			switch (p.ToLower())
			{
				#region up
				case "up":
					int exp = player.Exp;
					int lvl = player.Level;
					int need = UpGradeExp(lvl);
					if (player.HasPerm(Perms.VIP.LessCost))
					{
						need /= 3;
						while (exp > need)
						{
							exp -= need;
							need = UpGradeExp(++lvl) / 3;
						}
					}
					else
					{
						while (exp > need)
						{
							exp -= need;
							need = UpGradeExp(++lvl);
						}
					}
					player.Level = lvl;
					player.Exp = exp;
					player.Save();
					player.SendInfoMessage("当前等级:{0}", player.Level);
					player.SendInfoMessage("所需经验:{0}", UpGradeExp(player.Level));
					break;
				#endregion
				#region ForceUp
				case "forceup":
					if (!player.HasPerm(Perms.Aura.ForceUp))
					{
						goto default;
					}
					int up;
					try
					{
						up = int.Parse(args.Parameters[1]);
					}
					catch
					{
						up = 1;
					}
					player.Level += up;
					player.SendInfoMessage("当前等级:{0}", player.Level);
					player.Save();
					break;
				#endregion
				#region toexp
				case "toexp":
					Item item = player.TPlayer.inventory[0];
					int bagexp;
					try
					{
						bagexp = item.stack * BagExp(item.type);
						item.netDefaults(0);
						if (bagexp == 0)
						{
							throw new Exception();
						}
						player.Exp += bagexp;
						player.SendData(PacketTypes.PlayerSlot, "", player.Index, 0);
						player.Save();
					}
					catch
					{
						player.SendMessage("请将可兑换为经验的物品(主要为boss袋子)放置在背包第一格", Color.Red);
					}
					break;
				#endregion
				#region Setlvl
				case "setlvl":
					if (!player.HasPerm(Perms.Aura.SetLvl))
					{
						goto default;
					}
					try
					{
						player.Level = int.Parse(args.Parameters[1]);
						player.SendInfoMessage("设置成功");
						player.SendInfoMessage("当前等级:{0}", player.Level);
					}
					catch
					{
						player.SendMessage("正确用法:    /aura setlvl <level>", Color.Red);
					}
					break;
				#endregion
				#region list
				case "list":
					player.SendInfoMessage(SkillList);
					break;
				#endregion
				#region buy
				case "buy":
					try
					{
						need = int.Parse(args.Parameters[1]) - 1;
						if(player.Exp >= SkillSlot[need].Cost)
						{
							player.TSPlayer.GiveItem(SkillSlot[need].Item, "", 0, 0, 1, 0);
							player.Exp -= SkillSlot[need].Cost;
							player.SendMessage("购买成功", new Color(0, 0x80, 0));
						}
						else
						{
							player.SendMessage("你需要更多的经验来获取该技能物品",Color.Red);
						}
					}
					catch
					{
						player.SendMessage("应输入正确的武器序号:{1 ,2, 3, 4, 5}中的一个", Color.Red);
					}
					break;
				#endregion
				#region Set
				case "set":
					int slot;
					if(args.Parameters.Count < 3 || !int.TryParse(args.Parameters[1],out slot))
					{
						player.SendInfoMessage("格式错误");
						player.SendInfoMessage("正确用法:    set <slot> <skilltype>");
					}
					else
					{
						player.SetSkill(args.Parameters[2], slot);
					}
					break;
				#endregion
				#region CDLess
				case "cd":
					if(!player.HasPerm(Perms.Aura.IgnoreCD))
					{
						goto default;
					}
					player.ForceIgnoreCD ^= true;
					player.SendInfoMessage($"ForceIgnoreCD: {player.ForceIgnoreCD}");
					break;
				#endregion
				#region help
				case "help":
					try
					{
						int id = int.Parse(args.Parameters[1]);
						args.Player.SendMessage(SkillManager.Skills[id].Name, Color.Aqua);
						args.Player.SendMessage(SkillManager.Skills[id].Text, Color.Aqua);
					}
					catch
					{
						if(args.Parameters.Count >= 2)
						{
							string sklname = args.Parameters[1].ToLower();
							foreach(var skl in SkillManager.Skills)
							{
								if(skl.Name.ToLower().IndexOf(sklname) == 0)
								{
									args.Player.SendMessage(skl.Name, Color.Aqua);
									args.Player.SendMessage(skl.Text, Color.Aqua);
									return;
								}
							}
							args.Player.SendErrorMessage("技能名称错误");
							args.Player.SendErrorMessage("    list:  查看技能列表");
						}
						goto default;
					}
					break;
				#endregion
				#region default
				default:
					player.SendInfoMessage(HelpTexts.Aura);
					if (player.HasPerm(Perms.Aura.ForceUp))
					{
						player.SendInfoMessage("    forceup <levelup>:强制升级");
					}
					if (player.HasPerm(Perms.Aura.SetLvl))
					{
						player.SendInfoMessage("    setlvl <level>:设置等级");
					}
					break;
					#endregion
			}
		}
		#endregion
		#region Calculates
		#region NPCDefense
		internal static int NPCDefense(int raw)
		{
			float scale = Config.TaskNow + 1;
			return Convert.ToInt32(raw * scale);
		}
		#endregion
		#region NPCDamage
		public static int NPCDamage(int raw)
		{
			if (Config.TaskNow < 18)
			{
				return (int)(raw * Config.TaskNow / 5f + 1);
			}
			else
			{
				return (int)(raw * Config.TaskNow / 3f + 1);
			}
		}
		#endregion
		#region NPCLife
		internal static int NPCLife(int raw, bool isboss = false)
		{
			float scale = 1;
			scale += Config.TaskNow * 2.75f;
			float now = raw * scale;
			if (!isboss)
			{
				now = Math.Min(short.MaxValue, now);
			}
			raw = Convert.ToInt32(now);
			return raw;
		}
		#endregion
		#region UpgradeExp
		internal static int UpGradeExp(int lvl)
		{
			if(lvl < 1000)
			{
				return (int)(lvl * 10.5f);
			}
			else if(lvl < 2500)
			{
				return (int)(lvl * 15.5f);
			}
			else if (lvl < (int)1e4)
			{
				return Math.Min((int)6.25e6f, (int)(lvl * Math.Log(lvl)));
			}
			else if(lvl < (int)1e5)
			{
				return Math.Min((int)5.1e5f, (int)(lvl * 10 * Math.Log(lvl)));
			}
			else
			{
				return (int)1e6f;
			}
		}
		#endregion
		#region BagExp
		internal static int BagExp(int id)
		{
			int exp = 0;
			if (id == ItemID.CultistBossBag)
			{
				exp = (int)2e9;
			}
			if (id >= ItemID.KingSlimeBossBag && id <= ItemID.MoonLordBossBag)
			{
				exp = (id + 11 - ItemID.KingSlimeBossBag);
				exp *= exp;
				exp *= exp;
			}
			else if (id == ItemID.BossBagBetsy)
			{
				exp = BagExp(ItemID.FishronBossBag);
			}
			else if (id >= ItemID.WhiteCultistArcherBanner && id <= ItemID.WhiteCultistFighterBanner)
			{
				id = id - ItemID.WhiteCultistArcherBanner + 2;
				exp = 1000000 * id;
			}
			return exp;
		}
		#endregion
		#endregion
	}
}
