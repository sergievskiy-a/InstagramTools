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

        public async Task<OperationResult<AppUser>> GetUserByIdAsync(Guid id)
        {
            throw new NotImplementedException();
            //try
            //{
            //    var currUser =
            //        await _context.AppUsers.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
            //    return new OperationResult<AppUser>(true, currUser);
            //}
            //catch (Exception ex)
            //{
            //    return new OperationResult<AppUser>(false, ex.Message);
            //}
        }

        public async Task<OperationResult<AppUser>> GetUserByUsernameAsync(string username)
        {
            throw new NotImplementedException();
            //try
            //{
            //    var currUser = await _context.AppUsers.AsNoTracking().FirstOrDefaultAsync(s => s.Username.Equals(username));
            //    return new OperationResult<AppUser>(true, currUser);
            //}
            //catch (Exception ex)
            //{
            //    return new OperationResult<AppUser>(false, ex.Message);
            //}
        }

        public async Task<OperationResult<AppUser>> GetUserByPredicateAsync(Expression<Func<AppUserRow, bool>> predicate)
        {
            throw new NotImplementedException();
            //try
            //{
            //    var currUser = await _context.AppUsers.AsNoTracking().FirstOrDefaultAsync(predicate);
            //    return new OperationResult<AppUser>(true, currUser);
            //}
            //catch (Exception ex)
            //{
            //    return new OperationResult<AppUser>(false, ex.Message);
            //}

        }


        //List

        public async Task<OperationResult<List<AppUser>>> GetUsersAsync(PagingOptions pagingOptions)
        {
            throw new NotImplementedException();
            //try
            //{
            //    var currUser = await _context.AppUsers.AsNoTracking()
            //        .OrderByDescending(x => x.CreateDateTime)
            //        .Skip(pagingOptions.Skip)
            //        .Take(pagingOptions.Take)
            //        .ToListAsync();
            //    return new OperationResult<List<AppUser>>(true, currUser);
            //}
            //catch (Exception ex)
            //{
            //    return new OperationResult<List<AppUser>>(false, ex.Message);
            //}
        }

        public async Task<OperationResult<List<AppUser>>> GetUsersByPredicateAsync(PagingOptions pagingOptions, Expression<Func<AppUserRow, bool>> predicate)
        {
            throw new NotImplementedException();
            //try
            //{
            //    var currUser = await _context.AppUsers.AsNoTracking()
            //        .Where(predicate)
            //        .Skip(pagingOptions.Skip)
            //        .Take(pagingOptions.Take)
            //        .ToListAsync();
            //    return new OperationResult<List<AppUser>>(true, currUser);
            //}
            //catch (Exception ex)
            //{
            //    return new OperationResult<List<AppUser>>(false, ex.Message);
            //}
        }

        #endregion


        #region Edit

        public async Task<OperationResult> AddUserAsync(AppUser newUser)
        {
            throw new NotImplementedException();
            //try
            //{
            //    if (newUser == null)
            //        throw new ArgumentNullException(nameof(newUser));
            //    if (string.IsNullOrWhiteSpace(newUser.Username))
            //        throw new Exception("username is empty");
            //    if (string.IsNullOrWhiteSpace(newUser.Password))
            //        throw new Exception("password is empty");
            //    if (_context.AppUsers.Any(x => x.Username.Equals(newUser.Username)))
            //        throw new Exception($"user with username:{newUser.Id} is already exist");
            //    if (!_context.Roles.Any(x => x.Name.Equals(newUser.RoleName)))
            //        throw new Exception($"role {newUser.RoleName} is not exist");
            //    _context.AppUsers.Add(newUser);
            //    await _context.SaveChangesAsync();
            //    return new OperationResult(true);
            //}
            //catch (Exception ex)
            //{
            //    return new OperationResult(false, ex.Message);
            //}
        }

        public async Task<OperationResult> EditUserAsync(AppUser user)
        {
            throw new NotImplementedException();
            //try
            //{
            //    if (user == null)
            //        throw new ArgumentNullException(nameof(user));
            //    if (string.IsNullOrWhiteSpace(user.Username))
            //        throw new Exception("username is empty");
            //    if (string.IsNullOrWhiteSpace(user.Password))
            //        throw new Exception("password is empty");
            //    if (!_context.Roles.Any(x => x.Name.Equals(user.RoleName)))
            //        throw new Exception($"role {user.RoleName} is not exist");
            //    _context.Entry(user).State = EntityState.Modified;
            //    await _context.SaveChangesAsync();
            //    return new OperationResult(true);
            //}
            //catch (Exception ex)
            //{
            //    return new OperationResult(false, ex.Message);
            //}
        }

        public async Task<OperationResult> RemoveUserAsync(int id)
        {
            throw new NotImplementedException();
            //try
            //{
            //    var userToDelete = await _context.AppUsers.FirstOrDefaultAsync(x => x.Id == id);

            //    if (userToDelete == null)
            //        throw new UserNotFoundException(id);

            //    userToDelete.Deleted = DateTime.Now;
            //    _context.Entry(userToDelete).State = EntityState.Modified;
            //    await _context.SaveChangesAsync();
            //    return new OperationResult(true);
            //}
            //catch (Exception ex)
            //{
            //    return new OperationResult(false, ex.Message);
            //}
        }

        #endregion
    }
}
