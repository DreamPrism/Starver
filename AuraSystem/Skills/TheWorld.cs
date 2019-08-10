using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers.AuraSystem.Skills
{
	using Base;
	using Microsoft.Xna.Framework;
    using System.Threading;

	public class TheWorld : Skill
	{
		#region Fields
		/// <summary>
		/// 暂停的微秒数
		/// </summary>
		protected const int StopTime = 5000;
		protected static int TimeToStop;
		#endregion
		#region ctor
		public TheWorld() : base(SkillID.TheWorld)
		{
			MP = 110;
			CD = 120;
			Author = "逍遥";
			Description = $"让所有怪物以及弹幕暂停一段时间({StopTime / 1000}s)";
			Lvl = 3000;
			SetText();
		}
		#endregion
		#region Release
		public override void Release(StarverPlayer player, Vector2 vel)
		{
			if (NPCSystem.StarverNPC.TheWorld < 0)
			{
				new Thread(TheWorld_Release).Start();
			}
			else
			{
				TimeToStop = StopTime;
			}
		}
		#endregion
		#region Skill
		protected unsafe static void TheWorld_Release()
		{
			TimeToStop = StopTime;
			NPCSystem.StarverNPC.TheWorld++;
			Vector2* NPCVelocity = stackalloc Vector2[Terraria.Main.maxNPCs];
			Vector2* ProjVelocity = stackalloc Vector2[Terraria.Main.maxProjectiles];
			int* NPCAI = stackalloc int[Terraria.Main.maxNPCs];
			int* ProjAI = stackalloc int[Terraria.Main.maxProjectiles];
			ReadNPCState(NPCVelocity, NPCAI);
			ReadProjState(ProjVelocity, ProjAI);
			int Timer = 0;
			int sleepTime = 50;
			while (Timer < (TimeToStop) / sleepTime)
			{
				Timer++;
				UpdateNPCState();
				UpdateProjState();
				Thread.Sleep(sleepTime);
			}
			RestoreNPCState(NPCVelocity, NPCAI);
			RestoreProjState(ProjVelocity, ProjAI);
			NPCSystem.StarverNPC.TheWorld--;
		}
		#endregion
		#region Methods
		#region ReadNPC
		protected unsafe static void ReadNPCState(Vector2* NPCVelocity, int* NPCAI)
		{
			int t = 0;
			foreach (var npc in Terraria.Main.npc)
			{
				if (!npc.active)
				{
					continue;
				}
				NPCVelocity[t] = npc.velocity;
				NPCAI[t++] = npc.aiStyle;
				npc.SendData();
			}
		}
		#endregion
		#region ReadProj
		protected unsafe static void ReadProjState(Vector2* ProjVelocity, int* ProjAI)
		{
			int t = 0;
			foreach (var proj in Terraria.Main.projectile)
			{
				if (!proj.active)
				{
					continue;
				}
				ProjVelocity[t] = proj.velocity;
				ProjAI[t++] = proj.aiStyle;
				proj.SendData();
			}
		}
		#endregion
		#region UpdateNPC
		protected static void UpdateNPCState()
		{
			foreach (var npc in Terraria.Main.npc)
			{
				if (!npc.active)
				{
					continue;
				}
				npc.SendData();
			}
		}
		#endregion
		#region UpdateProj
		protected static void UpdateProjState()
		{
			foreach (var proj in Terraria.Main.projectile)
			{
				if (!proj.active)
				{
					continue;
				}
				proj.SendData();
			}
		}
		#endregion
		#region RestoreNPC
		protected unsafe static void RestoreNPCState(Vector2* NPCVelocity, int* NPCAI)
		{
			int t = 0;
			foreach (var npc in Terraria.Main.npc)
			{
				if (!npc.active)
				{
					continue;
				}
				npc.velocity = NPCVelocity[t];
				npc.aiStyle = NPCAI[t++];
				npc.SendData();
			}
		}
		#endregion
		#region RestoreProj
		protected unsafe static void RestoreProjState(Vector2* ProjVelocity, int* ProjAI)
		{
			int t = 0;
			foreach (var proj in Terraria.Main.projectile)
			{
				if (!proj.active)
				{
					continue;
				}
				proj.velocity = ProjVelocity[t];
				proj.aiStyle = ProjAI[t++];
				proj.SendData();
			}
		}
		#endregion
		#endregion
	}
}
