using BlueBoard.API.Contracts.Auth;
using BlueBoard.API.Contracts.Profile;
using BlueBoard.API.Contracts.Profile.Base;
using BlueBoard.API.Contracts.Trip;
using BlueBoard.API.Contracts.Trip.Base;
using BlueBoard.API.Contracts.Users;
using BlueBoard.Contract.Identity.Models;
using BlueBoard.Contract.Trip.Models;

namespace BlueBoard.API.Contracts
{
    public class ServiceProfile : AutoMapper.Profile
    {
        public ServiceProfile()
        {
            // Requests
            this.CreateMap<UpdateProfileRequest, UserModel>();
            this.CreateMap<CreateTripRequest, SlimTripInfo>();
            this.CreateMap<UpdateTripRequest, TripInfo>();
            this.CreateMap<CreateTripListRequest, SlimTripListInfo>();
            this.CreateMap<UpdateTripListRequest, TripListInfo>();

            // Responses
            this.CreateMap<AccessTokenModel, VerifyAccessResponse>();

            this.CreateMap<UserModel, ProfileResponse>();
            this.CreateMap<UserModel, GetProfileResponse>()
                .IncludeBase<UserModel, ProfileResponse>();
            this.CreateMap<UserModel, UpdateProfileResponse>()
                .IncludeBase<UserModel, ProfileResponse>();

            this.CreateMap<TripInfo, CreateTripResponse>();
            this.CreateMap<TripInfo, UpdateTripResponse>();
            this.CreateMap<TripInfo, GetTripResponse>();

            this.CreateMap<TripListInfo, TripListResponse>();
            this.CreateMap<CreateTripListResponse, TripListInfo>()
                .IncludeBase<TripListInfo, TripListResponse>();
            this.CreateMap<UpdateTripListResponse, TripListInfo>()
                .IncludeBase<TripListInfo, TripListResponse>();

            // Items
            this.CreateMap<SlimUserModel, SlimUserApiItem>();
            this.CreateMap<TripInfo, TripApiItem>();

        }
    }
}
