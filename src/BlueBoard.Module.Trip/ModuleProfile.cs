using AutoMapper;
using BlueBoard.Contract.Trip.Models;
using BlueBoard.Module.Trip.Repositories.Entities;

namespace BlueBoard.Module.Trip
{
    public class ModuleProfile : Profile
    {
        public ModuleProfile()
        {
            this.CreateMap<TripEntity, SlimTripInfo>();

            this.CreateMap<TripEntity, TripInfo>()
                .IncludeBase<TripEntity, SlimTripInfo>()
                .ForMember(dest=>dest.CreatedBy, src=>src.Ignore());

            this.CreateMap<SlimTripInfo, TripEntity>()
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

            this.CreateMap<TripInfo, TripEntity>()
                .IncludeBase<SlimTripInfo, TripEntity>()
                .ForMember(dest => dest.Id, src => src.Ignore());

        }

    }
}
