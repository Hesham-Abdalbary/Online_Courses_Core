using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Courses_Core.BindingModels
{
    public class UserModel
    {
        //private UserModel()
        //{

        //}
        //public UserModel(string userName, string password)
        //{
        //    UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        //    Password = password ?? throw new ArgumentNullException(nameof(password));
        //}
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
