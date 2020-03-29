using BlueBoard.API.Contracts.Auth;
using BlueBoard.API.Contracts.Profile;
using BlueBoard.Contract.Identity.Commands;
using BlueBoard.Contract.Identity.Models;
using BlueBoard.Persistence.Abstractions.Entities;

namespace BlueBoard.API.Contracts.Base
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            this.CreateMap<SignInRequest, SignInCommand>();
            this.CreateMap<SignUpRequest, SignUpCommand>();
            this.CreateMap<VerifyAccessRequest, VerifyAccessCommand>();
            this.CreateMap<AccessTokenModel, VerifyAccessResponse>();
            this.CreateMap<UserEntity, ProfileModel>();
            this.CreateMap<SlimProfileModel, UserEntity>();
            this.CreateMap<UpdateProfileRequest, UpdateCurrentProfileCommand>();
        }
    }
}
