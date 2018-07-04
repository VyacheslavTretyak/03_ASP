using AccountControlLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccountControl2.Controllers
{
    public class RoleController : Controller
    {
		private RoleRepository roles = RoleRepository.Instance;
		private UserRepository users = UserRepository.Instance;
		public ActionResult List()
        {
			return View(roles.GetAll() as List<Role>);
		}
		[HttpGet]
		public ActionResult Create()
		{
			ViewBag.roles = roles.GetAll() as List<Role>;
			ViewBag.role = new Role();
			return View();
		}
		[HttpPost]
		public ActionResult Create(Role role)
		{
			if (ModelState.IsValid)
			{				
				roles.Create(role);
				return RedirectToAction("List");
			}

			ViewBag.roles = roles.GetAll() as List<Role>;
			ViewBag.role = role;
			return View(role);
		}
		public ActionResult Edit(int? id)
		{
			int i = 0;
			if (id != null)
			{
				i = (int)id;
			}
			ViewBag.roles = roles.GetAll() as List<Role>;
			return View("Create", roles.Get(i));
		}
		[HttpPost]
		public ActionResult Edit(Role role)
		{
			if (ModelState.IsValid)
			{				
				roles.Update(role);
				return RedirectToAction("List");
			}

			ViewBag.roles = roles.GetAll() as List<Role>;
			ViewBag.user = role;
			return View("Create", role);
		}
		public ActionResult Delete(int? id)
		{
			int i = 0;
			if (id != null)
			{
				i = (int)id;
			}

			Role role = roles.Get((int)id);
			if((users.GetAll() as List<User>).Any(user => user.Role.ID == role.ID))
			{
				return RedirectToAction("List");
			}
			roles.Delete(role.ID);
			return RedirectToAction("List");
		}


	}
}