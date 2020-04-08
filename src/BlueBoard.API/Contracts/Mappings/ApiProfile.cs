using BlueBoard.API.Contracts.Auth;
using BlueBoard.API.Contracts.Profile;
using BlueBoard.API.Contracts.Profile.Base;
using BlueBoard.API.Contracts.Trip;
using BlueBoard.Contract.Identity.Commands;
using BlueBoard.Contract.Identity.Models;
using BlueBoard.Contract.Trip.Commands;
using BlueBoard.Contract.Trip.Models;

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

            this.CreateMap<CreateTripRequest, CreateTripCommand>()
                .ForMember(dest => dest.Trip, src => src.MapFrom(i => i));
            this.CreateMap<CreateTripRequest, SlimTripModel>();

            this.CreateMap<UpdateTripRequest, UpdateTripCommand>()
                .ForMember(dest => dest.Trip, src => src.MapFrom(i => i));
            this.CreateMap<UpdateTripRequest, TripModel>();

            this.CreateMap<TripModel, CreateTripResponse>();
            this.CreateMap<TripModel, UpdateTripResponse>();
        }
    }
}
