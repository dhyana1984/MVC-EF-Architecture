using MVCDemo.DAL;
using MVCDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using MVCDemo.Models;

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

        public ActionResult Edit(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SysUser user = db.SysUsers.Include(t => t.SysDepartment)
                                      .Include(t => t.SysUserRoles)
                                      .Where(t => t.ID == id).SingleOrDefault();
            //将用户所在的部门选出
            PopulateDepartmentsDropDownList(user.SysDepartmentID);
            //将某个用户下的所有角色取出
            PopulateAssigenedRoleData(user);
            if(user==null)
            {
                return HttpNotFound();
            }
            return View(user);
        }


        [HttpPost]
        public ActionResult Edit(int? id,string[] selectRoles)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userToUpdate = db.SysUsers.Include(t => t.SysUserRoles).Where(t => t.ID == id).SingleOrDefault();
            if (TryUpdateModel(userToUpdate,"",new string[]{"LoginName","Email","Password","CreateDate","SysDepartmentID"}))
            {
                try
                {
                    UpdateUserRoles(selectRoles, userToUpdate);
                    db.Entry(userToUpdate).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch(Exception)
                {
                    throw;
                }

            }
            //如果失败，重新绑定视图
            PopulateDepartmentsDropDownList(userToUpdate.SysDepartmentID); //将用户所在部门选出
            PopulateAssigenedRoleData(userToUpdate);//将某个用户下的所有角色选出
            return View(userToUpdate);

         
        }


        private void UpdateUserRoles(string[] selectRoles,SysUser userToUpdate)
        {
            using(AccountContext db2=new AccountContext())
            {
                //没有选择，全部清空
                if(selectRoles==null)
                {
                    var sysUserRoles = db2.SysUserRoles.Where(t => t.SysUserID == userToUpdate.ID).ToList();
                    foreach (var item in sysUserRoles)
                    {
                        db2.SysUserRoles.Remove(item);
                    }
                    db2.SaveChanges();
                    return;
                }

                //编辑后的角色
                var selectRolesHS = new HashSet<string>(selectRoles);
                //原来的角色
                var userRoles = new HashSet<int>(userToUpdate.SysUserRoles.Select(t => t.SysRoleID));

                foreach (var item in db.SysRoles)       
                {
                    //如果被选中，原来没有的要添加
                    if(selectRolesHS.Contains(item.ID.ToString()))
                    {
                        if(!userRoles.Contains(item.ID))
                        {
                            userToUpdate.SysUserRoles.Add(new SysUserRole { SysUserID = userToUpdate.ID, SysRoleID = item.ID });
                        }
                    }
                    else
                    {
                        if(userRoles.Contains(item.ID)) //如果没有选中，原来有的要去除
                        {
                            SysUserRole sysUserRole = db2.SysUserRoles.FirstOrDefault(t => t.SysRoleID == item.ID && t.SysUserID == userToUpdate.ID);

                            db2.SysUserRoles.Remove(sysUserRole);
                            db2.SaveChanges();
                        }
                    }
                }
            }
        }




        private void PopulateDepartmentsDropDownList(object selectedDepartment = null)
        {
            var departmentsQuery = from d in db.SysDepartments
                                   orderby d.DepartmentName
                                   select d;
            ViewBag.SysDepartmentID = new SelectList(departmentsQuery, "ID", "DepartmentName", selectedDepartment);

        }

        private void PopulateAssigenedRoleData(SysUser user)
        {
            var allRoles = db.SysRoles.ToList();
            var userRoles = new HashSet<int>(user.SysUserRoles.Select(t=>t.SysRoleID));
            var viewModel = new List<AssignedRoleData>();
            foreach (var item in allRoles)
            {
                viewModel.Add(new AssignedRoleData
                {
                    RoleId = item.ID,
                    RoleName = item.RoleName,
                    Assigned = userRoles.Contains(item.ID)
                });
            }
            ViewBag.Roles = viewModel;
        }

   
	}

   
}