using Starvers.WeaponSystem.Weapons;
using Starvers.WeaponSystem.Weapons.Ranged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers.WeaponSystem
{
	using Vector = TOFOUT.Terraria.Server.Vector2;
	public class StarverWeaponManager : IStarverPlugin
	{
		#region interfaces
		public StarverConfig Config => StarverConfig.Config;
		public bool Enabled => Config.EnableAura && Config.EnableBoss;
		public void Load()
		{
			TShockAPI.GetDataHandlers.NewProjectile += OnProj;
		}
		public void UnLoad()
		{
			TShockAPI.GetDataHandlers.NewProjectile -= OnProj;
		}
		#endregion
		#region Hooks
		private void OnProj(object sender,TShockAPI.GetDataHandlers.NewProjectileEventArgs args)
		{
			if(args.Owner > 40 || Terraria.Main.projectile[args.Index].hostile)
			{
				return;
			}
			Weapon[] weapons;
			if(Terraria.Main.projectile[args.Index].melee)
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
			foreach(var weapon in weapons)
			{
				if(weapon.Check(args))
				{
					if (Starver.Players[args.Owner].HasWeapon(weapon))
					{
						weapon.UseWeapon(Starver.Players[args.Owner], (Vector)args.Velocity, Starver.Players[args.Owner].Weapon[weapon.Career, weapon.Index],args);
					}
					break;
				}
			}
		}
		private delegate void WeaponDelegate(StarverPlayer player,Vector Velocity, TShockAPI.GetDataHandlers.NewProjectileEventArgs args);
		#endregion
		#region Fields
		private static Weapon[] Melee =
		{

		};
		private static Weapon[] Ranged =
		{
			new VortexBeaterEx(),
			new PhantasmEx(),
			new PhantomPhoenix(),

		};
		private static Weapon[] Magic =
		{
			
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
	}
}
