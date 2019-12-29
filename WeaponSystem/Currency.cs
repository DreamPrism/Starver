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
		public static int Melee { get; private set; } = ItemID.Fake_newchest1;
		public static int Ranged { get; private set; } = ItemID.Fake_newchest2;
		public static int Magic { get; private set; } = ItemID.BlueCultistCasterBanner;
		public static int Minion { get; private set; } = ItemID.BlueCultistFighterBanner;
		public static int[] Shards { get; private set; } 

		public static void Initialize()
		{
			if(Starver.IsPE)
			{
				Melee = Magic;
				Ranged = Minion;
			}
			Shards = new int[] { Melee, Ranged, Magic, Minion };
		}
	}
}
