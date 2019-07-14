using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers
{
	public class ExchangeItem
	{
		public int Need { get; private set; } = 1;
		public int From { get; private set; } = 1;
		public int To { get; private set; } = 1;
		public string Des { get; private set; } = null;
		public ExchangeItem(int from, int to, int need = 1,string description = null)
		{
			Need = need;
			From = from;
			To = to;
			Des = description;
		}
		public override string ToString()
		{
			return string.Format("[i/s{1}:{0}]=>[i:{2}]{3}", From, Need, To, Des == null ? "" : string.Format("({0})", Des));
		}
	}
}
