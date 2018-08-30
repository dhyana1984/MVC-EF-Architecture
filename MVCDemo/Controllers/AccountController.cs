using MVCDemo.DAL;
using MVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCDemo.Controllers
{
    public class AccountController : Controller
    {
        private AccountContext db = new AccountContext();
        //
        // GET: /Account/
        public ActionResult Index()
        {
            return View(db.SysUsers);
        }

        public ActionResult Login()
        {
            ViewBag.LoginState = "登录前。。。";
            return View();
        
        }

        [HttpPost]
        public ActionResult Login(FormCollection fc)
        {
            string email = fc["inputEmail3"];
            string password = fc["inputPassword3"];
            var user = db.SysUsers.SingleOrDefault(t => t.Email == email && t.Password == password);
            if(user!=null)
            { 
                ViewBag.LoginState =email+ "登录后。。。";
            }
            else 
            {
                ViewBag.LoginState = email + "用户不存在！";
            }
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(FormCollection fc)
        {
            //获取表单数据
            string email = fc["inputEmail3"];
            string password = fc["inputPassword3"];

            //进行下一步处理，这里先改下文字
            ViewBag.LoginState = "注册账号 " + email;
            return View();
        }

        public ActionResult Details(int id)
        {
            SysUser sysUser = db.SysUsers.Find(id);
            return View(sysUser);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(SysUser user)
        {
            db.SysUsers.Add(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            SysUser user = db.SysUsers.SingleOrDefault(t => t.ID == id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(SysUser user)
        {
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            SysUser user = db.SysUsers.SingleOrDefault(t=>t.ID==id);
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            SysUser user = db.SysUsers.SingleOrDefault(t => t.ID == id);
            db.SysUsers.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

 

    }
}