using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCDemo.Models
{
    public class SysUser
    {
        public int ID { set; get; }

        public string UserName { set; get; }
        public string Email { get; set; }

        public string Password { set; get; }
        public string Phone { get; set; }

        public virtual ICollection<SysUserRole> SysUserRoles { get; set; }
    }
}