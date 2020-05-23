using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Online_Courses_Core.BindingModels;
using Online_Courses_Core.Models;
using Online_Courses_Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Courses_Core.Service
{
    public class UserService
    {
        private UserRepository _repo;
        UserManager<User> _userManager;
        SignInManager<User> _signInManager;
        public UserService(UserRepository repo, UserManager<User> userManager,
        SignInManager<User> signInManager)
        {
            _repo = repo;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task AddUser(RegisterBindingModel registerBindingModel)
        {
            var user = new User(registerBindingModel.UserName, registerBindingModel.Email, registerBindingModel.Password);
            var result = await _userManager.CreateAsync(user, registerBindingModel.Password);

            if (!result.Succeeded)
                throw new Exception(result.Errors.FirstOrDefault()?.Description);
        }

        public IEnumerable<User> getAllUsers()
        {
           var users = _repo.GetAll();
            return users;
        }
            

    }
}
