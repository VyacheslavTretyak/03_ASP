using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountControlLib
{
	class RoleRepository : IRepository<Role>
	{
		private static RoleRepository instance = null;
		public static RoleRepository Instance => instance ?? (instance = new RoleRepository());

		private List<Role> list = new List<Role>();
		private RoleRepository() { }

		public void Create(Role item) => list?.Add(item);


		public bool Delete(int id) => list?.Remove(Get(id)) ?? false;

		public Role Get(int id) => list?.Find(role => role.ID == id);

		public IEnumerable<Role> GetAll() => list;

		public void Update(Role item)
		{

		}

		public bool IsInit { get; set; } = false;
	}
}
