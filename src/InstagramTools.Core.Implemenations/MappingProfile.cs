using System.Collections.Generic;
using AutoMapper;
using InstagramTools.Api.Common.Models.Models;
using InstagramTools.Core.Implemenations.Configurations;
using InstagramTools.Core.Models;
using InstagramTools.Core.Models.MessageModels;
using InstagramTools.Core.Models.PostModels;
using InstagramTools.Core.Models.ProfileModels;
using InstagramTools.Core.Models.TaskModels;
using InstagramTools.Data.Models;
using Microsoft.Extensions.Configuration;

namespace InstagramTools.Core.Implemenations
{
    public class MappingProfile : Profile
    {
        public MappingProfile(IConfigurationRoot root)
        {
            var configurator = new InstagramToolsConfigurations(root);

            #region DbContextModelsMaps


            CreateMap<Role, RoleRow>().ReverseMap();
            CreateMap<AppUser, AppUserRow>().ReverseMap();
            CreateMap<LoginInfo, InstLoginInfoRow>().ReverseMap();
            CreateMap<InstProfile, InstProfileRow>().ReverseMap();

            #endregion

        }
    }
}
