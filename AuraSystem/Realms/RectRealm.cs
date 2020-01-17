using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Microsoft.Xna.Framework;

namespace Starvers.AuraSystem.Realms
{
	public abstract class RectRealm : StarverRealm
	{
		public float Width
		{
			get;
			protected set;
		}
		public float Height
		{
			get;
			protected set;
		}

		public RectRealm(bool killByTimeLeft) : base(killByTimeLeft)
		{

		}

		public override bool InRange(Entity entity)
		{
			Vector2 u = entity.Center - Center;
			u.X = Math.Abs(u.X) + entity.width / 2;
			u.Y = Math.Abs(u.Y) + entity.height / 2;
			return u.X <= Width / 2 && u.Y <= Height / 2;
		}
		public override bool IsCross(Entity entity)
		{
			Vector2 u = entity.Center - Center;
			u.X = Math.Abs(u.X) + entity.width / 2;
			u.Y = Math.Abs(u.Y) + entity.height / 2;
			Vector2 v = u + new Vector2(entity.width, entity.height);
			return
				u.X <= Width / 2 && u.Y <= Height / 2 &&
				v.X >= Width / 2 && v.Y >= Height / 2;
		}
		/// <summary>
		/// 是否有重叠区域
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public bool HasOverlapping(Entity entity)
		{
			Vector2 u = entity.Center - Center;
			u.X = Math.Abs(u.X) - entity.width / 2;
			u.Y = Math.Abs(u.Y) - entity.height / 2;
			return u.X <= Width / 2 && u.Y <= Height / 2;
		}
	}
}
