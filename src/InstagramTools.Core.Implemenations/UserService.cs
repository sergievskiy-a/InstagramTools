using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using InstagramTools.Api.API.Builder;
using InstagramTools.Common.Helpers;
using InstagramTools.Core.Interfaces;
using InstagramTools.Core.Models;
using InstagramTools.Data;
using InstagramTools.Data.Models;
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

        //Single

        /// <summary>
        /// Return NOT DELETED user by ApiId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<OperationResult<AppUser>> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
            //try
            //{
            //    var currUser =
            //        await _context.AppUsers.AsNoTracking()
            //        .Where(u => u.Deleted == null)
            //        .FirstOrDefaultAsync(s => s.Id == id);
            //    return new OperationResult<AppUser>(true, _mapper.Map<UserRow, User>(currUser));
            //}
            //catch (Exception ex)
            //{
            //    return new OperationResult<AppUser>(false, ex.Message);
            //}
        }

        /// <summary>
        /// Return NOT DELETED user by predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<OperationResult<AppUser>> GetUserByPredicateAsync(Expression<Func<AppUserRow, bool>> predicate)
        {
            throw new NotImplementedException();
            //try
            //{
            //    var currUser = await _context.Users.AsNoTracking()
            //        .Where(u => u.Deleted == null)
            //        .FirstOrDefaultAsync(predicate);
            //    return new OperationResult<AppUser>(true, _mapper.Map<UserRow, User>(currUser));
            //}
            //catch (Exception ex)
            //{
            //    return new OperationResult<AppUser>(false, ex.Message);
            //}
        }

        /// <summary>
        /// Return NOT DELETED user by Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<OperationResult<AppUser>> GetUserByUsernameAsync(string email)
        {
            throw new NotImplementedException();
            //try
            //{
            //    var currUser = await _context.Users.AsNoTracking()
            //        .Where(u => u.Deleted == null)
            //        .FirstOrDefaultAsync(s => s.Username.Equals(email));
            //    return new OperationResult<AppUser>(true, _mapper.Map<UserRow, User>(currUser));
            //}
            //catch (Exception ex)
            //{
            //    return new OperationResult<AppUser>(false, ex.Message);
            //}
        }

        //List

        /// <summary>
        /// Return NOT DELETED users
        /// </summary>
        /// <param name="pagingOptions"></param>
        /// <returns></returns>
        public async Task<OperationResult<List<AppUser>>> GetUsersAsync(PagingOptions pagingOptions)
        {
            throw new NotImplementedException();
            //try
            //{
            //    var users = await _context.Users.AsNoTracking()
            //        .Where(u => u.Deleted == null)
            //        .OrderByDescending(x => x.CreateDateTime)
            //        .Skip(pagingOptions.Skip)
            //        .Take(pagingOptions.Take)
            //        .ToListAsync();

            //    return new OperationResult<List<AppUser>>(true, _mapper.Map<List<UserRow>, List<AppUser>>(users));
            //}
            //catch (Exception ex)
            //{
            //    return new OperationResult<List<AppUser>>(false, ex.Message);
            //}
        }

        /// <summary>
        /// Return NOT DELETED users by predicate
        /// </summary>
        /// <param name="pagingOptions"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<OperationResult<List<AppUser>>> GetUsersByPredicateAsync(PagingOptions pagingOptions, Expression<Func<AppUserRow, bool>> predicate)
        {
            throw new NotImplementedException();
            //try
            //{
            //    var users = await _context.Users.AsNoTracking()
            //        .Where(u => u.Deleted == null)
            //        .Where(predicate)
            //        .Skip(pagingOptions.Skip)
            //        .Take(pagingOptions.Take)
            //        .ToListAsync();
            //    return new OperationResult<List<AppUser>>(true, _mapper.Map<List<UserRow>, List<AppUser>>(users));
            //}
            //catch (Exception ex)
            //{
            //    return new OperationResult<List<AppUser>>(false, ex.Message);
            //}

        }

    }
}
