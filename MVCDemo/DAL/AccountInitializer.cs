using MVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCDemo.DAL
{
    public class AccountInitializer:DropCreateDatabaseIfModelChanges<AccountContext>
    {
        protected override void Seed(AccountContext context)
        {
            var sysUsers = new List<SysUser>
            {
                new SysUser{UserName="Tom",Password="1",Email="Tom@sohu.com"},
                new SysUser{UserName="Jerry",Password="2",Email="Jerry@sohu.com"}
            };
            sysUsers.ForEach(s => context.SysUsers.Add(s));
            context.SaveChanges();

            var sysRole = new List<SysRole>
            {
                new SysRole{RoleName="Administrator",RoleDesc="Administrator has full athorization."},
                new SysRole{RoleName="General Users",RoleDesc="General Users can access the shared data."}
            };
            sysRole.ForEach(s => context.SysRoles.Add(s));
            context.SaveChanges();
        }
    }
}