using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers.WeaponSystem.Weapons.Melee
{
    using System.Runtime.CompilerServices;
    using System.Threading;
    using TShockAPI;
	using IID = Terraria.ID.ItemID;
	using PID = Terraria.ID.ProjectileID;
	using Vector = TOFOUT.Terraria.Server.Vector2;
	using Vector2 = Microsoft.Xna.Framework.Vector2;
	public class TerraBladeEx : Weapon
	{
		#region Fields
		private const int TerraBeamID = PID.TerraBeam;
		#endregion
		#region Ctor
		public TerraBladeEx() : base(1, IID.TerraBlade, PID.DD2SquireSonicBoom, CareerType.Melee, 312)
		{
			CatchID = PID.TerraBeam;
		}
		#endregion
		#region UseWeapon
		public override void UseWeapon(StarverPlayer player, Vector Velocity, int lvl, GetDataHandlers.NewProjectileEventArgs args)
		{
			AsyncUse(player, Velocity, lvl, args);
		}
		private async void AsyncUse(StarverPlayer player, Vector Velocity, int lvl, GetDataHandlers.NewProjectileEventArgs args)
		{
			int damage = CalcDamage(lvl);
			player.ProjCircleEx(player.Center, Rand.NextAngle(), 16 * 6, 15, ProjID, Rand.Next(7, 10), damage * 3 / 4, 2);
			await Task.Run(() =>
			{
				Terraria.Projectile proj = Terraria.Main.projectile[args.Index];
				Vector Vertical = Velocity.Vertical();
				Vertical.Length = 16 * 3.75f;
				uint Timer = 0;
				Vector location = (Vector)proj.Center;
				Velocity *= 2;
				try
				{
					proj.active = false;
					StarverPlayer.All.SendData(PacketTypes.ProjectileNew, "", proj.whoAmI);
				}
				catch (Exception e)
				{
					StarverPlayer.All.SendErrorMessage(e.ToString());
				}
				//player.ProjLine(player.Center, player.Center + args.Velocity.ToLenOf(16 * 20), args.Velocity.ToLenOf(1), 10, CalcDamage(lvl), PID.SolarWhipSwordExplosion);
				while (Timer++ < 50)
				{
					player.ProjLine(location + Vertical, location - Vertical, Vector2.Zero, 6, damage, PID.SolarWhipSwordExplosion);
					location += Velocity;
					Thread.Sleep(20);
				}
			});
		}
		#endregion
	}
}
