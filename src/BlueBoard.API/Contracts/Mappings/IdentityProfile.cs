using BlueBoard.API.Contracts.Auth;
using BlueBoard.API.Contracts.Profile;
using BlueBoard.API.Contracts.Profile.Base;
using BlueBoard.Contract.Common.Models;
using BlueBoard.Contract.Identity.Models;
using BlueBoard.Persistence.Abstractions.Entities;

namespace BlueBoard.API.Contracts.Mappings
{
    public class IdentityProfile : AutoMapper.Profile
    {
        public IdentityProfile()
        {
            // Requests
            this.CreateMap<UpdateProfileRequest, SlimProfileModel>();

            // Responses
            this.CreateMap<AccessTokenModel, VerifyAccessResponse>();
            this.CreateMap<ProfileModel, ProfileResponse>();

            this.CreateMap<ProfileModel, GetProfileResponse>()
                .IncludeBase<ProfileModel, ProfileResponse>();

            this.CreateMap<ProfileModel, UpdateProfileResponse>()
                .IncludeBase<ProfileModel, ProfileResponse>();


            // Models
            this.CreateMap<UserEntity, SlimUserModel>();

            this.CreateMap<UserEntity, SlimProfileModel>()
                .IncludeBase<UserEntity, SlimUserModel>();

            this.CreateMap<UserEntity, ProfileModel>()
                .IncludeBase<UserEntity, SlimProfileModel>();

            this.CreateMap<SlimProfileModel, UserEntity>()
                .ForMember(dest => dest.FirstName, src =>
                {
                    src.Condition(i => !string.IsNullOrEmpty(i.FirstName));
                    src.MapFrom(i => i.FirstName);
                })
                .ForMember(dest => dest.LastName, src =>
                {
                    src.Condition(i => !string.IsNullOrEmpty(i.LastName));
                    src.MapFrom(i => i.LastName);
                })
                .ForMember(dest => dest.Username, src =>
                {
                    src.Condition(i => !string.IsNullOrEmpty(i.Username));
                    src.MapFrom(i => i.Username);
                })
                .ForMember(dest => dest.Email, src =>
                {
                    src.Condition(i => !string.IsNullOrEmpty(i.Email));
                    src.MapFrom(i => i.Email);
                })
                .ForMember(dest => dest.Phone, src =>
                {
                    src.Condition(i => !string.IsNullOrEmpty(i.Phone));
                    src.MapFrom(i => i.Phone);
                });
        }
    }
}
