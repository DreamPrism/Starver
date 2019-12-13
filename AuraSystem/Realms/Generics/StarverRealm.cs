using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers.AuraSystem.Realms.Generics
{
	using Interfaces;
	using Terraria;

	public abstract class StarverRealm<T> : StarverRealm
		where T : IRealmConditioner, new()
	{
		protected T conditioner;
		protected StarverRealm(bool killByTimeLeft) : base(killByTimeLeft)
		{

		}

		public override void Start()
		{
			conditioner.Center = Center;
			conditioner.Start();
			base.Start();
		}
		public override void Kill()
		{
			conditioner.Kill();
			base.Kill();
		}
		public override bool IsCross(Entity entity)
		{
			return conditioner.IsCross(entity);
		}
		public override bool InRange(Entity entity)
		{
			return conditioner.InRange(entity);
		}
		protected override void SetDefault()
		{

		}
		protected override void InternalUpdate()
		{
			conditioner.Center = Center;
			conditioner.Update(TimeLeft);
		}
	}
}
