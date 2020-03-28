using AutoMapper;
using BlueBoard.API.Contracts.Auth;
using BlueBoard.Contract.Identity.Commands;
using BlueBoard.Contract.Identity.Models;

namespace BlueBoard.API.Contracts.Base
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<SignInRequest, SignInCommand>();
            this.CreateMap<SignUpRequest, SignUpCommand>();
            this.CreateMap<VerifyAccessRequest, VerifyAccessCommand>();
            this.CreateMap<AccessTokenModel, VerifyAccessResponse>();
        }
    }
}
