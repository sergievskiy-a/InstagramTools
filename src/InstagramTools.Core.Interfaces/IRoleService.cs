using System.Collections.Generic;
using System.Threading.Tasks;
using InstagramTools.Common.Helpers;
using InstagramTools.Core.Models;

namespace InstagramTools.Core.Interfaces
{
	public interface IRoleService
	{
        //One
        Task<OperationResult<Role>> GetRoleAsync(string name);

        //List
        Task<OperationResult<List<Role>>> GetRolesAsync();

    }
}
