using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace Starvers.Events
{
	public class NPCStrikeEventArgs : HandledEventArgs
	{
		public NPC NPC { get; }
		public NPC RealNPC { get; }
		public int RawDamage { get; set; }
		public int RealDamage { get; set; }
		public float KnockBack { get; set; }
		public bool Crit { get; set; }
	}
}
