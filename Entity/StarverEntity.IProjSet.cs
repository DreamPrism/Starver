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
		protected interface IProjSet
		{
			/// <summary>
			/// 将弹幕以及状态压入
			/// </summary>
			/// <param name="idx">弹幕索引</param>
			/// <param name="velocity">速度</param>
			/// <returns>代表是否可以继续添加(是否已满)</returns>
			bool Push(int idx, Vector velocity);
			/// <summary>
			/// 发射若干个弹幕
			/// </summary>
			/// <param name="HowMany">发射多少个</param>
			/// <returns>是否可以继续发射(是否为空)</returns>
			bool Launch(int HowMany);
			/// <summary>
			/// 发射所有剩余弹幕
			/// </summary>
			void Launch();
			/// <summary>
			/// 清楚元素
			/// </summary>
			/// <param name="ClearItems"></param>
			void Reset(bool ClearItems = false);
		}
	}
}
