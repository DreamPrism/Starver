﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers.AuraSystem.Skills
{
	using Base;
	using Microsoft.Xna.Framework;
    using System.Threading;
    using Terraria;
    using Terraria.ID;
    using Vector = TOFOUT.Terraria.Server.Vector2;
	using Circle = TOFOUT.Terraria.Server.Shapes.RoundRing;
    using System.Runtime.CompilerServices;

    public class NatureStorm : Skill
	{
		#region Fields
		private const float XDistance = 16 * 65;
		private const float YDistance = 16 * 30;
		private const int UpingGreenID = ProjectileID.TerraBeam;
		private const int ExStrikeID = ProjectileID.FlamingJack;
		private const int LeafType = ProjectileID.CrystalLeafShot;
		private readonly static Vector2 UpingVel = new Vector2(0, -27);
		#endregion
		#region ctor
		public NatureStorm() : base(SkillIDs.NatureStorm)
		{
			Level = 50000;
			MP = 8000;
			CD = 60 * 10;
			ForceCD = true;
		}
		#endregion
		#region Release
		public override void Release(StarverPlayer player, Vector2 vel)
		{
			unsafe
			{
				AsyncRelease(player, *(Vector*)&vel);
			}
		}
		private async void AsyncRelease(StarverPlayer player, Vector vel)
		{
			await Task.Run(() =>
			{
				int damage = (int)Math.Sqrt(player.Level);
				damage *= 20;
				damage += 1500;

				AsyncUpingGreen(player, player);

				Vector2 Distance;

				NPC target = null;

				foreach(var npc in Main.npc)
				{
					if(npc == null||!npc.active)
					{
						continue;
					}
					Distance = npc.Center - player.Center;
					Distance.X = Math.Abs(Distance.X);
					Distance.Y = Math.Abs(Distance.Y);
					#region SelectMainStrikeTarget
					if (target == null)
					{
						target = npc;
					}
					else if (target.life < npc.life) 
					{
						target = npc;
					}
					#endregion
					if(Distance.X < XDistance && Distance.Y < YDistance)
					{
						AsyncUpingGreen(player, npc);
					}
					Thread.Sleep(5);
				}
				target = target is null ? Main.npc[0] : target;

				Vector Direct = (Vector)(player.Center - target.Center);
				Direct.Length = 16 * 8f;

				Circle circle = new Circle((Vector)player.Center, 16 * 2.5f, 64);
				circle.Anchor += Direct;
				circle.Position -= Direct;
				vel.Length = 15;
				int[] projs = circle.CreateProjs(ExStrikeID, damage, vel, player);
				SetTarget(projs, target.whoAmI);
				damage /= 4;
				Vector2 HitPos = Main.projectile[projs[0]].Center;
				while (CheckProjs(projs))
				{
					#region playerEffect
					for (int i = 0; i < 4; i++)
					{
						Vector StartPos = (Vector)player.Center;
						StartPos.Y += player.TPlayer.width / 2;
						StartPos.X += Rand.Next(-2 * player.TPlayer.width, 2 * player.TPlayer.width);
						player.NewProj(StartPos, UpingVel, UpingGreenID, damage);
						Thread.Sleep(1);
					}
					#endregion
					AddLeaf(projs, player);
					HitPos = Main.projectile[projs[0]].Center;
				}
				if(target.active)
				{
					HitPos = target.Center;
				}
				Boom(HitPos, player);
			});
		}
		#endregion
		#region UpingGreen
		internal static void AsyncUpingGreen(StarverPlayer player, Entity target)
		{
			#region Wasted
			/*
			await Task.Run(() =>
			{
				int Timer = 0;
				int speed = 16;
				Vector velocity = new Vector(0, -speed);
				Vector StartPos = (Vector)target.Center;
				StartPos.Y += target.height / 4 + target.height / 2;
				Vector Line = new Vector(2 * target.width, 0);
				Line.X = Math.Min(16 * 6, Line.X);

				int Num = (int)(Line.Length / 4);

				int damage = (int)Math.Sqrt(player.Level);
				damage *= 5;
				damage += 900;


				while (Timer++ < 6)
				{
					player.ProjLine(StartPos + Line, StartPos - Line, velocity, Num, damage, UpingGreenID);
					Thread.Sleep(3 * speed);
				}
			});
			*/
			#endregion
			AsyncUpingGreen(player, target.Center, target.height, target.width);
		}
		internal static async void AsyncUpingGreen(StarverPlayer player, Vector2 Center, int Height,int Width)
		{
			await Task.Run(() =>
			{
				int Timer = 0;
				int speed = 18;
				Vector velocity = new Vector(0, -speed);
				Vector StartPos = (Vector)Center;
				StartPos.Y += Height / 4 + Height / 2;
				Vector Line = new Vector(2 * Width, 0);
				Line.X = Math.Min(16 * 6, Line.X);

				int Num = (int)(Line.Length / 4);

				int damage = (int)Math.Sqrt(player.Level);
				damage *= 5;
				damage += 900;


				while (Timer++ < 10) 
				{
					player.ProjLine(StartPos + Line, StartPos - Line, velocity, Num, damage, UpingGreenID);
					Thread.Sleep(5 * speed);
				}
			});
		}
		#endregion
		#region CheckProjs
		private static bool CheckProjs(int[] indexes)
		{
			bool flag = true;
			foreach(var idx in indexes)
			{
				if(Main.projectile[idx].active == false || Main.projectile[idx].type != ExStrikeID)
				{
					flag = false;
					break;
				}
			}
			return flag;
		}
		#endregion
		#region AddLeaf
		private static void AddLeaf(int[] projs,StarverPlayer player)
		{
			int damage = (int)Math.Sqrt(player.Level);
			damage *= 4;
			damage += 500;
			//for (int i = 0; i < 4; i++)
			{
				foreach (var idx in projs)
				{
					player.NewProj(Main.projectile[idx].Center, Rand.NextVector2(6), LeafType, damage);
					Thread.Sleep(1);
				}
			}
		}
		#endregion
		#region SetTarget
		private static void SetTarget(int[] projs,int target)
		{
			foreach(var idx in projs)
			{
				Main.projectile[idx].ai[0] = target;
				NetMessage.SendData((int)PacketTypes.ProjectileNew, -1, -1, null, idx);
			}
		}
		#endregion
		#region Boom
		private static void Boom(Vector2 pos,StarverPlayer player)
		{
			int damage = (int)Math.Sqrt(player.Level);
			damage *= 20;
			damage += 1550;

			float radium = 2 * 16;

			player.ProjCircle(pos, radium / 2, 25, ProjectileID.DD2SquireSonicBoom, 32, damage);

			for (int i = 0; i < 4; i++)
			{
				player.ProjCircle(pos, radium, 0.1f, ProjectileID.PureSpray, (int)(radium * 2 * Math.PI / 36), damage / 2, 2);
				radium += 16 * 3f;
				Thread.Sleep(175);
			}
			player.ProjCircle(pos, 16 * 60, 25, ExStrikeID, 50, damage);
			AsyncUpingGreen(player, pos, 16 * 3, 16 * 30);
		}
		#endregion
	}
}
