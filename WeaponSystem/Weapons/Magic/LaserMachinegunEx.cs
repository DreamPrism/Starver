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
		#endregion
		#region Ctor
		public LaserMachinegunEx() : base(3, IID.LaserMachinegun, PID.LaserMachinegunLaser, CareerType.Magic, 242)
		{
			CatchID = PID.LaserMachinegun;
		}
		#endregion
		#region UseWeapon
		public override void UseWeapon(StarverPlayer player, Vector Velocity, int lvl, GetDataHandlers.NewProjectileEventArgs args)
		{
			InternalUse(player, Velocity, lvl, args);
		}
		private void InternalUse(StarverPlayer player, Vector Velocity, int lvl, GetDataHandlers.NewProjectileEventArgs args)
		{
			//await Task.Run(() =>
			{
				Vector Vertical = Velocity.Vertical();
				Vertical.Length = 16 * 3;
				int damage = CalcDamage(lvl);
				int Count = 17 + Math.Min(lvl / 3, 20);
				for (int i = 0; i < Count; i++)
				{
					player.NewProj(args.Position + Vertical * Starver.Rand.NextFloat(-2, 2), Velocity, ProjID, damage);
				}
			};
		}
		#endregion
	}
}
