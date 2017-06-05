using System.Collections.Generic;
using AutoMapper;
using InstagramTools.Api.Common.Models.Models;
using InstagramTools.Core.Implemenations.Configurations;
using InstagramTools.Core.Models;
using InstagramTools.Core.Models.PostModels;
using InstagramTools.Core.Models.ProfileModels;
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

            CreateMap<InstLoginInfoRow, LoginInfo>();

            CreateMap<AppUserRow, AppUser>();
            CreateMap<RoleRow, Role>();
            CreateMap<FollowRequestRow, FollowRequest>();

            CreateMap<InstProfileRow, InstProfile>()
                .ForMember(dst => dst.ApiId,
                    x => x.MapFrom(src => src.ApiId))
                .ForAllOtherMembers(m => m.Ignore());

            #endregion

            #region InstApiModelsMaps
            
            //Profile
            CreateMap<InstaUser, InstProfile>()
                .ForMember(dst => dst.ApiId,
                x => x.MapFrom(src => src.Pk))
                .ForAllOtherMembers(m => m.Ignore());

            CreateMap<InstaUserList, List<InstProfile>>();

            CreateMap<InstaFriendshipStatus, FriendshipStatus>();

            //Media
            CreateMap<InstaMedia, MediaPost>()
                .ForMember(dst => dst.Likers,
                x => x.MapFrom(src => src.Likers));

            CreateMap<MediaImage, PostImage>();

            CreateMap<InstaCaption, PostCaption>();

            CreateMap<InstaUserTag, PostUserTag>();

            CreateMap<InstaPosition, Position>();

            #endregion

        }
    }
}
