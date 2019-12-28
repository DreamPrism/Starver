using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers.WeaponSystem.Weapons.Ranged
{
	using System.Threading;
	using TShockAPI;
	using Vector = TOFOUT.Terraria.Server.Vector2;
	public class XenopopperEx : Weapon
	{
		#region Fields
		#endregion
		#region Ctor
		public XenopopperEx() : base(3, Terraria.ID.ItemID.Xenopopper, Terraria.ID.ProjectileID.ElectrosphereMissile, CareerType.Ranged, 223)
		{
			CatchID = Terraria.ID.ProjectileID.Xenopopper;
		}
		#endregion
		#region UseItem
		public override void UseWeapon(StarverPlayer player, Vector Velocity, int lvl, TShockAPI.GetDataHandlers.NewProjectileEventArgs args)
		{
			Thread.Sleep(90);
			player.ProjCircle(Terraria.Main.projectile[args.Index].Center, 16 * 5, 23, ProjID, 6, CalcDamage(lvl));
		}
		#endregion
	}
}
