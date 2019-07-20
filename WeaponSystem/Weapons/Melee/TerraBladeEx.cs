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
	public class TerraBladeEx : Weapon
	{
		#region ctor
		public TerraBladeEx() : base(1, IID.TerraBlade, PID.DD2SquireSonicBoom, CareerType.Melee, 288)
		{
			CatchID = PID.TerraBeam;
		}
		#endregion
		#region UseWeapon
		public override void UseWeapon(StarverPlayer player, Vector Velocity, int lvl, GetDataHandlers.NewProjectileEventArgs args)
		{
			player.ProjCircle(player.Center, 16 * 6, 15, ProjID, 8, CalcDamage(lvl) * 3 / 4, 2);
			player.ProjLine(player.Center, player.Center + args.Velocity.ToLenOf(16 * 20), args.Velocity.ToLenOf(1), 10, CalcDamage(lvl), ProjID);
		}
		#endregion
	}
}
