using System.Threading.Tasks;
using InstagramTools.Core.Models;

namespace InstagramTools.Core.Interfaces
{
	public interface IAuthorizeService
	{
		Task<AppUser> ValidateUser(string login, string password);
	}
}
