using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace Starvers.TaskSystem
{
	public struct TaskItem
	{
		private class IDConvert : JsonConverter
		{
			#region Statics
			private static Dictionary<int, string> Map;
			private static Dictionary<string, int> UnMap;
			static IDConvert()
			{
				var ItemID = typeof(Terraria.ID.ItemID);
				var Literals = ItemID.GetFields().Where(field => field.IsLiteral);

				Map = new Dictionary<int, string>(Literals.Count());
				UnMap = new Dictionary<string, int>(Literals.Count());

				int value;

				foreach (var literal in Literals)
				{
					value = (int)literal.GetRawConstantValue();
					Map.Add(value, literal.Name);
					UnMap.Add(literal.Name, value);
				}
			}
			#endregion
			public override bool CanConvert(Type type)
			{
				return type == typeof(int);
			}
			public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
			{
				string id = reader.ReadAsString();
				return UnMap[id];
			}
			public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
			{
				writer.WriteValue(Map[(int)value]);
			}
		}
		[JsonConverter(typeof(IDConvert))]
		public int ID;
		public int Stack;
		public int Prefix;
		public TaskItem(int id = 2, int stack = 1, int prefix = 0)
		{
			ID = id;
			Stack = stack;
			Prefix = prefix;
		}
		public bool Match(Item item)
		{
			bool flag = true;
			flag &= ID == item.type;
			flag &= Stack <= item.stack;
			if (Prefix != 0)
			{
				flag &= Prefix == item.prefix;
			}
			return flag;
		}
		public override string ToString()
		{
			if (Prefix == 0)
			{
				return string.Format("[i/s{0}:{1}]", Stack, ID);
			}
			else
			{
				return string.Format("[i/p{0}:{1}]", Prefix, ID);
			}
		}


		public static implicit operator TaskItem(short value)
		{
			return new TaskItem(value);
		}
		public static implicit operator TaskItem((int, int) value)
		{
			return new TaskItem(value.Item1, value.Item2);
		}
		public static implicit operator TaskItem((int, int, int) value)
		{
			return new TaskItem(value.Item1, value.Item2, value.Item3);
		}
	}
}
