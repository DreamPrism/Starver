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
		protected struct ProjPair
		{
			public int Index;
			public Vector Velocity;
			public void Launch()
			{
				Main.projectile[Index].velocity = Velocity;
				NetMessage.SendData((int)PacketTypes.ProjectileNew, -1, -1, null, Index);
			}
		}
	}
}
