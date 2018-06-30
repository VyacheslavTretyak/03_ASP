using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountControlLib
{
	class UserRepository : IRepository<User>
	{
		private static UserRepository instance = null;
		public static UserRepository Instance => instance ?? (instance = new UserRepository());

		private List<User> list = new List<User>();
		private UserRepository() { }

		public void Create(User item)
		{
			item.ID = (list.LastOrDefault()?.ID ?? 0) + 1;
			list?.Add(item);
		}

		public bool Delete(int id) => list?.Remove(Get(id)) ?? false;

		public User Get(int id) => list?.Find(user => user.ID == id);

		public IEnumerable<User> GetAll() => list;

		public void Update(User item)
		{
			throw new NotImplementedException();
		}

		public bool IsInit { get; set; } = false;
	}
}

