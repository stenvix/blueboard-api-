using BlueBoard.API.Contracts.Auth;
using BlueBoard.API.Contracts.Profile;
using BlueBoard.API.Contracts.Profile.Base;
using BlueBoard.Contract.Identity.Commands;
using BlueBoard.Contract.Identity.Models;

namespace BlueBoard.API.Contracts.Mappings
{
    public class ApiProfile : AutoMapper.Profile
    {
        public ApiProfile()
        {
            this.CreateMap<SignInRequest, SignInCommand>();
            this.CreateMap<SignUpRequest, SignUpCommand>();
            this.CreateMap<VerifyAccessRequest, VerifyAccessCommand>();
            this.CreateMap<UpdateProfileRequest, UpdateCurrentProfileCommand>()
                .ForMember(dest => dest.Profile, src => src.MapFrom(i => i));
            this.CreateMap<UpdateProfileRequest, SlimProfileModel>();

            this.CreateMap<AccessTokenModel, VerifyAccessResponse>();
            this.CreateMap<ProfileModel, ProfileResponse>();
            this.CreateMap<ProfileModel, GetProfileResponse>()
                .IncludeBase<ProfileModel, ProfileResponse>();
            this.CreateMap<ProfileModel, UpdateProfileResponse>()
                .IncludeBase<ProfileModel, ProfileResponse>();
        }
    }
}
