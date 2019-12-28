using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Starvers.DB
{
	internal class TableCreator
	{
		#region Ctors
		private TableCreator()
		{

		}
		internal TableCreator(MySqlConnection cn)
		{
			conn = cn;
		}
		#endregion
		#region Create
		internal void CreateTable(SQLTable table)
		{
			try
			{
				string CreateSTR = string.Format("CREATE TABLE IF NOT EXISTS {0}(", table.Name);
				int i = 0;
				string type;
				foreach (var Column in table.Columns)
				{
					type = GetDBType(Column.DataType, Column.Length);
					CreateSTR += string.Format("{0} {1} ", Column.Name, type);
					if(++i != table.Columns.Count)
					{
						CreateSTR += ",";
					}
				}
				CreateSTR += ")";
				using (MySqlCommand cmd = new MySqlCommand(CreateSTR, conn))
				{
					cmd.ExecuteNonQuery();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
		#endregion
		#region Fields
		private MySqlConnection conn;
		private static string GetDBType(MySqlDbType type,int? len)
		{
			switch(type)
			{
				case MySqlDbType.Int32:
					return "INT";
				case MySqlDbType.String:
					return string.Format("CHAR({0})", len);
				case MySqlDbType.Float:
				case MySqlDbType.Double:
					return string.Format(type.ToString().ToUpper());
				default:
					return string.Format("{0}({1})",type.ToString().ToUpper(),len);
			}
		}
		#endregion
	}
}
