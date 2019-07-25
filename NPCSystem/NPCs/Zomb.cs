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
			NPCID.ZombieEskimo,
			NPCID.ZombieMushroom,
			NPCID.ZombiePixie,
			NPCID.ZombieSweater,
			NPCID.ZombieSuperman
		};
		#endregion
		#region ctor
		public Zomb()
		{
			Checker = SpawnChecker.ZombieLike;
			RawType = NPCID.Zombie;
			DefaultLife = 2000;
			Checker.Task = 21;
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
			RealNPC.aiStyle = 3;
		}
		#endregion
		#region OnDead
		public override void OnDead()
		{
			Terraria.Main.projectile[NewProj(Center, Microsoft.Xna.Framework.Vector2.Zero, ProjectileID.Explosives, 3000)].active = false;
			foreach(var ply in Starver.Players)
			{
				if(ply is null||!ply.Active)
				{
					continue;
				}
				if(Microsoft.Xna.Framework.Vector2.Distance(TargetPlayer.Center,Center) < 16 * 30)
				{
					ply.Damage(3000);
				}
			}
		}
		#endregion
		#region OnSpawn
		public override void OnSpawn()
		{
			base.OnSpawn();
			RawType = ZombieTypes[Rand.Next(ZombieTypes.Length)];
			RealNPC.aiStyle = 3;
			RealNPC.SetDefaults(RawType);
			RealNPC.life = DefaultLife;
			RealNPC.lifeMax = DefaultLife;
			RealNPC.defense = DefaultDefense;
			SendData();
		}
		#endregion
	}
}
