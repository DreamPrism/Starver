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
	/// <summary>
	/// 事先将弹幕和速度存入,稍后发射
	/// </summary>
	public class ProjQueue : IProjSet
	{
		#region Fields
		private int Size = 30;
		private int[] ProjIndex;
		private Vector[] Velocity;
		//private int* ProjIndex;
		//private Vector* Velocity;
		/// <summary>
		/// 存了多少个
		/// </summary>
		private int t;
		/// <summary>
		/// 计算发射了多少个
		/// </summary>
		private int CurrentLaunch;
		#endregion
		#region ctor
		public ProjQueue(int size = 30)
		{
			Size = size;
			ProjIndex = new int[size];
			Velocity = new Vector[size];
			//ProjIndex = (int*)Marshal.AllocHGlobal(sizeof(int) * Size);
			//Velocity = (Vector*)Marshal.AllocHGlobal(sizeof(Vector) * Size);
		}
		#endregion
		#region dtor
		/*
		~ProjQueue()
		{
			Dispose(false);
		}
		*/
		#endregion
		#region Indexer
		public Projectile this[int idx] => Main.projectile[ProjIndex[idx]];
		#endregion
		#region Push
		public bool Push(int idx, Vector velocity)
		{
			if (t >= Size || idx <= 0)
			{
				return false;
			}
			ProjIndex[t] = idx;
			Velocity[t++] = velocity;
			return true;
		}
		public bool Push(IEnumerable<int> idxes, Vector vel)
		{
			foreach(var idx in idxes)
			{
				if (!Push(idx, vel))
				{
					return false;
				}
			}
			return t < Size;
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
			return t < Size;
		}
		#endregion
		#region Launch
		public void Launch()
		{
			for (int i = CurrentLaunch; i < t; i++) 
			{
				if (this[t].active)
				{
					this[t].velocity = Velocity[t];
					NetMessage.SendData((int)PacketTypes.ProjectileNew, -1, -1, null, ProjIndex[t]);
				}
			}
			Reset(true);
		}
		public void Launch(Vector Velocity)
		{
			for (int i = CurrentLaunch; i < t; i++)
			{
				if (this[t].active)
				{
					this[t].velocity = Velocity;
					NetMessage.SendData((int)PacketTypes.ProjectileNew, -1, -1, null, ProjIndex[t]);
				}
			}
			Reset(true);
		}
		public bool Launch(int HowMany, Vector Vel)
		{
			int Limit = CurrentLaunch + HowMany;
			for (; CurrentLaunch < Limit && CurrentLaunch < t; CurrentLaunch++)
			{
				if (this[CurrentLaunch].active)
				{
					this[CurrentLaunch].velocity = Vel;
					NetMessage.SendData((int)PacketTypes.ProjectileNew, -1, -1, null, ProjIndex[CurrentLaunch]);
				}
			}
			return CurrentLaunch < Size;
		}
		public bool Launch(int HowManyProjs)
		{
			int Limit = CurrentLaunch + HowManyProjs;
			for (; CurrentLaunch < Limit && CurrentLaunch < t; CurrentLaunch++)
			{
				if (this[CurrentLaunch].active)
				{
					this[CurrentLaunch].velocity = Velocity[CurrentLaunch];
					NetMessage.SendData((int)PacketTypes.ProjectileNew, -1, -1, null, ProjIndex[CurrentLaunch]);
				}
			}
			return CurrentLaunch < Size;
		}
		public bool LaunchTo(int HowMany, Vector Pos, float vel)
		{
			int Limit = CurrentLaunch + HowMany;
			for (; CurrentLaunch < Limit && CurrentLaunch < t; CurrentLaunch++)
			{
				if (this[CurrentLaunch].active)
				{
					this[CurrentLaunch].velocity = (Pos - this[CurrentLaunch].Center).ToLenOf(vel);
					NetMessage.SendData((int)PacketTypes.ProjectileNew, -1, -1, null, ProjIndex[CurrentLaunch]);
				}
			}
			return CurrentLaunch < Size;
		}
		public void LaunchTo(Vector Pos, float vel)
		{
			LaunchTo(t - CurrentLaunch, Pos, vel);
		}
		#endregion
		#region Reset
		public void Reset(bool ClearItems = false)
		{
			t = 0;
			CurrentLaunch = 0;
			if (ClearItems)
			{
				for (int i = 0; i < ProjIndex.Length; i++)
				{
					ProjIndex[i] = 0;
				}
				/*
				int* ptr = ProjIndex;
				int* end = ptr + Size;
				while (ptr != end)
				{
					*ptr++ = 0;
				}
				*/
			}
		}
		#endregion
		#region Dispose
		/*
		public void Dispose()
		{
			Dispose(true);
		}
		protected virtual void Dispose(bool disposing)
		{
			if(disposing)
			{
				GC.SuppressFinalize(this);
			}
			if(ProjIndex == null)
			{
				return;
			}
			Marshal.FreeHGlobal((IntPtr)ProjIndex);
			Marshal.FreeHGlobal((IntPtr)Velocity);
			ProjIndex = null;
			Velocity = null;
		}
		*/
		#endregion
	}
}
