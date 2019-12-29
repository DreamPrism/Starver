
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TShockAPI;

namespace Starvers.WeaponSystem
{
	using Weapons;
	using Weapons.Ranged;
	using Weapons.Magic;
	using Weapons.Melee;
	using Vector = TOFOUT.Terraria.Server.Vector2;
	public class StarverWeaponManager : IStarverPlugin
	{
		#region Fields
		private static Weapon[] Melee =
		{
			new NorthPoleEx(),
			new TerraBladeEx(),
			new MushroomSpearEx()
		};
		private static Weapon[] Ranged =
		{
			new VortexBeaterEx(),
			new PhantasmEx(),
			new PhantomPhoenix(),
			new XenopopperEx()
		};
		private static Weapon[] Magic =
		{
			new NebulaArcanumEx(),
			new NebulaBlazeEx(),
			new LastPrismEx(),
			new LaserMachinegunEx()
		};
		private static Weapon[] Minion =
		{

		};
		private static Weapon[][] WeaponList =
		{
			Melee,
			Ranged,
			Magic,
			Minion
		};
		#endregion
		#region Iterfaces
		public StarverConfig Config => StarverConfig.Config;
		public bool Enabled => Config.EnableAura;
		public void Load()
		{
			GetDataHandlers.NewProjectile += OnProj;
			Currency.Initialize();
			Commands.ChatCommands.Add(new TShockAPI.Command(Perms.Normal, Command, "weapon", "wp"));
		}
		public void UnLoad()
		{
			GetDataHandlers.NewProjectile -= OnProj;
			Commands.ChatCommands.RemoveAll(cmd => cmd.HasAlias("wp"));
		}
		#endregion
		#region Command
		private void Command(CommandArgs args)
		{
			if (args.Parameters.Count < 1)
			{
				args.Player.SendErrorMessage(HelpTexts.Weapon);
				return;
			}
			else
			{
				Weapon[] weapons = null;
				try
				{
					weapons = WeaponList[int.Parse(args.Parameters[0])];
				}
				catch
				{
					string str = args.Parameters[0].ToLower();
					if ("melee".IndexOf(str) == 0)
					{
						weapons = WeaponList[CareerType.Melee];
					}
					else if ("ranged".IndexOf(str) == 0)
					{
						weapons = WeaponList[CareerType.Ranged];
					}
					else if ("magic".IndexOf(str) == 0)
					{
						weapons = WeaponList[CareerType.Magic];
					}
					else if ("minion".IndexOf(str) == 0)
					{
						weapons = WeaponList[CareerType.Minion];
					}
				}
				if (args.Parameters.Count < 2)
				{
					if (weapons.Length < 1)
					{
						args.Player.SendInfoMessage("该类型武器尚未制作");
					}
					else
					{
						foreach (var weapon in weapons)
						{
							args.Player.SendInfoMessage(weapon.Name);
						}
					}
					return;
				}
				else
				{
					if (weapons.Length < 1)
					{
						args.Player.SendInfoMessage("该类型武器尚未制作");
					}
					else
					{
						Weapon weapon = null;
						string read = args.Parameters[1].ToLower();
						try
						{
							weapon = weapons[int.Parse(read)];
						}
						catch
						{
							foreach (var wp in weapons)
							{
								if (wp.Name.ToLower().IndexOf(read) == 0)
								{
									weapon = wp;
									break;
								}
							}
						}
						weapon.UPGrade(args.SPlayer());
					}
					return;
				}
			}
		}
		#endregion
		#region Hooks
		private void OnProj(object sender, GetDataHandlers.NewProjectileEventArgs args)
		{
			if (args.Owner >= Starver.Players.Length || !Terraria.Main.projectile[args.Index].friendly)
			{
				return;
			}
			Weapon[] weapons;
			if (Terraria.Main.projectile[args.Index].melee)
			{
				weapons = WeaponList[CareerType.Melee];
			}
			else if (Terraria.Main.projectile[args.Index].ranged)
			{
				weapons = WeaponList[CareerType.Ranged];
			}
			else if (Terraria.Main.projectile[args.Index].magic)
			{
				weapons = WeaponList[CareerType.Magic];
			}
			else
			{
				weapons = WeaponList[CareerType.Minion];
			}
			foreach (var weapon in weapons)
			{
				if (weapon.Check(args))
				{
					if (Starver.Players[args.Owner].HasWeapon(weapon))
					{
						weapon.UseWeapon(Starver.Players[args.Owner], (Vector)args.Velocity, Starver.Players[args.Owner].Weapon[weapon.Career, weapon.Index], args);
					}
					break;
				}
			}
		}
		private delegate void WeaponDelegate(StarverPlayer player, Vector Velocity, TShockAPI.GetDataHandlers.NewProjectileEventArgs args);
		#endregion
	}
}
