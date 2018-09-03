using MVCDemo.DAL;
using MVCDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace MVCDemo.Controllers
{
    public class UserRoleController : Controller
    {
        private AccountContext db = new AccountContext();
        //
        // GET: /UserRole/
        public ActionResult Index(int? id)
        {
            var viewModel = new UserRoleIndexData();
            viewModel.SysUsers = db.SysUsers
                .Include(t => t.SysDepartment)
                .Include(t => t.SysUserRoles.Select(ur => ur.SysRole))
                .OrderBy(t => t.UserName);
            if(id!=null)
            {
                ViewBag.UserID = id.Value;
                viewModel.SysUserRoles = viewModel.SysUsers.Where(t => t.ID == id.Value).Single().SysUserRoles;
                viewModel.SysRoles = (viewModel.SysUserRoles.Where(t => t.SysUserID == id.Value)).Select(t => t.SysRole);
            }
            return View(viewModel);
        }

   
	}

   
}