using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers.TaskSystem
{
	public interface ITask
	{
		public bool IsFinished { get; }
		public string Description { get; }
		public bool Check(StarverPlayer player);
	}
}
