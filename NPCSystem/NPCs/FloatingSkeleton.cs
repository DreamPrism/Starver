using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace Starvers.NPCSystem.NPCs
{
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

		}
		#endregion
	}
}
