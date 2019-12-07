using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace Starvers.WeaponSystem
{
	public static class Currency
	{
		public const int Melee = ItemID.Fake_newchest1;
		public const int Ranged = ItemID.Fake_newchest2;
		public const int Magic = ItemID.BlueCultistCasterBanner;
		public const int Minion = ItemID.BlueCultistFighterBanner;
		public static int[] Shards { get; } = new int[] { Melee, Ranged, Magic, Minion };
	}
}
