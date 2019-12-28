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
	public class ProjStack : Stack<ProjPair>, IProjSet
	{
		#region Fields
		private int Size;
		#endregion
		#region Ctor
		public ProjStack(int Size = 30) : base(Size)
		{
			this.Size = Size;
		}
		#endregion
		#region Push
		public bool Push(int idx, Vector Velocity)
		{
			Push(new ProjPair() { Index = idx, Velocity = Velocity });
			return Count < Size;
		}
		public bool Push(IEnumerable<int> idxes, Vector Velocity)
		{
			foreach(int idx in idxes)
			{
				if (!Push(idx, Velocity))
				{
					return false;
				}
			}
			return Count < Size;
		}
		public unsafe bool Push(int* ptr, int count, Vector vel)
		{
			int* end = ptr + count;
			while (ptr != end)
			{
				if (!Push(*ptr++, vel))
				{
					return false;
				}
			}
			return Count < Size;
		}
		#endregion
		#region Launch
		public bool Launch(int HowMany)
		{
			ProjPair pair;
			for (int i = 0; i < HowMany && base.Count > 0; i++)
			{
				pair = Pop();
				pair.Launch();
			}
			return base.Count > 0;
		}
		public bool Launch(int HowMany, Vector vel)
		{
			ProjPair pair;
			for (int i = 0; i < HowMany && base.Count > 0; i++)
			{
				pair = Pop();
				pair.Launch(vel);
			}
			return Count > 0;
		}
		public bool LaunchTo(int HowMany, Vector Where, float vel)
		{
			ProjPair pair;
			for (int i = 0; i < HowMany && base.Count > 0; i++)
			{
				pair = Pop();
				pair.Launch(Where, vel);
			}
			return Count > 0;
		}
		public void Launch()
		{
			while (Count > 0)
			{
				Pop().Launch();
			}
		}
		public void Launch(Vector vel)
		{
			while (Count > 0)
			{
				Pop().Launch(vel);
			}
		}
		public void LaunchTo(Vector Where, float vel)
		{
			while (Count > 0)
			{
				Pop().Launch(Where, vel);
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
