using System;
using System.Collections.Generic;
using System.Linq;
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
    public class RoleService : MainService<RoleService>, IRoleService
    {
        public RoleService(IConfigurationRoot root, ILogger<RoleService> logger,
                    IMemoryCache memoryCache, IMapper mapper, InstagramToolsContext context,
                    IInstaApiBuilder apiBuilder)
            : base(root, logger, memoryCache, mapper, context)
        {
        }

        
        //Single
        /// <summary>
        /// Return non deleted role by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual async Task<OperationResult<Role>> GetRoleAsync(string name)
        {
            throw new NotImplementedException();
            //try
            //{
            //    var role = await _context.Roles
            //        .AsNoTracking()
            //        .FirstOrDefaultAsync(r => r.Name == name && r.Deleted == null);
            //    if (role != null)
            //        return new OperationResult<Role>(true, _mapper.Map<RoleRow, Role>(role));
            //    return new OperationResult<Role>(false, "Role is not exist");

            //}
            //catch (Exception ex)
            //{
            //    return new OperationResult<Role>(false, ex.Message);
            //}

        }

        //List

        /// <summary>
        /// Return non deleted roles
        /// </summary>
        /// <returns></returns>
        public virtual async Task<OperationResult<List<Role>>> GetRolesAsync()
        {
            throw new NotImplementedException();
            //try
            //{
            //    var roles = await _context.Roles
            //        .AsNoTracking()
            //        .Where(r => r.Deleted == null)
            //        .ToListAsync();
            //    return new OperationResult<List<Role>>(true, _mapper.Map<List<RoleRow>, List<Role>>(roles));
            //}
            //catch (Exception ex)
            //{
            //    return new OperationResult<List<Role>>(false, ex.Message);
            //}
		}

	}
}
