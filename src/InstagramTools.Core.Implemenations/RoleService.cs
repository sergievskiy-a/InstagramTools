using System;
using System.Collections.Generic;
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
        
        // One
        public async Task<OperationResult<Role>> GetRoleAsync(string name)
        {
            return await this.ProcessRequestAsync(async () =>
            {
                var roleRow = await this.Context.Roles
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Name == name);
                if (roleRow == null)
                    throw new Exception($"Role: {name} not found");
                var role = this.Mapper.Map<RoleRow, Role>(roleRow);

                return new OperationResult<Role>(role);
            });
        }


        // List
        public async Task<OperationResult<List<Role>>> GetRolesAsync()
        {
            return await this.ProcessRequestAsync(async () =>
            {
                var rolesRows = await this.Context.Roles.AsNoTracking().ToListAsync();

                var roles = this.Mapper.Map<List<RoleRow>, List<Role>>(rolesRows);

                return new OperationResult<List<Role>>(roles);
            });
        }


	}
}
