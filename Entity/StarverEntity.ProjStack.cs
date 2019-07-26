using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using TerrariaApi.Server;
using TShockAPI;
using Terraria.Localization;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security;
namespace Starvers
{
	using Vector = TOFOUT.Terraria.Server.Vector2;
	using BigInt = System.Numerics.BigInteger;
	public abstract partial class StarverEntity
	{
		protected class ProjStack : Stack<ProjPair>, IProjSet
		{
			#region Fields
			private int Size;
			#endregion
			#region ctor
			public ProjStack(int Size = 30) : base(Size)
			{
				this.Size = Size;
			}
			#endregion
			#region Push
			public bool Push(int idx,Vector Velocity)
			{
				base.Push(new ProjPair() { Index = idx, Velocity = Velocity });
				return base.Count < Size;
			}
			#endregion
			#region Launch
			public bool Launch(int HowMany)
			{
				ProjPair pair;
				for (int i = 0; i < HowMany && base.Count > 0; i++)
				{
					pair = base.Pop();
					pair.Launch();
				}
				return base.Count > 0;
			}
			public void Launch()
			{
				while(base.Count > 0)
				{
					base.Pop().Launch();
				}
			}
			#endregion
			#region Reset
			/// <summary>
			/// 没用到
			/// </summary>
			/// <param name="ClearItem"></param>
			public void Reset(bool ClearItem)
			{

			}
			#endregion
		}
	}
}
