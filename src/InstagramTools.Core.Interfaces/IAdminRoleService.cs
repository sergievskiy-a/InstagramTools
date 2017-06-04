using System.Collections.Generic;
using System.Threading.Tasks;
using InstagramTools.Common.Helpers;
using InstagramTools.Core.Models;

namespace InstagramTools.Core.Interfaces
{
	public interface IAdminRoleService
    {
        //One
        Task<OperationResult<Role>> GetRoleAsync(string name);

        //List
        Task<OperationResult<List<Role>>> GetRolesAsync();

        //Edit
        Task<OperationResult> AddRoleAsync(Role newRole);
        Task<OperationResult> EditRoleAsync(Role role);
        Task<OperationResult> RemoveRoleAsync(string roleName);

    }
}
