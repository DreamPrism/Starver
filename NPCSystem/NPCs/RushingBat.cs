using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Terraria.ID;

namespace Starvers.NPCSystem.NPCs
{
	using Vector = TOFOUT.Terraria.Server.Vector2;
	using Microsoft.Xna.Framework;
	public class RushingBat : StarverNPC
	{
		#region Fields
		/// <summary>
		/// 加速度
		/// </summary>
		private Vector Acc;
		/// <summary>
		/// 死亡地点
		/// </summary>
		private Vector2 DeathPos;
		#endregion
		#region Properties
		protected override float CollidingIndex => DamageIndex;
		#endregion
		#region ctor
		public RushingBat() : base(2)
		{
			AfraidSun = true;
			Width = 13 * 2;
			Height = Width;
			Checker = SpawnChecker.UnderGroundLike;
			RawType = NPCID.GiantBat;
			CollideDamage = 400;
			DefaultLife = 3000;
			DefaultDefense = 400000;
		}
		#endregion
		#region RealAI
		protected override void RealAI()
		{
			if (Vector2.Distance(TargetPlayer.Center, Center) < 16 * 3)
			{
				Proj(Center, Vector.Zero, ProjectileID.StardustGuardianExplosion, 0);
				DeathPos = Center;
				new Thread(TransformToBolt).Start();
				KillMe();
				return;
			}
			else //if(Timer % 20 == 0)
			{
				AIUsing[0] = 0;
				Acc = (Vector)(TargetPlayer.Center + NewByPolar(AIUsing[1], 16 * 5) - Center);
				Acc.Length = 0.2f;
				FakeVelocity += Acc;
				if (FakeVelocity.Length > 9)
				{
					FakeVelocity.Length = 9;
				}
			}
			if (AIUsing[0]++ > 60 * 3)
			{
				Proj(Center, Vector.Zero, ProjectileID.StardustGuardianExplosion, 0);
				DeathPos = Center;
				new Thread(TransformToBolt).Start();
				KillMe();
			}
			AIUsing[1] += PI / 120;
		}
		#endregion
		#region TransFormToBolt
		private void TransformToBolt()
		{
			Thread.Sleep(500);
			Proj(DeathPos, Vector.Zero, ProjectileID.NebulaSphere, 200);
		}
		#endregion
	}
}
