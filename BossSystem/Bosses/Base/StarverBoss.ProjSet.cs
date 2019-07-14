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
namespace Starvers.BossSystem.Bosses.Base
{
	using Vector = TOFOUT.Terraria.Server.Vector2;
	using BigInt = System.Numerics.BigInteger;
	public abstract partial class StarverBoss
	{
		#region ProjSet
		/// <summary>
		/// 事先将弹幕和速度压入,稍后发射
		/// </summary>
		protected unsafe class ProjDelay : IProjSet
		{
			#region Fields
			private int Size = 30;
			private int* ProjIndex;
			private Vector* Velocity;
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
			public ProjDelay(int size = 30)
			{
				Size = size;
				ProjIndex = (int*)Marshal.AllocHGlobal(sizeof(int) * Size).ToPointer();
				Velocity = (Vector*)Marshal.AllocHGlobal(sizeof(Vector) * Size).ToPointer();
			}
			#endregion
			#region dtor
			~ProjDelay()
			{
				Marshal.FreeHGlobal((IntPtr)ProjIndex);
				Marshal.FreeHGlobal((IntPtr)Velocity);
			}
			#endregion
			#region Indexer
			public Projectile this[int idx] => Main.projectile[ProjIndex[idx]];
			#endregion
			#region Push
			public bool Push(int idx, Vector velocity)
			{
				if (t >= Size || idx >= Size || idx <= 0)
				{
					return false;
				}
				ProjIndex[t] = idx;
				Velocity[t++] = velocity;
				return true;
			}
			#endregion
			#region Launch
			public void Launch()
			{
				int* ptr = ProjIndex + CurrentLaunch;
				int* end = ProjIndex + t;
				Vector* pVelocity = Velocity + CurrentLaunch;
				while (ptr < end)
				{
#if DEBUG
					StarverPlayer.All.SendDeBugMessage($"*ptr:{*ptr}");
#endif
					if (Main.projectile[*ptr].active)
					{
						Main.projectile[*ptr].velocity = *pVelocity;
						NetMessage.SendData((int)PacketTypes.ProjectileNew, -1, -1, null, *ptr);
					}
					ptr++;
					pVelocity++;
				}
				Reset(true);
			}
			public bool Launch(int HowManyProjs)
			{
				int Limit = CurrentLaunch + HowManyProjs;
				for (; CurrentLaunch < Limit && CurrentLaunch < t; CurrentLaunch++)
				{
#if DEBUG
					StarverPlayer.All.SendDeBugMessage($"ProjIndex[{CurrentLaunch}]:{ProjIndex[CurrentLaunch]}");
#endif
					if (Main.projectile[ProjIndex[CurrentLaunch]].active)
					{
						Main.projectile[ProjIndex[CurrentLaunch]].velocity = Velocity[CurrentLaunch];
						NetMessage.SendData((int)PacketTypes.ProjectileNew, -1, -1, null, ProjIndex[CurrentLaunch]);
					}
				}
				return CurrentLaunch < Size;
			}
			#endregion
			#region Reset
			public void Reset(bool ClearItems = false)
			{
				t = 0;
				CurrentLaunch = 0;
				if (ClearItems)
				{
					int* ptr = ProjIndex;
					int* end = ptr + Size;
					while (ptr != end)
					{
						*ptr++ = 0;
					}
				}
			}
			#endregion
		}
		#endregion
	}
}
