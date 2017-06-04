using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InstagramTools.Api.API.Builder;
using InstagramTools.Common;
using InstagramTools.Common.Helpers;
using InstagramTools.Core.Implemenations.Configurations;
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
    public class AdminRoleService : MainService<AdminRoleService>, IAdminRoleService
    {

        public AdminRoleService(IConfigurationRoot root, ILogger<AdminRoleService> logger,
                IMemoryCache memoryCache, IMapper mapper, InstagramToolsContext context,
                IInstaApiBuilder apiBuilder)
            : base(root, logger, memoryCache, mapper, context)
        {
        }

        #region Get

        //One

        public async Task<OperationResult<Role>> GetRoleAsync(string name)
        {
            throw new NotImplementedException();
            //try
            //{
            //    var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == name);
            //    if (role == null)
            //        throw new Exception($"Role: {name} not found");
            //    return new OperationResult<Role>(true, role);

            //}
            //catch (Exception ex)
            //{
            //    return new OperationResult<Role>(false, ex.Message);
            //}

        }


        //List

        public async Task<OperationResult<List<Role>>> GetRolesAsync()
        {
            throw new NotImplementedException();
            //try
            //{
            //    var roles = await _context.Roles.ToListAsync();
            //    return new OperationResult<List<Role>>(true, roles);
            //}
            //catch (Exception ex)
            //{
            //    return new OperationResult<List<Role>>(false, ex.Message);
            //}

        }

        #endregion

        #region Edit

        public async Task<OperationResult> AddRoleAsync(Role newRole)
        {
            throw new NotImplementedException();
            //try
            //{
            //    if (newRole == null)
            //        throw new ArgumentNullException(nameof(newRole));
            //    if (string.IsNullOrWhiteSpace(newRole.Name))
            //        throw new Exception("role name is empty");
            //    if (_context.Roles.Any(x => x.Name.Equals(newRole.Name)))
            //        throw new Exception($"role with id:{newRole.Name} is already exist");
            //    _context.Roles.Add(newRole);
            //    await _context.SaveChangesAsync();
            //    return new OperationResult(true);
            //}
            //catch (Exception ex)
            //{
            //    return new OperationResult(false, ex.Message);
            //}
        }

        public async Task<OperationResult> EditRoleAsync(Role role)
        {
            throw new NotImplementedException();
            //try
            //{
            //    if (role == null)
            //        throw new ArgumentNullException(nameof(role));
            //    if (string.IsNullOrWhiteSpace(role.Name))
            //        throw new Exception("Role name is empty");
            //    _context.Entry(role).State = EntityState.Modified;
            //    await _context.SaveChangesAsync();
            //    return new OperationResult(true);
            //}
            //catch (Exception ex)
            //{
            //    return new OperationResult(false, ex.Message);
            //}
        }

        public async Task<OperationResult> RemoveRoleAsync(string roleName)
        {
            throw new NotImplementedException();
            //try
            //{
            //    var roleToDelete = await _context.Roles.FirstOrDefaultAsync(x => x.Name == roleName);

            //    if (roleToDelete == null)
            //        throw new RoleNotFoundException(roleName);

            //    roleToDelete.Deleted = DateTime.Now;
            //    _context.Entry(roleToDelete).State = EntityState.Modified;
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
