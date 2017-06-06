using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using InstagramTools.Common.Helpers;
using InstagramTools.Core.Models;
using InstagramTools.Data.Models;

namespace InstagramTools.Core.Interfaces
{
	public interface IUserService
	{
        //Single
        /// <summary>
        /// Return NOT DELETED user by ApiId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<OperationResult<AppUser>> GetUserByIdAsync(string id);

        /// <summary>
        /// Return NOT DELETED user by Email
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<OperationResult<AppUser>> GetUserByUsernameAsync(string username);

        /// <summary>
        /// Return NOT DELETED user by predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<OperationResult<AppUser>> GetUserByPredicateAsync(Expression<Func<AppUserRow, bool>> predicate);

        //List
        /// <summary>
        /// Return NOT DELETED users
        /// </summary>
        /// <param name="pagingOptions"></param>
        /// <returns></returns>
        Task<OperationResult<List<AppUser>>> GetUsersAsync(PagingOptions pagingOptions);
        /// <summary>
        /// Return NOT DELETED Users by predicate
        /// </summary>
        /// <param name="pagingOptions"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<OperationResult<List<AppUser>>> GetUsersByPredicateAsync(PagingOptions pagingOptions, Expression<Func<AppUserRow, bool>> predicate);

    }
}
