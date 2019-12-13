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
		public int ID { get; private set; }
		public int Stack { get; private set; }
		public int Prefix { get; private set; }
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
