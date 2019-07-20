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
	public class NebulaArcanumEx : Weapon
	{
		#region Fields
		private Vector DeflectUp;
		private Vector DeflectDown;
		#endregion
		#region ctor
		public NebulaArcanumEx() : base(0, IID.NebulaArcanum, PID.NebulaArcanum, CareerType.Magic, 251)
		{
			CatchID = PID.NebulaArcanum;
		}
		#endregion
		#region Match
		public override bool Check(GetDataHandlers.NewProjectileEventArgs args)
		{
			return base.Check(args) && !Terraria.Main.projectile[args.Index].thrown;
		}
		#endregion
		#region UseWeapon
		public override void UseWeapon(StarverPlayer player, Vector Velocity, int lvl, GetDataHandlers.NewProjectileEventArgs args)
		{
			args.Handled = true;
			Terraria.Main.projectile[args.Index].active = false;
			DeflectUp = Velocity.Deflect(Math.PI / 6);
			DeflectUp.Length = 16 * 5;
			DeflectDown = Velocity.Deflect(-Math.PI / 6);
			DeflectDown.Length = 16 * 5;
			Terraria.Main.projectile[player.NewProj(args.Position, Velocity, ProjID, CalcDamage(lvl), args.Knockback)].thrown = true;
			Terraria.Main.projectile[player.NewProj(args.Position + DeflectUp, Velocity, ProjID, CalcDamage(lvl), args.Knockback)].thrown = true;
			Terraria.Main.projectile[player.NewProj(args.Position + 2 * DeflectUp, Velocity, ProjID, CalcDamage(lvl), args.Knockback)].thrown = true;
			Terraria.Main.projectile[player.NewProj(args.Position + DeflectDown, Velocity, ProjID, CalcDamage(lvl), args.Knockback)].thrown = true;
			Terraria.Main.projectile[player.NewProj(args.Position + 2 * DeflectDown, Velocity, ProjID, CalcDamage(lvl), args.Knockback)].thrown = true;
		}
		#endregion
	}
}
