using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TTWeb.BusinessLogic.Exceptions;
using TTWeb.BusinessLogic.Models.Entities.LoginUser;
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
            if (!IsValidEmailAddress(loginUserModel.Email))
                throw new InvalidInputException(nameof(loginUserModel.Email));

            var loginUser = new LoginUser
            {
                Email = loginUserModel.Email,
                FirstName = loginUserModel.FirstName,
                LastName = loginUserModel.LastName
            };

            await _context.LoginUsers.AddAsync(loginUser);
            await _context.SaveChangesAsync();

            loginUserModel = _mapper.Map<LoginUserModel>(loginUser);
            return loginUserModel;
        }

        public async Task<LoginUserModel> GetUserByEmailAsync(string email)
        {
            if (!IsValidEmailAddress(email)) throw new InvalidInputException(nameof(email));

            var loginUser = await _context.LoginUsers
                .Include(u => u.LoginUserPermissionMappings)
                .FirstOrDefaultAsync(u => u.Email == email);

            return _mapper.Map<LoginUserModel>(loginUser);
        }

        private bool IsValidEmailAddress(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase,
                    TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}