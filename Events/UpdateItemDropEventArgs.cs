using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Starvers.Events
{
	public class UpdateItemDropEventArgs : HandledEventArgs
	{
		public int ItemIndex { get; }
		public Vector2 Position { get; set; }
		public Vector2 Velocity { get; set; }
		public int ID { get; set; }
		public int Stack { get; set; }
		public int Prefix { get; set; }
		public UpdateItemDropEventArgs(int itemIndex)
		{
			ItemIndex = itemIndex;
		}
		public override string ToString()
		{
			if (Prefix == 0)
			{
				return $"[i/s{Stack}:{ID}]({ItemIndex}), Pos: {Position}, Vel: {Velocity}";
			}
			return $"[i/p{Prefix}:{ID}]({ItemIndex}), Pos: {Position}, Vel: {Velocity}";
		}
	}
}
