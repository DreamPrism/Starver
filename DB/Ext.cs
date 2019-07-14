using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Starvers.DB
{
	internal static class Ext
	{
		internal static MySqlDataReader QueryReader(this MySqlConnection con, string cmdtxt, params object[] args)
		{
			var db = con.SafeClone();
			db.ConnectionString = con.ConnectionString;
			db.Open();
			MySqlDataReader result;
			using (MySqlCommand cmd = db.CreateCommand() as MySqlCommand)
			{
				cmd.CommandText = cmdtxt;
				for (int i = 0; i < args.Length; i++)
				{
					cmd.AddParameter("@" + i, args[i]);
				}
				result = cmd.ExecuteReader();
			}
			return result;
		}
		internal static void Excute(this MySqlConnection con, string cmdtxt, params object[] args)
		{
			using(var con2 = con.SafeClone())
			{
				con2.ConnectionString = con.ConnectionString;
				con2.Open();
				using (MySqlCommand cmd = con2.CreateCommand())
				{
					cmd.CommandText = cmdtxt;
					for (int i = 0; i < args.Length; i++)
					{
						cmd.AddParameter("@" + i, args[i]);
					}
					cmd.ExecuteNonQuery();
				}
				con.Dispose();
			}
			
		}
		public static void AddParameter(this MySqlCommand command, string name, object data)
		{
			command.Parameters.AddWithValue(name, data);
		}
		public static MySqlConnection SafeClone(this MySqlConnection connection)
		{
			MySqlConnection db = new MySqlConnection(connection.ConnectionString);
			return db;
		}
	}
}
