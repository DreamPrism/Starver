using Starvers.AuraSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers.Events
{
	public class ReleaseSkillEventArgs : HandledEventArgs
	{
		/// <summary>
		/// [0, MaxSkills)
		/// </summary>
		public int Slot { get; set; }
		/// <summary>
		/// 单位: 帧(1 / 60秒)
		/// </summary>
		public int CD { get; set; }
		public int MPCost { get; set; }
		public SkillIDs SkillID { get; set; }
		public bool Banned { get; set; }
	}
}
