using Starvers.BossSystem.Bosses.Base;
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
		public NPCStrikeEventArgs(NPC NPC, NPC RealNPC, float InterDamage)
		{
			this.NPC = NPC;
			this.RealNPC = RealNPC;
			this.InterDamage = InterDamage;
		}
		public NPC NPC { get; }
		public NPC RealNPC { get; }
		public int RawDamage { get; set; }
		public float InterDamage { get; }
		/// <summary>
		/// 暴击, Boss.DamageIndex / snpc.DamageIndex, npc.DontTakeDamage与player.Level均已计算上去了
		/// </summary>
		public int RealDamage { get; set; }
		public bool Crit { get; set; }
	}
}
