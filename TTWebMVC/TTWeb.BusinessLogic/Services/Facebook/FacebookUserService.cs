using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TTWeb.BusinessLogic.Exceptions;
using TTWeb.BusinessLogic.Models.Entities.FacebookUser;
using TTWeb.BusinessLogic.Services.Encryption;
using TTWeb.Data.Database;
using TTWeb.Data.Models;

namespace TTWeb.BusinessLogic.Services.Facebook
{
    public class FacebookUserService : IFacebookUserService
    {
        private readonly TTWebContext _context;
        private readonly IMapper _mapper;
        private readonly IEncryptionHelper _encryptionHelper;

        public FacebookUserService(TTWebContext context, IMapper mapper, IEncryptionHelper encryptionHelper)
        {
            _context = context;
            _mapper = mapper;
            _encryptionHelper = encryptionHelper;
        }

        public async Task<FacebookUserModel> AddAsync(FacebookUserModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var facebookUser = _mapper.Map<FacebookUser>(model);
            facebookUser.Password = _encryptionHelper.Encrypt(facebookUser.Password);

            await _context.FacebookUsers.AddAsync(facebookUser);
            await _context.SaveChangesAsync();

            return _mapper.Map<FacebookUserModel>(facebookUser).ClearPassword();
        }

        public async Task<FacebookUserModel> UpdateAsync(FacebookUserModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var existingUserModel = await GetByIdAsync(model.Id);
            if (existingUserModel == null) throw new ResourceNotFoundException(nameof(existingUserModel), model.Id.ToString());

            existingUserModel = _mapper.Map(model, existingUserModel);
            var facebookUser = _mapper.Map<FacebookUser>(existingUserModel);
            facebookUser.Password = _encryptionHelper.Encrypt(facebookUser.Password);

            _context.FacebookUsers.Attach(facebookUser);
            await _context.SaveChangesAsync();

            model = _mapper.Map<FacebookUserModel>(facebookUser);
            return model;
        }

        public async Task DeleteAsync(int id, int loginUserId)
        {
            var existingUserModel = await GetByIdAsync(id);
            if (existingUserModel == null) throw new ResourceNotFoundException(nameof(existingUserModel), id.ToString());
            if (existingUserModel.OwnerId != loginUserId) throw new ResourceAccessDeniedException();

            var facebookUser = new FacebookUser{ Id = id };
            _context.FacebookUsers.Attach(facebookUser);
            _context.FacebookUsers.Remove(facebookUser);
            await _context.SaveChangesAsync();
        }

        public async Task<FacebookUserModel> GetByIdAsync(int id)
        {
            var facebookUser = await _context.FacebookUsers
                .Include(u => u.Owner)
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Id == id);

            facebookUser.Password = _encryptionHelper.Decrypt(facebookUser.Password);

            return _mapper.Map<FacebookUserModel>(facebookUser);
        }

        public async Task<IEnumerable<FacebookUserModel>> GetByOwnerAsync(int ownerId)
        {
            var facebookUsers = await _context.FacebookUsers
                .Include(u => u.Owner)
                .AsNoTracking()
                .Where(u => u.OwnerId == ownerId)
                .Select(u => _mapper.Map<FacebookUserModel>(u))
                .ToListAsync();
            
            facebookUsers.ForEach(u => u.Password = _encryptionHelper.Decrypt(u.Password));
            
            return facebookUsers;
        }
    }
}