using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers.NPCSystem
{
	public struct SpawnChecker
	{
		#region Properties
		public static StarverConfig Config => StarverConfig.Config;
		public SpawnConditions Condition { get; set; }
		public BiomeType Biome { get; set; }
		public int Task { get; set; }
		public int SpawnRate { get; set; }
		public float SpawnChance { get; set; }
		#endregion
		#region Method
		public bool Match(SpawnChecker value)
		{
			return value.Condition.HasFlag(Condition) && value.Biome.HasFlag(Biome);
		}
		#endregion
		#region Static
		#region Fields
		private static SpawnChecker zombieLike;
		private static SpawnChecker slimeLike;
		private static SpawnChecker dungeonLike;
		#endregion
		#region ctor
		static SpawnChecker()
		{
			zombieLike = new SpawnChecker
			{
				Condition = SpawnConditions.Night,
				Biome = BiomeType.Grass,
				SpawnRate = 60 * 2 + 30,
				SpawnChance = 0.6f
			};
			slimeLike = new SpawnChecker
			{
				Condition = SpawnConditions.Day,
				SpawnRate = 60 * 5,
				SpawnChance = 0.6f
			};
			dungeonLike = new SpawnChecker
			{
				Biome = BiomeType.Dungeon,
				SpawnRate = 60 * 4,
				SpawnChance = 0.25f
			};
		}
		#endregion
		#region Properties
		public static SpawnChecker ZombieLike => zombieLike;
		public static SpawnChecker SlimeLike => slimeLike;
		public static SpawnChecker DungeonLike => dungeonLike;
		#endregion
		#endregion
	}
}
