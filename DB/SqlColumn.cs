using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Starvers.DB
{
	internal class SQLColumn
	{
		public string Name { get; set; }
		public MySqlDbType DataType { get; set; }
		public int? Length { get; set; }
		public SQLColumn()
		{

		}
		public SQLColumn(string name, MySqlDbType type, int length)
		{
			Name = name;
			DataType = type;
			Length = length;
		}
	}
}
