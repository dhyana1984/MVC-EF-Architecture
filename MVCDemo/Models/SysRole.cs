using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCDemo.Models
{
    public class SysRole
    {
        [Key]
        public int ID { get; set; }

        public string RoleName { get; set; }

        public string RoleDesc { get; set; }

        public ICollection<SysUserRole> SysUserRoles { get; set; }
 
    }
}