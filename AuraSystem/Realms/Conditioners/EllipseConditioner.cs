using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Starvers.AuraSystem.Realms.Conditioners
{
	using Interfaces;
	using Vector = TOFOUT.Terraria.Server.Vector2;
	public class EllipseConditioner : IBorderConditioner
	{
		protected const int BorderMax = 60;
		protected int[] Border;
		protected double angleVel;

		/// <summary>
		/// a / b 的值
		/// </summary>
		protected float K => ShaftA / ShaftB;
		/// <summary>
		/// X轴上的半轴长
		/// </summary>
		public float ShaftA { get; set; }
		/// <summary>
		/// Y轴上的半轴长
		/// </summary>
		public float ShaftB { get; set; }
		/// <summary>
		/// 旋转度数(弧度制)
		/// </summary>
		public double Rotation { get; set; }
		/// <summary>
		/// 边界弹幕类型
		/// </summary>
		public int ProjID { get; set; }
		/// <summary>
		/// 弹幕的TimeLeft
		/// </summary>
		public int DefProjTimeLeft { get; set; }
		public Vector2 Center { get; set; }

		public bool InRange(Entity entity)
		{
			return InternalInRange((Vector)entity.Center);
		}

		public unsafe bool IsCross(Entity entity)
		{
			Vector* points = stackalloc Vector[4];
			Vector2* P = (Vector2*)points;
			P[0] = entity.Center - Center;
			points[1] = points[0] + (entity.width, 0);
			points[2] = points[0] + (entity.width, entity.height);
			points[3] = points[0] + (0, entity.height);

			int crossPointCount = 0;

			crossPointCount += Crossed(points[0], points[1]) ? 1 : 0; if (crossPointCount > 0) return true;
			crossPointCount += Crossed(points[1], points[2]) ? 1 : 0; if (crossPointCount > 0) return true;
			crossPointCount += Crossed(points[2], points[3]) ? 1 : 0; if (crossPointCount > 0) return true;
			crossPointCount += Crossed(points[3], points[0]) ? 1 : 0; if (crossPointCount > 0) return true;

			return false;
		}
		public virtual void Start()
		{
			Border = new int[BorderMax];
			for (int i = 0; i < Border.Length; i++)
			{
				Border[i] = Utils.NewProj
					(
					position: Center + RelativePosition(angle: Math.PI * 2 / BorderMax * i),
					velocity: Vector2.Zero,
					Type: ProjID,
					Damage: 0,
					KnockBack: 0,
					Owner: Main.myPlayer
					);
				Main.projectile[Border[i]].aiStyle = -2;
				if (DefProjTimeLeft != 0)
					Main.projectile[Border[i]].timeLeft = DefProjTimeLeft;
			}
		}
		public virtual void Kill()
		{
			for (int i = 0; i < Border.Length; i++)
			{
				Main.projectile[Border[i]].KillMeEx();
			}
		}
		public virtual void Update(int Timer)
		{
			Rotation += angleVel;
			Vector2 Pos;
			for (int i = 0; i < Border.Length; i++)
			{
				Pos = Center + RelativePosition(Math.PI * 2 / BorderMax * i);
				if (Main.projectile[Border[i]].active == false || Main.projectile[Border[i]].type != ProjID)
				{
					Border[i] = Utils.NewProj
						  (
						  position: Pos,
						  velocity: Vector2.Zero,
						  Type: ProjID,
						  Damage: 0,
						  KnockBack: 0,
						  Owner: Main.myPlayer
						  );
					Main.projectile[Border[i]].aiStyle = -2;
					if (DefProjTimeLeft != 0)
						Main.projectile[Border[i]].timeLeft = DefProjTimeLeft;
				}
				else
					Main.projectile[Border[i]].Center = Pos;
				Main.projectile[Border[i]].SendData();
			}
		}
		public bool AtBorder(Entity entity)
		{
			return InternalAtBorder((Vector)entity.position);
		}

		public EllipseConditioner()
		{
			ProjID = ProjectileID.Bat;
			ShaftA = 16 * 20;
			ShaftB = 16 * 30;
			angleVel = Math.PI / 120;
		}


		protected virtual bool InternalAtBorder(Vector Pos)
		{
			Pos -= (Vector)Center;
			Pos.Angle -= Rotation;
			Pos.X /= K;
			Vector r = Pos;
			r.Length = ShaftB;
			r.X *= K;
			Pos.X *= K;
			return Vector.Distance(Pos, r) <= 16 * 4;
		}

		private bool InternalInRange(Vector Pos)
		{
			Pos -= (Vector)Center;
			Pos.Angle -= Rotation;
			Pos.X /= K;
			return Pos.Length < ShaftB;
		}

		private bool Crossed(Vector point1, Vector point2)
		{
			point1.Angle -= Rotation;
			point2.Angle -= Rotation;
			return CrossedRotated(point1, point2);
		}
		/// <summary>
		/// 判断是否与两点线段相交(不计算旋转)
		/// </summary>
		private bool CrossedRotated(Vector p1, Vector p2)
		{
			throw new NotImplementedException();
		}

		private Vector RelativePosition(double angle)
		{
			Vector value = (ShaftA * Math.Cos(angle), ShaftB * Math.Sin(angle));
			value.Angle += Rotation;
			return value;
		}

	}
}
