using System;
using AutoMapper;

using InstagramTools.Api.Common.Models.Models;
using InstagramTools.Core.Implemenations.Configurations;
using InstagramTools.Core.Models;
using InstagramTools.Core.Models.ProfileModels;
using InstagramTools.Data.Models;
using Microsoft.Extensions.Configuration;

namespace InstagramTools.Core.Implemenations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Role, RoleRow>().ReverseMap();
            this.CreateMap<AppUser, AppUserRow>().ReverseMap();
            this.CreateMap<LoginInfo, InstLoginInfoRow>().ReverseMap();
            this.CreateMap<InstProfile, InstProfileRow>().ReverseMap();

            this.CreateMap<InstProfile, FollowRequest>()
                .ForMember(dest => dest.InstProfileId, opt => opt.MapFrom(src => src.Id))
                .ForAllOtherMembers(opt => opt.Ignore());

            this.CreateMap<InstProfile, FollowRequestRow>()
                .ForMember(dest => dest.InstProfileId, opt => opt.MapFrom(src => src.Id))
                .ForAllOtherMembers(opt => opt.Ignore());

            this.CreateMap<InstProfileRow, FollowRequestRow>()
                .ForMember(dest => dest.InstProfileId, opt => opt.MapFrom(src => src.Id))
                .ForAllOtherMembers(opt => opt.Ignore());

            this.CreateMap<InstProfileRow, FollowRequest>()
                .ForMember(dest => dest.InstProfileId, opt => opt.MapFrom(src => src.Id))
                .ForAllOtherMembers(opt => opt.Ignore());

            this.CreateMap<InstaUser, InstProfile>()
                .ForMember(dest => dest.ApiId, opt => opt.MapFrom(src => src.Pk))
                .ForMember(dest => dest.FriendshipStatus, opt => opt.MapFrom(src => src.FriendshipStatus))

                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.Deleted, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            this.CreateMap<InstaFriendshipStatus, FriendshipStatus>();

            this.CreateMap<FollowRequest, FollowRequestRow>();
        }
    }
}
