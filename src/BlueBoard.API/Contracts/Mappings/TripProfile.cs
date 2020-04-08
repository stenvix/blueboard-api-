using BlueBoard.Contract.Trip.Models;
using BlueBoard.Persistence.Abstractions.Entities;

namespace BlueBoard.API.Contracts.Mappings
{
    public class TripProfile : AutoMapper.Profile
    {
        public TripProfile()
        {
            this.CreateMap<TripEntity, SlimTripModel>();

            this.CreateMap<TripEntity, TripModel>()
                .IncludeBase<TripEntity, SlimTripModel>();

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
        }
    }
}
