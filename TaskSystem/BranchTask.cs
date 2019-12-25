﻿using Starvers.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TShockAPI;

namespace Starvers.TaskSystem
{
	public abstract class BranchTask : ITask, ICloneable
	{
		public StarverPlayer TargetPlayer { get; }
		public string Description { get; protected set; }

		protected BranchTask(StarverPlayer player)
		{
			TargetPlayer = player;
		}

		public virtual void Start()
		{

		}
		public virtual void End()
		{

		}


		public virtual void Updating(int Timer)
		{

		}
		public virtual void Updated(int Timer)
		{

		}

		public virtual void StrikingNPC(NPCStrikeEventArgs args)
		{

		}
		public virtual void StrikedNPC(NPCStrikeEventArgs args)
		{

		}

		public virtual void ReleasingSkill(ReleaseSkillEventArgs args)
		{

		}
		public virtual void ReleasedSkill(ReleaseSkillEventArgs args)
		{

		}

		public virtual object Clone()
		{
			return MemberwiseClone();
		}
	}
}
