using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers.AuraSystem
{
	public class AuraSkillWeapon
	{
		public int Item { get; private set; }
		public int Proj { get; private set; }
		public int Cost { get; private set; }
		public AuraSkillWeapon(int item,int proj,int cost)
		{
			Item = item;
			Proj = proj;
			Cost = cost;
		}
	}
}
