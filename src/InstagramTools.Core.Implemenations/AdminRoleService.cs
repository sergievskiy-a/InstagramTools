using System;
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
            return await this.ProcessRequestAsync(async () =>
            {
                if (newRole == null)
                    throw new ArgumentNullException(nameof(newRole));
                if (string.IsNullOrWhiteSpace(newRole.Name))
                    throw new Exception("role name is empty");
                if (this.Context.Roles.Any(x => x.Name.Equals(newRole.Name)))
                    throw new Exception($"role with id:{newRole.Name} is already exist");

                var newRoleRow = this.Mapper.Map<Role, RoleRow>(newRole);

                this.Context.Roles.Add(newRoleRow);
                await this.Context.SaveChangesAsync();

                return new OperationResult(true);
            });
        }

        public async Task<OperationResult> EditRoleAsync(Role role)
        {
            return await this.ProcessRequestAsync(async () =>
            {
                if (role == null)
                    throw new ArgumentNullException(nameof(role));
                if (string.IsNullOrWhiteSpace(role.Name))
                    throw new Exception("Role name is empty");

                var roleRow = this.Mapper.Map<Role, RoleRow>(role);

                this.Context.Entry(roleRow).State = EntityState.Modified;
                await this.Context.SaveChangesAsync();

                return new OperationResult(true);
            });
        }

        #endregion

    }
}
