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
	public class LastPrismEx : Weapon
	{
		#region Fields
		private Vector Up;
		private Vector Down;
		#endregion
		#region ctor
		public LastPrismEx() : base(2, IID.LastPrism, PID.LunarFlare, CareerType.Magic, 286)
		{
			CatchID = PID.LastPrism;
		}
		#endregion
		#region UseWeapon
		public override void UseWeapon(StarverPlayer player, Vector Velocity, int lvl, GetDataHandlers.NewProjectileEventArgs args)
		{
			Up = Velocity.Vertical();
			Up.Length = 16 * 3.5f;
			Down = -Up;
			player.ProjLine(player.Center + Up, player.Center + Down, Velocity, 7, CalcDamage(lvl), ProjID);
		}
		#endregion
	}
}
