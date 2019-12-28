using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace Starvers.NPCSystem.NPCs
{
	public class Zomb : StarverNPC
	{
		#region Fields
		private static int[] ZombieTypes =
		{
			NPCID.BloodZombie,
			NPCID.Zombie,
			NPCID.ZombieDoctor,
			NPCID.ZombieElf,
			NPCID.ZombieElf,
			NPCID.ZombieMushroom,
			NPCID.ZombiePixie,
			NPCID.ZombieSweater,
			NPCID.ZombieSuperman
		};
		#endregion
		#region Properties
		protected override float CollidingIndex => DamageIndex;
		#endregion
		#region Ctor
		public Zomb()
		{
			AfraidSun = true;
			Checker = SpawnChecker.ZombieLike;
			RawType = NPCID.Zombie;
			DefaultLife = 12000;
			DefaultDefense = 280000;
			Checker.Task = 21;
			AIStyle = 3;
			Types = ZombieTypes;
			DamagedIndex = 0.1f;
			CollideDamage = 200;
		}
		#endregion
		#region CheckSpawn
		protected override bool CheckSpawn(StarverPlayer player)
		{
			return base.CheckSpawn(player) && Rand.Next(StarverConfig.Config.TaskNow) > 12;
		}
		#endregion
		#region RealAI
		protected override void RealAI()
		{
			
		}
		#endregion
		#region OnDead
		public override void OnDead()
		{
			int idx = NewProj(Center, Microsoft.Xna.Framework.Vector2.Zero, ProjectileID.Explosives, 3000);
			//if (Rand.Next(100) > 2 * StarverConfig.Config.TaskNow)
			{
				Terraria.Main.projectile[idx].active = false;
				foreach (var ply in Starver.Players)
				{
					if (ply is null || !ply.Active)
					{
						continue;
					}
					if (Microsoft.Xna.Framework.Vector2.Distance(TargetPlayer.Center, Center) < 16 * 15)
					{
						ply.Damage((int)(3000 * DamageIndex));
					}
				}
			}
		}
		#endregion
		#region OnSpawn
		public override void OnSpawn()
		{
			base.OnSpawn();
		}
		#endregion
	}
}
