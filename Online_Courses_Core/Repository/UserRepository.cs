using Online_Courses_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Courses_Core.Repository
{
    public class UserRepository
    {
        Context _context;
        public UserRepository(Context context)
        {
            _context = context;
        }


        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
           var users= _context.Users.ToList();
            return users;
        }
    }
}
