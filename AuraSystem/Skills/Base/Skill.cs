using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;

namespace Starvers.AuraSystem.Skills.Base
{
	public abstract class Skill
	{
		public static Random Rand { get { return Starver.Rand; } }
		public string Name { get; private set; }
		public int Index { get; private set; }
		public int CD { get; protected set; }
		public int MP { get; protected set; }
		public int Lvl { get; protected set; }
		public bool BossBan { get; protected set; } = false;
		public string Author { get; protected set; } = "";
		public string Description { get; protected set; } = "";
		public string Text { get; private set; }
		public abstract void Release(StarverPlayer player, Vector2 vel);
		public Skill(int idx)
		{
			Index = idx;
			Name = GetType().Name;
		}
		protected void SetText()
		{
			Text += string.Format("创意来源:{0}\n", Author);
			Text += string.Format("CD:{0}s\n", CD);
			Text += string.Format("所需等级:{0}\n", Lvl);
			Text += string.Format("{0}", Description);
		}
	}
}
