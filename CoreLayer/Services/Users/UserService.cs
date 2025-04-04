﻿using CoreLayer.DTOs.Users;
using CoreLayer.Utilities;
using DataLayer.Context;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services.Users
{
    public class UserService : IUserService
    {
        private readonly BlogContext _context;
        public UserService(BlogContext context)
        {
            _context = context;
        }


        public OperationResult RegisterUser(UserRegisterDto registerDto)
        {
            var IsUserExist = _context.Users.Any(u => u.UserName == registerDto.UserName);
            if (IsUserExist)
                return OperationResult.Error("نام کاربری تکراری است");
            var passwordHash = registerDto.Password.EncodeToMd5();
            _context.Users.Add(new User()
            {
                FullName = registerDto.FullName,
                UserName = registerDto.UserName,
                IsDelete = false,
                Password = passwordHash,
                Role = UserRole.User,
                CreationDate = DateTime.Now
            });
            _context.SaveChanges(); 
            return OperationResult.Success();
        }
        public OperationResult LoginUser(LoginUserDto loginDto)
        {
            var PasswordHashed = loginDto.Password.EncodeToMd5();
            var IsUserExist = _context.Users.Any(u=> u.UserName==loginDto.UserName && u.Password== PasswordHashed);
            if(IsUserExist == false)
            {
                return OperationResult.NotFound();
            }
            return OperationResult.Success();
        }
    }
}
