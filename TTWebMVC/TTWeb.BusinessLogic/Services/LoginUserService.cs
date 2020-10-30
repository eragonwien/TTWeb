﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models;
using TTWeb.Data.Database;
using TTWeb.Data.Models;

namespace TTWeb.BusinessLogic.Services
{

    public class LoginUserService : ILoginUserService
    {
        private readonly TTWebContext _context;
        private readonly IMapper _mapper;

        public LoginUserService(TTWebContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<LoginUserModel> CreateUserAsync(LoginUserModel loginUserModel)
        {
            if (loginUserModel is null) throw new ArgumentNullException(nameof(loginUserModel));

            var loginUser = new LoginUser
            {
                Email = loginUserModel.Email,
                FirstName = loginUserModel.FirstName,
                LastName = loginUserModel.LastName
            };

            await _context.LoginUsers.AddAsync(loginUser);
            await _context.SaveChangesAsync();

            return _mapper.Map<LoginUserModel>(loginUser);
        }

        public Task<LoginUserModel> GetOrAddUserAsync(LoginUserModel loginUserModel)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginUserModel> GetUserByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            var loginUser = await _context.LoginUsers
                .Include(u => u.LoginUserPermissionMappings)
                .FirstOrDefaultAsync(u => u.Email == email);

            return _mapper.Map<LoginUserModel>(loginUser);
        }
    }
}
