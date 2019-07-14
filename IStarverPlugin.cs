using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers
{
	public interface IStarverPlugin
	{
		void Load();
		void UnLoad();
		bool Enabled { get; }
	}
}
