using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers.TaskSystem.Branches
{
	public partial class YrtAEvah : BranchLine
	{
		public override int Count => 6;

		public override BranchTask this[int index] => throw new NotImplementedException();

		public YrtAEvah() : base(BLID.YrtAEvah)
		{
			throw new NotImplementedException();
		}

		public override (bool Success, string Message) TryStartTask(StarverPlayer player, int index)
		{
			throw new NotImplementedException();
		}
	}
}
