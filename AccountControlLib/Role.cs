using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountControlLib
{
	public class Role
	{
		public int ID { get; set; }
		public Role()
		{
			
		}
		public Role(string name)
		{
			Name = name;
		}
		public string Name { get; set; }
		public override string ToString()
		{
			return Name;
		}
	}
}
