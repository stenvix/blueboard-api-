using BlueBoard.Contract.Identity.Models;
using BlueBoard.Persistence.Abstractions.Entities;

namespace BlueBoard.API.Contracts.Mappings
{
    public class ProfileProfile : AutoMapper.Profile
    {
        public ProfileProfile()
        {
            this.CreateMap<UserEntity, ProfileModel>()
                .IncludeBase<UserEntity, SlimProfileModel>();

            this.CreateMap<UserEntity, SlimProfileModel>();

            this.CreateMap<SlimProfileModel, UserEntity>()
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
                })
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
        }
    }
}
