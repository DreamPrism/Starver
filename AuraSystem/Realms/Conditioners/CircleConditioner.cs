using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Microsoft.Xna.Framework;

namespace Starvers.AuraSystem.Realms.Conditioners
{
	using Interfaces;
    using static Starvers.AuraSystem.Realms.InternalShapes;
    using Vector = TOFOUT.Terraria.Server.Vector2;
	public class CircleConditioner  : IBorderConditioner
	{
		private const int Max = 60;
		private int[] Border;

		public CircleConditioner()
		{
			Radium = 16 * 25;
		}

		public int DefProjTimeLeft { get; set; }
		public int ProjID { get; set; }
		public Vector2 Center { get; set; }
		public float Radium { get; set; }

		public virtual bool AtBorder(Entity entity)
		{
			Vector2 v = entity.Center - Center;
			return Math.Abs(v.Length() - Radium) < 16 * 2.5f;
		}
		public virtual bool InRange(Entity entity)
		{
			return IsContained(entity.Hitbox, (Center, Radium));
		}
		public virtual bool IsCross(Entity entity)
		{
			return IsCrossed(entity.Hitbox, (Center, Radium));
		}

		public virtual void Start()
		{
			Border = new int[Max];
			for (int i = 0; i < Border.Length; i++)
			{
				Border[i] = Utils.NewProj(Center + Vector.FromPolar(Math.PI * 2 / 60 * i, Radium), default, ProjID, 1, 20, 255);
				Main.projectile[Border[i]].aiStyle = -2;
				if (DefProjTimeLeft != 0)
				{
					Main.projectile[Border[i]].timeLeft = DefProjTimeLeft;
				}
			}
		}

		public virtual void Kill()
		{

		}

		public virtual void Update(int Timer)
		{
			for (int i = 0; i < Border.Length; i++)
			{
				if (!Main.projectile[Border[i]].active || Main.projectile[Border[i]].type != ProjID)
				{
					Border[i] = Utils.NewProj(Center + Vector.FromPolar(Math.PI * 2 / 60 * i, Radium), default, ProjID, 1, 20, 255);
					Main.projectile[Border[i]].aiStyle = -2;
					if (DefProjTimeLeft != 0)
					{
						Main.projectile[Border[i]].timeLeft = DefProjTimeLeft;
					}
				}
				Main.projectile[Border[i]].SendData();
			}
		}

		#region Utils
		/// <summary>
		/// 判断是否有重叠
		/// </summary>
		/// <param name="rect"></param>
		/// <param name="circle"></param>
		/// <returns></returns>
		protected static bool IsOverlapping(Rectangle rect, Circle circle)
		{
			Vector h = (rect.Width / 2, rect.Height / 2);
			Vector v = new Vector
			{
				X = Math.Abs(circle.X - rect.Center.X),
				Y = Math.Abs(circle.Y - rect.Center.Y)
			};
			Vector u = new Vector
			{
				X = Math.Max(v.X - h.X, 0),
				Y = Math.Max(v.Y - h.Y, 0)
			};
			return u.Length <= circle.Radium;
		}
		/// <summary>
		/// 判断rect是否在circle内
		/// </summary>
		/// <param name="rect"></param>
		/// <param name="circle"></param>
		/// <returns></returns>
		protected static bool IsContained(Rectangle rect, Circle circle)
		{
			Vector v = new Vector
			{
				X = Math.Abs(circle.X - rect.Center.X),
				Y = Math.Abs(circle.Y - rect.Center.Y)
			};

			return v.Length <= circle.Radium;
		}
		/// <summary>
		/// 判断circle与rect相交但不被包含
		/// </summary>
		/// <param name="rect"></param>
		/// <param name="circle"></param>
		/// <returns></returns>
		protected static bool IsCrossed(Rectangle rect, Circle circle)
		{
			Vector v = new Vector
			{
				X = Math.Abs(circle.X - rect.Center.X),
				Y = Math.Abs(circle.Y - rect.Center.Y)
			};
			return v.Length > circle.Radium;
		}

		#region Wasted
		// 以下内容应该是用于矩形Realm才对

		
		#endregion

		#endregion
	}
}
