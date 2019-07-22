using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers.WeaponSystem.Weapons.Magic
{
	using TShockAPI;
	using IID = Terraria.ID.ItemID;
	using PID = Terraria.ID.ProjectileID;
	using Vector = TOFOUT.Terraria.Server.Vector2;
	public class LaserMachinegunEx : Weapon
	{
		#region Fields
		private Vector Vertical;
		#endregion
		#region ctor
		public LaserMachinegunEx() : base(3,IID.LaserMachinegun,PID.LaserMachinegunLaser,CareerType.Magic,242)
		{
			CatchID = PID.LaserMachinegun;
		}
		#endregion
		#region UseWeapon
		public override void UseWeapon(StarverPlayer player, Vector Velocity, int lvl, GetDataHandlers.NewProjectileEventArgs args)
		{
			Vertical = Velocity.Vertical();
			Vertical.Length = Starver.Rand.Next(-4,5) * 16;
			player.NewProj(args.Position + Vertical, Velocity, ProjID, CalcDamage(lvl), args.Knockback);
		}
		#endregion
	}
}
