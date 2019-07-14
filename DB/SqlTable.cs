using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers.DB
{
	internal class SQLTable
	{
		internal List<SQLColumn> Columns { get; set; }
		public string Name { get; set; }
		public SQLTable(string name, params SQLColumn[] columns) : this(name, new List<SQLColumn>(columns))
		{

		}
		public SQLTable(string name, List<SQLColumn> columns)
		{
			Name = name;
			Columns = columns;
		}
	}
}
