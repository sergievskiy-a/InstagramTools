using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using InstagramTools.Api.API.Builder;
using InstagramTools.Common.Helpers;
using InstagramTools.Common.Exceptions;
using InstagramTools.Core.Interfaces;
using InstagramTools.Core.Models;
using InstagramTools.Data;
using InstagramTools.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace InstagramTools.Core.Implemenations
{
    public class AdminUserService : MainService<AdminUserService>, IAdminUserService
    {
        public AdminUserService(IConfigurationRoot root, ILogger<AdminUserService> logger,
                IMemoryCache memoryCache, IMapper mapper, InstagramToolsContext context,
                IInstaApiBuilder apiBuilder)
            : base(root, logger, memoryCache, mapper, context)
        {
        }

        #region Get

        //One

        public async Task<OperationResult<AppUser>> GetUserByIdAsync(string id)
        {
            return await ProcessRequestAsync(async () =>
            {
                var userRow =
                   await _context.AppUsers.AsNoTracking().FirstOrDefaultAsync(s => s.Id == Guid.Parse(id));

                var user = _mapper.Map<AppUserRow, AppUser>(userRow);

                return new OperationResult<AppUser>(user);
            });
        }

        public async Task<OperationResult<AppUser>> GetUserByUsernameAsync(string username)
        {
            return await ProcessRequestAsync(async () =>
            {
                var userRow =
                   await _context.AppUsers.AsNoTracking().FirstOrDefaultAsync(s => s.Username.Equals(username));

                var user = _mapper.Map<AppUserRow, AppUser>(userRow);

                return new OperationResult<AppUser>(user);
            });
        }

        public async Task<OperationResult<AppUser>> GetUserByPredicateAsync(Expression<Func<AppUserRow, bool>> predicate)
        {
            return await ProcessRequestAsync(async () =>
            {
                var userRow =
                   await _context.AppUsers.AsNoTracking().FirstOrDefaultAsync(predicate);

                var user = _mapper.Map<AppUserRow, AppUser>(userRow);

                return new OperationResult<AppUser>(user);
            });
        }


        //List
        public async Task<OperationResult<List<AppUser>>> GetUsersAsync(PagingOptions pagingOptions)
        {
            return await ProcessRequestAsync(async () =>
            {
                var usersRows = await _context.AppUsers.AsNoTracking()
                   .OrderByDescending(x => x.Created)
                   .Skip(pagingOptions.Skip)
                   .Take(pagingOptions.Take)
                   .ToListAsync();

                var users = _mapper.Map<List<AppUserRow>, List<AppUser>>(usersRows);

                return new OperationResult<List<AppUser>>(users);
            });
        }

        public async Task<OperationResult<List<AppUser>>> GetUsersByPredicateAsync(PagingOptions pagingOptions, Expression<Func<AppUserRow, bool>> predicate)
        {
            return await ProcessRequestAsync(async () =>
            {
                var usersRows = await _context.AppUsers.AsNoTracking()
                   .Where(predicate)
                   .Skip(pagingOptions.Skip)
                   .Take(pagingOptions.Take)
                   .ToListAsync();

                var users = _mapper.Map<List<AppUserRow>, List<AppUser>>(usersRows);

                return new OperationResult<List<AppUser>>(users);
            });
        }

        #endregion


        #region Edit
        public async Task<OperationResult> AddUserAsync(AppUser newUser)
        {
            return await ProcessRequestAsync(async () =>
            {
                if (newUser == null)
                    throw new ArgumentNullException(nameof(newUser));
                if (string.IsNullOrWhiteSpace(newUser.Username))
                    throw new Exception("username is empty");
                if (string.IsNullOrWhiteSpace(newUser.Password))
                    throw new Exception("password is empty");
                if (_context.AppUsers.Any(x => x.Username.Equals(newUser.Username)))
                    throw new Exception($"user with username:{newUser.Id} is already exist");
                if (!_context.Roles.Any(x => x.Name.Equals(newUser.RoleId)))
                    throw new Exception($"role {newUser.RoleId} is not exist");

                var userRow = _mapper.Map<AppUser, AppUserRow>(newUser);
                _context.AppUsers.Add(userRow);
                await _context.SaveChangesAsync();
                return new OperationResult(true);
            });
        }

        public async Task<OperationResult> EditUserAsync(AppUser user)
        {
            return await ProcessRequestAsync(async () =>
            {
                if (user == null)
                    throw new ArgumentNullException(nameof(user));
                if (string.IsNullOrWhiteSpace(user.Username))
                    throw new Exception("username is empty");
                if (string.IsNullOrWhiteSpace(user.Password))
                    throw new Exception("password is empty");
                if (!_context.Roles.Any(x => x.Name.Equals(user.RoleId)))
                    throw new Exception($"role {user.RoleId} is not exist");
                
                var userRow = _mapper.Map<AppUser, AppUserRow>(user);
                _context.Entry(userRow).State = EntityState.Modified;
                
                await _context.SaveChangesAsync();
                return new OperationResult(true);
            });
        }

        public async Task<OperationResult> RemoveUserAsync(string id)
        {
            return await ProcessRequestAsync(async () =>
            {
                var userToDelete = await _context.AppUsers.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));

                if (userToDelete == null)
                    throw new UserNotFoundException(id);

                userToDelete.Deleted = DateTime.Now;
                _context.Entry(userToDelete).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return new OperationResult(true);
            });
        }

        #endregion
    }
}
