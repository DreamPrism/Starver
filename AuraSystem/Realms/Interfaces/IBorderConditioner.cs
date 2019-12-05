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
		/// <summary>
		/// 判断entity是否在边界上
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public bool AtBorder(Entity entity);
		/// <summary>
		/// 弹幕的默认TimeLeft
		/// </summary>
		public int DefProjTimeLeft { get; set; }
		/// <summary>
		/// 边界弹幕ID
		/// </summary>
		public int ProjID { get; set; }
	}
}
