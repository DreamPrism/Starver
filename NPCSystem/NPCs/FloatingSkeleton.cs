using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace Starvers.NPCSystem.NPCs
{
	using Vector = TOFOUT.Terraria.Server.Vector2;
	public class FloatingSkeleton : StarverNPC
	{
		#region Fields
		private int[] SkeletonTypes =
		{
			NPCID.Skeleton,
			NPCID.SkeletonAlien,
			NPCID.SkeletonAstonaut,
			NPCID.PantlessSkeleton,
			NPCID.BigHeadacheSkeleton,
			NPCID.BigMisassembledSkeleton,
			NPCID.SmallSkeleton,
			NPCID.SmallPantlessSkeleton,
			NPCID.SmallHeadacheSkeleton,
			NPCID.SmallHeadacheSkeleton
		};
		#endregion
		#region Properties
		protected override float CollidingIndex => DamageIndex;
		#endregion
		#region ctor
		public FloatingSkeleton()
		{
			RawType = NPCID.Skeleton;
			Types = SkeletonTypes;
			AIStyle = 2;
			DefaultLife = 13000;
			DefaultDefense = 300000;
			NoTileCollide = true;
			Checker = SpawnChecker.ZombieLike;
			Checker.SpawnChance /= 2;
			Checker.SpawnRate *= 3;
			Checker.SpawnRate /= 2;
			DamagedIndex = 0.1f;
			CollideDamage = 350;
		}
		#endregion
		#region RealAI
		protected override void RealAI()
		{
			FakeVelocity = (Vector)Velocity;
			if(Timer < 60 * 7 / 2) // 3.5 * 2s一个周期
			{
				if (CheckSecond(1.25) || CheckSecond(1.95) || CheckSecond(2.65))
				{
					Vel = (Vector)(TargetPlayer.Center - Center);
					Vel.Length = 19;
					Proj(Center, Vel, ProjectileID.Bone, 200, 20);
				}
			}
		}
		#endregion
	}
}
