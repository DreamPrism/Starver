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
		/// <summary>
		/// 是否是自杀
		/// </summary>
		private bool KillSelf;
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
			float Distance = Vector2.Distance(TargetPlayer.Center, Center);
			if (Distance < 16 * 3)
			{
				Proj(Center, Vector.Zero, ProjectileID.StardustGuardianExplosion, 0);
				DeathPos = Center;
				new Thread(TransformToBolt).Start();
				KillMe();
				return;
			}
			else if(Distance < 16 * 40)
			{

			}
			else //if(Timer % 20 == 0)
			{
				AIUsing[0] = 0;
				Acc = (Vector)(TargetPlayer.Center + NewByPolar(AIUsing[1], 16 * 8) - Center);
				Acc.Length = 0.2f;
				FakeVelocity += Acc;
				if (FakeVelocity.Length > 9)
				{
					FakeVelocity.Length = 9;
				}
			}
			if (AIUsing[0]++ > 60 * 5)
			{
				Proj(Center, Vector.Zero, ProjectileID.StardustGuardianExplosion, 0);
				DeathPos = Center;
				KillSelf = true;
				new Thread(TransformToBolt).Start();
				KillMe();
				return;
			}
			AIUsing[1] += PI / 120;
		}
		#endregion
		#region TransFormToBolt
		private void TransformToBolt()
		{
			Thread.Sleep(500);
			if (!KillSelf)
			{
				Proj(DeathPos, Vector.Zero, ProjectileID.NebulaSphere, 200);
			}
		}
		#endregion
	}
}
