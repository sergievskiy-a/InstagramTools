using System.Threading.Tasks;
using AutoMapper;
using InstagramTools.Api.API.Builder;
using InstagramTools.Core.Interfaces;
using InstagramTools.Core.Models;
using InstagramTools.Data;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace InstagramTools.Core.Implemenations
{
	public class AuthorizeService : MainService<AuthorizeService>, IAuthorizeService
	{
		private readonly IAdminUserService _adminUserService;

        public AuthorizeService(IConfigurationRoot root, ILogger<AuthorizeService> logger,
                    IMemoryCache memoryCache, IMapper mapper, InstagramToolsContext context,
                    IInstaApiBuilder apiBuilder, IAdminUserService adminUserService)
            : base(root, logger, memoryCache, mapper, context)
        {
            this._adminUserService = adminUserService;
        }


		public async Task<AppUser> ValidateUser(string login, string password)
		{
			var userByEmailOperationResult = await this._adminUserService.GetUserByUsernameAsync(login);
		    var userByEmail = userByEmailOperationResult.Model;
            return userByEmail.Password.Equals(password) ?
					userByEmail :
					null;
		}
	}
}
