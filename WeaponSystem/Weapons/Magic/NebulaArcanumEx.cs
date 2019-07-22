using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers.WeaponSystem.Weapons.Magic
{
    using System.Runtime.InteropServices;
    using TShockAPI;
	using IID = Terraria.ID.ItemID;
	using PID = Terraria.ID.ProjectileID;
	using Vector = TOFOUT.Terraria.Server.Vector2;
	public class NebulaArcanumEx : Weapon
	{
		#region Fields
		private Vector DeflectUp;
		private Vector DeflectDown;
		private unsafe DateTime* LastRelease;
		#endregion
		#region ctor dtor
		public NebulaArcanumEx() : base(0, IID.NebulaArcanum, PID.NebulaArcanum, CareerType.Magic, 251)
		{
			CatchID = PID.NebulaArcanum;
			unsafe
			{
				LastRelease = (DateTime*)Marshal.AllocHGlobal(sizeof(DateTime) * 40).ToPointer();
			}
		}
		~NebulaArcanumEx()
		{
			unsafe
			{
				Marshal.FreeHGlobal((IntPtr)LastRelease);
			}
		}
		#endregion
		#region CheckTime
		private unsafe bool CheckTime(int who)
		{
			if ((DateTime.Now - LastRelease[who]).TotalSeconds > 1.5)
			{
				LastRelease[who] = DateTime.Now;
				return true;
			}
			else
			{
				return false;
			}
		}
		#endregion
		#region Match
		public unsafe override bool Check(GetDataHandlers.NewProjectileEventArgs args)
		{
			return base.Check(args) && CheckTime(args.Owner);
		}
		#endregion
		#region UseWeapon
		public override void UseWeapon(StarverPlayer player, Vector Velocity, int lvl, GetDataHandlers.NewProjectileEventArgs args)
		{
			Terraria.Main.projectile[args.Index].active = false;
			DeflectUp = Velocity.Deflect(Math.PI / 6);
			DeflectUp.Length = 16 * 5;
			DeflectDown = Velocity.Deflect(-Math.PI / 6);
			DeflectDown.Length = 16 * 5;
			player.NewProj(args.Position, Velocity, ProjID, CalcDamage(lvl), args.Knockback);
			player.NewProj(args.Position + DeflectUp, Velocity, ProjID, CalcDamage(lvl), args.Knockback);
			player.NewProj(args.Position + 2 * DeflectUp, Velocity, ProjID, CalcDamage(lvl), args.Knockback);
			player.NewProj(args.Position + DeflectDown, Velocity, ProjID, CalcDamage(lvl), args.Knockback);
			player.NewProj(args.Position + 2 * DeflectDown, Velocity, ProjID, CalcDamage(lvl), args.Knockback);
		}
		#endregion
	}
}
