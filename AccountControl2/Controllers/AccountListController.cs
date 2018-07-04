using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccountControlLib;

namespace AccountControl2.Controllers
{
    public class AccountListController : Controller
    {
		private UserRepository users = UserRepository.Instance;
		private RoleRepository roles= RoleRepository.Instance;
		public AccountListController()
		{
			InitRoles();
			InitUsers();
		}

		private void InitRoles()
		{
			roles= RoleRepository.Instance;
			if (roles.IsInit)
			{
				return;
			}
			roles.Create(new Role("Guest"));
			roles.Create(new Role("Admin"));
			roles.Create(new Role("Programmer"));
			roles.IsInit = true;
		}

		private void InitUsers()
		{
			users = UserRepository.Instance;
			if (users.IsInit)
			{
				return;
			}

			users.Create(new User()
			{
				FirstName = "Vyacheslav",
				LastName = "Tretyak",
				Adress = "Kryvyi Rih",
				Email = "tretyak1c@gmail.com",
				Password = "6ECE4FD51BC113942692637D9D4B860E",
				Role = roles.Get(1)
			});
			users.Create(new User()
			{
				FirstName = "Chack",
				LastName = "Norris",
				Adress = "Los Angeles",
				Email = "bestofthebest@gmail.com",
				Password = "0970FFE8F1B935F2E126C6D98B492F91",
				Role = roles.Get(2)
			});
			users.Create(new User()
			{
				FirstName = "Kostya",
				LastName = "Nonamed",
				Adress = "Kryvyi Rih",
				Email = "kostya@gmail.com",
				Password = "5608E1BF11D627C3CF09842375D41677",
				Role = roles.Get(3)
			});
			users.IsInit = true;
		}

		[HttpGet]
		public ActionResult List()
        {			
			return View(users.GetAll() as List<User>);			
        }	
		[HttpPost]
		public ActionResult List(List<User> model)
		{
			foreach (var item in model)
			{
				users.GetAll().First(u => u.ID == item.ID).Selected = item.Selected;				
			}
			return RedirectToAction("Delete");
		}
		public ActionResult Delete()
		{
			(users.GetAll() as List<User>).RemoveAll(i => i.Selected);
			return RedirectToAction("List");
		}		
		public ActionResult Edit(int? id)
		{
			int i = 0;
			if(id != null)
			{
				i = (int)id;
			}
			ViewBag.roles = roles.GetAll() as List<Role>;
			return View("Create", users.Get(i));
		}
		[HttpPost]
		public ActionResult Edit(User user)
		{
			if (ModelState.IsValid)
			{
				user.Role = roles.GetAll().First(r => r.Name == user.RoleString);
				users.Update(user);				
				return RedirectToAction("List");
			}

			ViewBag.roles = roles.GetAll() as List<Role>;
			ViewBag.user = user;
			return View("Create", user);
		}
		[HttpGet]
		public ActionResult Create()
		{			
			ViewBag.roles = roles.GetAll() as List<Role>;
			ViewBag.user = new User();
			return View();
		}
		[HttpPost]
		public ActionResult Create(User user)
		{
			if (ModelState.IsValid)
			{
				user.Role = roles.GetAll().First(r => r.Name == user.RoleString);
				users.Create(user);
				return RedirectToAction("List");
			}
			
			ViewBag.roles = roles.GetAll() as List<Role>;
			ViewBag.user = user;
			return View(user);
		}
	}
}