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
                new SysUser{UserName="Tom",Password="1",Email="Tom@sohu.com",Phone="111"},
                new SysUser{UserName="Jerry",Password="2",Email="Jerry@sohu.com",Phone="222"}
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

                var sysUserRole = new List<SysUserRole>
            {
                new SysUserRole{SysRoleID=1,SysUserID=1},
                new SysUserRole{SysRoleID=2,SysUserID=1},
    
            };
            sysUserRole.ForEach(t => context.SysUserRoles.Add(t));
            context.SaveChanges();
        }
    }
}