using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Microsoft.Xna.Framework;
using System.Runtime.InteropServices;

namespace Starvers.AuraSystem.Realms
{
	using Vector = TOFOUT.Terraria.Server.Vector2;
	public static class InternalShapes
	{
		[StructLayout(LayoutKind.Explicit)]
		public struct Circle
		{
			[FieldOffset(0)]
			public Vector2 Position;
			[FieldOffset(sizeof(float) * 0)]
			public float X;
			[FieldOffset(sizeof(float) * 1)]
			public float Y;
			[FieldOffset(sizeof(float) * 2)]
			public float Radium;

			public static implicit operator Circle((float, float, float) value)
			{
				return new Circle { X = value.Item1, Y = value.Item2, Radium = value.Item3 };
			}
			public static implicit operator Circle((Vector, float) value)
			{
				return new Circle { X = value.Item1.X, Y = value.Item1.Y, Radium = value.Item2 };
			}
			public static implicit operator Circle((Vector2, float) value)
			{
				return new Circle { X = value.Item1.X, Y = value.Item1.Y, Radium = value.Item2 };
			}
		}
	}
}
