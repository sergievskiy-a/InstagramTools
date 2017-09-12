using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using InstagramTools.Api.API.Builder;
using InstagramTools.Common.Helpers;
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
    public class UserService : MainService<UserService>, IUserService
    {
        public UserService(IConfigurationRoot root, ILogger<UserService> logger,
                    IMemoryCache memoryCache, IMapper mapper, InstagramToolsContext context,
                    IInstaApiBuilder apiBuilder)
            : base(root, logger, memoryCache, mapper, context)
        {
        }

                #region Get

        // One
        public async Task<OperationResult<AppUser>> GetUserByIdAsync(int id)
        {
            return await this.ProcessRequestAsync(async () =>
            {
                var userRow =
                   await this.Context.AppUsers
                   .AsNoTracking()
                   .FirstOrDefaultAsync(s => s.Id == id && s.Deleted == null);

                var user = this.Mapper.Map<AppUserRow, AppUser>(userRow);

                return new OperationResult<AppUser>(user);
            });
        }

        public async Task<OperationResult<AppUser>> GetUserByUsernameAsync(string username)
        {
            return await this.ProcessRequestAsync(async () =>
            {
                var userRow =
                   await this.Context.AppUsers
                   .AsNoTracking()
                   .FirstOrDefaultAsync(s => s.Username.Equals(username) && s.Deleted == null);

                var user = this.Mapper.Map<AppUserRow, AppUser>(userRow);

                return new OperationResult<AppUser>(user);
            });
        }

        public async Task<OperationResult<AppUser>> GetUserByPredicateAsync(Expression<Func<AppUserRow, bool>> predicate)
        {
            return await this.ProcessRequestAsync(async () =>
            {
                var userRow =
                   await this.Context.AppUsers
                   .AsNoTracking()
                   .Where(s => s.Deleted == null)
                   .FirstOrDefaultAsync(predicate);

                var user = this.Mapper.Map<AppUserRow, AppUser>(userRow);

                return new OperationResult<AppUser>(user);
            });
        }


        // List
        public async Task<OperationResult<List<AppUser>>> GetUsersAsync(PagingOptions pagingOptions)
        {
            return await this.ProcessRequestAsync(async () =>
            {
                var usersRows = await this.Context.AppUsers
                    .AsNoTracking()
                   .OrderByDescending(x => x.Created)
                   .Where(s => s.Deleted == null)
                   .Skip(pagingOptions.Skip)
                   .Take(pagingOptions.Take)
                   .ToListAsync();

                var users = this.Mapper.Map<List<AppUserRow>, List<AppUser>>(usersRows);

                return new OperationResult<List<AppUser>>(users);
            });
        }

        public async Task<OperationResult<List<AppUser>>> GetUsersByPredicateAsync(PagingOptions pagingOptions, Expression<Func<AppUserRow, bool>> predicate)
        {
            return await this.ProcessRequestAsync(async () =>
            {
                var usersRows = await this.Context.AppUsers.AsNoTracking()
                   .Where(s => s.Deleted == null)
                   .Where(predicate)
                   .Skip(pagingOptions.Skip)
                   .Take(pagingOptions.Take)
                   .ToListAsync();

                var users = this.Mapper.Map<List<AppUserRow>, List<AppUser>>(usersRows);

                return new OperationResult<List<AppUser>>(users);
            });
        }

        #endregion

    }
}
