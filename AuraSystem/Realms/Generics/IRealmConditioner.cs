using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace Starvers.AuraSystem.Realms.Generics
{
	public interface IRealmConditioner
	{
		public Vector2 Center { get; set; }
		public bool InRange(Entity entity);
		public bool IsCross(Entity entity);
		public void Start();
		public void Kill();
		public void Update(int Timer);
	}
}
