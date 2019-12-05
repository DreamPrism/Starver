using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Starvers.AuraSystem.Realms.Generics
{
	using Terraria;
	using Interfaces;
	using Vector = TOFOUT.Terraria.Server.Vector2;
	public class BlindingRealm<T> : StarverRealm
		where T : IBorderConditioner, new()
	{
		private const int Proj = ProjectileID.Bat;
		private const int Blind = BuffID.Obstructed;
		private const int MaxTimeLeft = 60 * 60 * 2;
		private T Conditioner;

		public BlindingRealm() : this(new T())
		{

		}

		public BlindingRealm(T conditioner) : base(true)
		{
			DefaultTimeLeft = MaxTimeLeft;
			Conditioner = conditioner;
			conditioner.ProjID = Proj;
		}

		public override void Kill()
		{
			base.Kill();
			Conditioner.Kill();
		}

		public override void Start()
		{
			base.Start();
			Conditioner.Center = Center;
			Conditioner.Start();
		}

		public override bool InRange(Entity entity)
		{
			return Conditioner.InRange(entity);
		}

		public override bool IsCross(Entity entity)
		{
			return Conditioner.IsCross(entity);
		}

		protected override void SetDefault()
		{

		}

		protected override void InternalUpdate()
		{
			Conditioner.Center = Center;
			Conditioner.Update(TimeLeft);
			if (TimeLeft % 60 == 0)
			{
				foreach (var player in Starver.Players)
				{
					if (player == null || !player.Active)
						continue;
					if (InRange(player))
					{
						player.SetBuff(Blind, 60);
					}
				}
			}

		}
	}
}
