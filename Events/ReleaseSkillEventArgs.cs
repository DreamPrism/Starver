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
	}
}
