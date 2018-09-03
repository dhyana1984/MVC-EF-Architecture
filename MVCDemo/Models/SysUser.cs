using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace MVCDemo.Models
{
    public class SysUser
    {   [Key]
        public int ID { set; get; }
    
        [Display(Name="用户名"),Column("LoginName"),StringLength(10,MinimumLength=1,ErrorMessage="名字长度在1-10之间。")]
        public string UserName { set; get; }
        public string Email { get; set; }

        public string Password { set; get; }
        public string Phone { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}",ApplyFormatInEditMode=true)]
        public DateTime CreateDate { get; set; }
        public virtual ICollection<SysUserRole> SysUserRoles { get; set; }

        public int? SysDepartmentID { get; set; }

        public virtual SysDepartment SysDepartment { get; set; }
    }
}