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
		private int cd;
		public const int MaxSlots = 5;
		public static Random Rand => Starver.Rand;
		public string Name { get; private set; }
		public int Index { get; private set; }
		public int CD
		{
			get => cd;
			protected set => cd = value * 60;
		}
		public int MP { get; protected set; }
		public int Lvl { get; protected set; }
		public bool ForceCD { get; protected set; }
		public bool BossBan { get; protected set; }
		public string Author { get; protected set; } = string.Empty;
		public string Description { get; protected set; } = string.Empty;
		public string Text { get; private set; } = string.Empty;
		public abstract void Release(StarverPlayer player, Vector2 vel);
		public Skill(int idx)
		{
			Index = idx;
			Name = GetType().Name;
		}
		protected void SetText()
		{
			StringBuilder sb = new StringBuilder(30);
			sb.AppendLine($"创意来源: {Author}");
			sb.AppendLine($"CD: {CD}s");
			sb.AppendLine($"所需等级: {Lvl}");
			sb.AppendLine($"MP: {MP}");
			sb.Append($"{Description}");
			Text = sb.ToString();
		}
	}
}
