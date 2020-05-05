using BlueBoard.API.Contracts.Trip;
using BlueBoard.API.Contracts.Trip.Base;
using BlueBoard.Contract.Common.Models;
using BlueBoard.Contract.Trip.Commands;
using BlueBoard.Contract.Trip.Models;
using BlueBoard.Persistence.Abstractions.Entities;

namespace BlueBoard.API.Contracts.Mappings
{
    public class TripProfile : AutoMapper.Profile
    {
        public TripProfile()
        {
            // Requests
            this.CreateMap<CreateTripRequest, SlimTripModel>();
            this.CreateMap<UpdateTripRequest, TripModel>();

            // Responses
            this.CreateMap<TripModel, CreateTripResponse>();
            this.CreateMap<TripModel, UpdateTripResponse>();
            this.CreateMap<TripModel, GetTripResponse>();

            //Models
            this.CreateMap<TripEntity, SlimTripModel>();

            this.CreateMap<TripEntity, TripModel>()
                .IncludeBase<TripEntity, SlimTripModel>()
                .ForMember(dest=>dest.CreatedBy, src=>src.Ignore());

            this.CreateMap<SlimTripModel, TripEntity>()
                .ForMember(dest => dest.Name, src =>
                {
                    src.Condition(i => !string.IsNullOrEmpty(i.Name));
                    src.MapFrom(i => i.Name);
                })
                .ForMember(dest => dest.Description, src => src.MapFrom(i => i.Description))
                .ForMember(dest => dest.StartDate, src =>
                {
                    src.Condition(i => i.StartDate != default);
                    src.MapFrom(i => i.StartDate);
                })
                .ForMember(dest => dest.EndDate, src =>
                {
                    src.Condition(i => i.EndDate != default);
                    src.MapFrom(i => i.EndDate);
                });

            this.CreateMap<TripModel, TripEntity>()
                .IncludeBase<SlimTripModel, TripEntity>()
                .ForMember(dest => dest.Id, src => src.Ignore());


            this.CreateMap<TripModel, TripItem>();

            this.CreateMap<UserEntity, ParticipantModel>()
                .IncludeBase<UserEntity, SlimUserModel>();

            this.CreateMap<ParticipantModel, ParticipantItem>();
            this.CreateMap<ParticipantEntity, ParticipantModel>();

            this.CreateMap<AddParticipantCommand, ParticipantEntity>();
        }
    }
}
