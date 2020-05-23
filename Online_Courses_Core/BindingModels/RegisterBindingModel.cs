using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Courses_Core.BindingModels
{
    public class RegisterBindingModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsApproved { get; set; }
        public string Role { get; set; }
        public string MobileNumber { get; set; }
        public string ImagePath { get; set; }
        public string Address { get; set; }
        //public virtual List<UserCourse> UserCourse { get; set; }
    }
}
