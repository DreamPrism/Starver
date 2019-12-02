using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Microsoft.Xna.Framework;

namespace Starvers.AuraSystem.Realms
{
	using Vector = TOFOUT.Terraria.Server.Vector2;
	/// <summary>
	/// 表示一个领域(结界)
	/// </summary>
	public interface IRealm
	{
		/// <summary>
		/// 领域的中心
		/// </summary>
		public Vector2 Center { get; set; }
		/// <summary>
		/// 表示领域是否已消失(在Update后检测, 若为false则将其清除出链表)
		/// </summary>
		public bool Active { get; }
		/// <summary>
		/// 初始化
		/// </summary>
		public void Start();
		/// <summary>
		/// 清除一个领域
		/// </summary>
		public void Kill();
		/// <summary>
		/// 更新领域的状态，特效等
		/// </summary>
		public void Update();
		/// <summary>
		/// 判断一个Entity是否完全在领域内
		/// </summary>
		public bool InRange(Entity entity);
		/// <summary>
		/// 判断一个Entity是不是与领域相交(有重合区域但不完全在领域内)
		/// </summary>
		public bool IsCross(Entity entity);
	}
}
