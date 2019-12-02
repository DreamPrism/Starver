using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;

namespace Starvers.AuraSystem.Realms
{
	public abstract class StarverRealm : IRealm
	{
		private bool UseTimeLeft;

		protected uint TimeLeft;

		public uint DefaultTimeLeft { get; set; }
		public Vector2 Center { get; set; }
		public bool Active { get; set; }

		public abstract bool InRange(Entity entity);
		public abstract bool IsCross(Entity entity);

		public virtual void Kill()
		{
			Active = false;
		}

		public virtual void Start()
		{
			Active = true;
			TimeLeft = DefaultTimeLeft;
			SetDefault();
		}

		public virtual void Update()
		{
			InternalUpdate();
			UpdateTimeLeft();
		}

		protected StarverRealm(bool useTimeLeft)
		{
			UseTimeLeft = useTimeLeft;
		}

		protected void UpdateTimeLeft()
		{
			if(!UseTimeLeft)
			{
				return;
			}
			if(--TimeLeft == 0)
			{
				Kill();
			}
		}

		protected abstract void SetDefault();
		protected abstract void InternalUpdate();
	}
}
