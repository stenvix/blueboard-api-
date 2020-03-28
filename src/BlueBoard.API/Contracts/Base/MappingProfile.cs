using AutoMapper;
using BlueBoard.API.Contracts.Auth;
using BlueBoard.Contract.Identity.Commands;

namespace BlueBoard.API.Contracts.Base
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            this.CreateMap<SignInRequest, SignInCommand>();
            this.CreateMap<SignUpRequest, SignUpCommand>();
        }
    }
}
