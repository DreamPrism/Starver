using Starvers.TaskSystem;
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
		#region Properties
		public static SpawnChecker ZombieLike { get; } = new SpawnChecker
		{
			Condition = SpawnConditions.Night,
			Biome = BiomeType.Grass,
			SpawnRate = 60 * 2 + 30,
			SpawnChance = 0.6f,
			Task = TaskID.MoonLord + 1
		};
		public static SpawnChecker SlimeLike { get; } = new SpawnChecker
		{
			Condition = SpawnConditions.Day,
			SpawnRate = 60 * 5,
			SpawnChance = 0.6f,
			Task = TaskID.MoonLord + 1
		};
		public static SpawnChecker DungeonLike { get; } = new SpawnChecker
		{
			Biome = BiomeType.Dungeon,
			SpawnRate = 60 * 4,
			SpawnChance = 0.25f,
			Task = TaskID.MoonLord + 1
		};
		public static SpawnChecker UnderGroundLike { get; } = new SpawnChecker
		{
			Biome = BiomeType.UnderGround,
			SpawnRate = 60 * 2,
			SpawnChance = 0.2f,
			Task = TaskID.MoonLord + 1
		};
		/// <summary>
		/// 晚上出生的稀有怪;
		/// </summary>
		public static SpawnChecker RareNight { get; } = new SpawnChecker
		{
			Condition = SpawnConditions.Night,
			Biome = BiomeType.Grass,
			SpawnRate = (60 * 2 + 30)*2,
			SpawnChance = 0.6f / 4,
			Task = TaskID.MoonLord + 1
		};
		#endregion
		#endregion
	}
}
