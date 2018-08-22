using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCDemo.Models
{
    public class SysUserRole
    {
        public int ID { set; get; }

        public int SysUserID { get; set; }

        public int SysRoleID { get; set; }

        public virtual SysUser SysUser { get; set; }

        public virtual SysRole SysRole { get; set; }
    }
}