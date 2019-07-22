using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers.WeaponSystem.Weapons.Melee
{
	using TShockAPI;
	using IID = Terraria.ID.ItemID;
	using PID = Terraria.ID.ProjectileID;
	using Vector = TOFOUT.Terraria.Server.Vector2;
	public class NorthPoleEx : Weapon
	{
		#region Fields
		#endregion
		#region ctor
		public NorthPoleEx() : base(0,IID.NorthPole,PID.NorthPoleSpear,CareerType.Melee,271)
		{
			CatchID = PID.NorthPoleWeapon;
		}
		#endregion
		#region UseWeapon
		public override void UseWeapon(StarverPlayer player, Vector Velocity, int lvl, GetDataHandlers.NewProjectileEventArgs args)
		{
			player.ProjSector(args.Position, 19, 16 * 3, Velocity.Angle, Math.PI / 3, CalcDamage(lvl), ProjID, lvl > 20 ? 5 : 3);
		}
		#endregion
	}
}
