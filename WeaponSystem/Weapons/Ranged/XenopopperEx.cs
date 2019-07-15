using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers.WeaponSystem.Weapons.Ranged
{
	using System.Threading;
	using TOFOUT.Terraria.Server;
	using TShockAPI;
	using Vector = TOFOUT.Terraria.Server.Vector2;
	public class XenopopperEx : Weapon
	{
		#region Fields
		#endregion
		#region ctor
		public XenopopperEx() : base(3, Terraria.ID.ItemID.Xenopopper, Terraria.ID.ProjectileID.Xenopopper, CareerType.Ranged, 223)
		{
			
		}
		#endregion
		#region Check
		public override bool Check(GetDataHandlers.NewProjectileEventArgs args)
		{
			return Terraria.Main.player[args.Owner].HeldItem.type == ItemID;
		}
		#endregion
		#region UseItem
		public override void UseWeapon(StarverPlayer player, Vector Velocity, int lvl, TShockAPI.GetDataHandlers.NewProjectileEventArgs args)
		{
			Thread.Sleep(110);
			player.ProjCircle(Terraria.Main.projectile[args.Index].Center, 16 * 5, 23, ProjID, 6, CalcDamage(lvl));
		}
		#endregion
	}
}
