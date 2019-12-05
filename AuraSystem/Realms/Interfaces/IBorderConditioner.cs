using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace Starvers.AuraSystem.Realms.Interfaces
{
	/// <summary>
	/// 表示可设置边界类型的IRealmConditioner
	/// </summary>
	public interface IBorderConditioner : IRealmConditioner
	{
		public bool AtBorder(Entity entity);
		public int ProjID { get; set; }
	}
}
