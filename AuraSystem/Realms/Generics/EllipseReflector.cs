using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Starvers.AuraSystem.Realms.Generics
{
	using Vector = TOFOUT.Terraria.Server.Vector2;
	public class EllipseReflector : IRealmReflector
	{
		protected const int BorderMax = 60;
		protected int[] Border;
		protected int projID;

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
		public Vector2 Center { get; set; }

		public bool AtBorder(Entity entity)
		{
			return InternalAtBorder((Vector)entity.position);
		}
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
					Type: projID,
					Damage: 0,
					KnockBack: 0,
					Owner: Main.myPlayer
					);
				Main.projectile[Border[i]].aiStyle = -2;
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
			Rotation += Math.PI / 120;
			Vector2 Pos;
			for (int i = 0; i < Border.Length; i++)
			{
				Pos = Center + RelativePosition( Math.PI * 2 / BorderMax * i);
				if (Main.projectile[Border[i]].active == false || Main.projectile[Border[i]].type != projID)
				{
					Border[i] = Utils.NewProj
						  (
						  position: Pos,
						  velocity: Vector2.Zero,
						  Type: projID,
						  Damage: 0,
						  KnockBack: 0,
						  Owner: Main.myPlayer
						  );
					Main.projectile[Border[i]].aiStyle = -2;
				}
				else
					Main.projectile[Border[i]].Center = Pos;
				Main.projectile[Border[i]].SendData();
			}
		}
		public void Reflect(Entity entity)
		{
			entity.velocity = CalcReflectedVelocity(entity);
		}

		public EllipseReflector()
		{
			projID = ProjectileID.Bat;
			ShaftA = 16 * 20;
			ShaftB = 16 * 30;
		}

		private Vector CalcReflectedVelocity(Entity entity)
		{
			Vector relativePos = (Vector)(entity.Center - Center);
			Vector vel = (Vector)entity.velocity;

			vel.Angle -= Rotation;
			relativePos.Angle -= Rotation;

			double alpha = Math.Atan2(relativePos.X / ShaftA, relativePos.Y / ShaftB);
			Vector u = (ShaftA * Math.Cos(alpha), ShaftB * Math.Sin(alpha));

			vel -= 2 * Vector.Dot(vel, u) * u / u.LengthSquared;

			vel.Angle += Rotation;
			return vel;
		}
		private bool InternalAtBorder(Vector Pos)
		{
			Pos -= (Vector)Center;
			Pos.Angle -= Rotation;
			Pos.X /= K;
			return Math.Abs(Pos.Length - ShaftB) <= 16 * 4;
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
		private bool CrossedRotated(Vector p1,Vector p2)
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
