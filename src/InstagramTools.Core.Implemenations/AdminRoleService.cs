using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InstagramTools.Api.API.Builder;
using InstagramTools.Common;
using InstagramTools.Common.Helpers;
using InstagramTools.Common.Exceptions;
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
    public class AdminRoleService : RoleService, IAdminRoleService
    {

        public AdminRoleService(IConfigurationRoot root, ILogger<AdminRoleService> logger,
                IMemoryCache memoryCache, IMapper mapper, InstagramToolsContext context,
                IInstaApiBuilder apiBuilder)
            : base(root, logger, memoryCache, mapper, context, apiBuilder)
        {
        }

        #region Edit
        public async Task<OperationResult> AddRoleAsync(Role newRole)
        {
            return await ProcessRequestAsync(async () =>
            {
                if (newRole == null)
                    throw new ArgumentNullException(nameof(newRole));
                if (string.IsNullOrWhiteSpace(newRole.Name))
                    throw new Exception("role name is empty");
                if (_context.Roles.Any(x => x.Name.Equals(newRole.Name)))
                    throw new Exception($"role with id:{newRole.Name} is already exist");

                var newRoleRow = _mapper.Map<Role, RoleRow>(newRole);

                _context.Roles.Add(newRoleRow);
                await _context.SaveChangesAsync();

                return new OperationResult(true);
            });
        }

        public async Task<OperationResult> EditRoleAsync(Role role)
        {
            return await ProcessRequestAsync(async () =>
            {
                if (role == null)
                    throw new ArgumentNullException(nameof(role));
                if (string.IsNullOrWhiteSpace(role.Name))
                    throw new Exception("Role name is empty");

                var roleRow = _mapper.Map<Role, RoleRow>(role);

                _context.Entry(roleRow).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return new OperationResult(true);
            });
        }

        #endregion

    }
}
