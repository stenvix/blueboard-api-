using BlueBoard.API.Contracts.Auth;
using BlueBoard.API.Contracts.Profile;
using BlueBoard.API.Contracts.Profile.Base;
using BlueBoard.API.Contracts.Users;
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
            this.CreateMap<UpdateProfileRequest, UserModel>();

            // Responses
            this.CreateMap<AccessTokenModel, VerifyAccessResponse>();
            this.CreateMap<UserModel, ProfileResponse>();

            this.CreateMap<UserModel, GetProfileResponse>()
                .IncludeBase<UserModel, ProfileResponse>();

            this.CreateMap<UserModel, UpdateProfileResponse>()
                .IncludeBase<UserModel, ProfileResponse>();


            // Models
            this.CreateMap<UserEntity, BaseUserModel>();

            this.CreateMap<UserEntity, SlimUserModel>()
                .IncludeBase<UserEntity, BaseUserModel>();

            this.CreateMap<UserEntity, UserModel>()
                .IncludeBase<UserEntity, SlimUserModel>();

            this.CreateMap<BaseUserModel, UserEntity>()
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
                });

            this.CreateMap<SlimUserModel, UserEntity>()
                .IncludeBase<BaseUserModel, UserEntity>()
                .ForMember(dest => dest.Id, src => src.Ignore());

            this.CreateMap<UserModel, UserEntity>()
                .IncludeBase<SlimUserModel, UserEntity>()
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

            this.CreateMap<SlimUserModel, SlimUserApiItem>();
        }
    }
}
