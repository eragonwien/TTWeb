using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TTWeb.BusinessLogic.Exceptions;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.Data.Database;
using TTWeb.Data.Extensions;
using TTWeb.Data.Models;

namespace TTWeb.BusinessLogic.Services
{
    public class FacebookUserService : IFacebookUserService
    {
        private readonly TTWebContext _context;
        private readonly IMapper _mapper;
        private readonly IEncryptionHelper _encryptionHelper;

        private IQueryable<FacebookUser> BaseQuery =>
            _context.FacebookUsers
            .Include(u => u.Owner);

        public FacebookUserService(TTWebContext context, IMapper mapper, IEncryptionHelper encryptionHelper)
        {
            _context = context;
            _mapper = mapper;
            _encryptionHelper = encryptionHelper;
        }

        public async Task<FacebookUserModel> CreateAsync(FacebookUserModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var facebookUser = _mapper.Map<FacebookUser>(model);
            facebookUser.Password = _encryptionHelper.Encrypt(facebookUser.Password);

            await _context.FacebookUsers.AddAsync(facebookUser);
            await _context.SaveChangesAsync();

            model = _mapper.Map<FacebookUserModel>(facebookUser);
            model.Password = _encryptionHelper.Decrypt(model.Password);
            return model;
        }

        public async Task<FacebookUserModel> UpdateAsync(FacebookUserModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var facebookUser = await BaseQuery.FilterById(model.Id).SingleOrDefaultAsync();
            if (facebookUser == null) throw new ResourceNotFoundException(nameof(facebookUser), model.Id.ToString());
            if (facebookUser.OwnerId != model.OwnerId) throw new UnauthorizedAccessException();

            facebookUser = _mapper.Map(model, facebookUser);
            facebookUser.Password = _encryptionHelper.Encrypt(facebookUser.Password);

            await _context.SaveChangesAsync();

            model = _mapper.Map<FacebookUserModel>(facebookUser);
            model.Password = _encryptionHelper.Decrypt(model.Password);
            return model;
        }

        public async Task DeleteAsync(int id, int? ownerId)
        {
            var facebookUser = new FacebookUser { Id = id };
            if (ownerId.HasValue)
                facebookUser.OwnerId = ownerId.Value;

            _context.FacebookUsers.Attach(facebookUser);
            _context.FacebookUsers.Remove(facebookUser);

            await _context.SaveChangesAsync();
        }

        public async Task<FacebookUserModel> ReadByIdAsync(int id, int? ownerId)
        {
            var facebookUser = await BaseQuery
                .AsNoTracking()
                .FilterById(id)
                .FilterByOwnerId(ownerId)
                .SingleOrDefaultAsync();

            if (facebookUser != null)
                facebookUser.Password = _encryptionHelper.Decrypt(facebookUser.Password);

            return _mapper.Map<FacebookUserModel>(facebookUser);
        }

        public async Task<IEnumerable<FacebookUserModel>> Read()
        {
            var facebookUsers = await BaseQuery
                .AsNoTracking()
                .Select(u => _mapper.Map<FacebookUserModel>(u))
                .ToListAsync();

            facebookUsers.ForEach(u => u.Password = _encryptionHelper.Decrypt(u.Password));

            return facebookUsers;
        }
    }
}