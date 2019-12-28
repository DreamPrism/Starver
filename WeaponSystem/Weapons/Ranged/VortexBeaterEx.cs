using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers.WeaponSystem.Weapons.Ranged
{
	using IDs = Terraria.ID;
	public class VortexBeaterEx : Weapon
	{
		#region Ctor
		public VortexBeaterEx() : base(0,IDs.ItemID.VortexBeater, IDs.ProjectileID.VortexBeaterRocket, CareerType.Ranged, 235)
		{
			CatchID = IDs.ProjectileID.VortexBeater;
		}
		#endregion
	}
}
