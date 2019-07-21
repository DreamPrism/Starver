using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TShockAPI;

namespace Starvers.NPCSystem
{
	using Vector = TOFOUT.Terraria.Server.Vector2;
	public abstract class StarverNPC : BaseNPC
	{
		#region Fields
		protected float[] AIUsing;
		protected int RawType;
		protected int DefaultLife;
		protected int DefaultDefense;
		protected DateTime LastSpawn = DateTime.Now;
		protected StarverNPC Root;
		protected SpawnChecker Checker;
		protected abstract void RealAI();
		#endregion
		#region ctor
		protected StarverNPC(int AIs = 0)
		{
			AIUsing = new float[AIs];
		}
		#endregion
		#region Spawn
		public virtual bool Spawn(Vector where)
		{
			_active = true;
			Index = NewNPC(where, Vector.Zero, RawType, DefaultLife, DefaultDefense);
			return true;
		}
		#endregion
		#region CheckSpawn
		protected virtual bool CheckSpawn(StarverPlayer player)
		{
			return Checker.Match();
		}
		#endregion
		#region AI
		public override void AI(object args = null)
		{
			if (!Active)
			{
				return;
			}
			if (RealNPC.aiStyle == None)
			{
				if (Target < 0 || Target > 40 || TargetPlayer == null || !TargetPlayer.Active)
				{
					TargetClosest();
					if (Target == None || Vector2.Distance(TargetPlayer.Center, Center) > 16 * 400)
					{
						KillMe();
						return;
					}
				}
				Velocity = FakeVelocity;
			}
			RealAI();
			SendData();
			++Timer;
		}
		#endregion
	}
}
