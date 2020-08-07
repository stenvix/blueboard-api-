using AutoMapper;
using BlueBoard.Contract.Identity.Models;
using BlueBoard.Module.Identity.Repositories.Entities;

namespace BlueBoard.Module.Identity
{
    public class ModuleProfile : Profile
    {
        public ModuleProfile()
        {
            this.CreateMap<UserEntity, BaseUserModel>();

            this.CreateMap<UserEntity, SlimUserModel>()
                .IncludeBase<UserEntity, BaseUserModel>();

            this.CreateMap<UserEntity, UserModel>()
                .IncludeBase<UserEntity, SlimUserModel>();

            this.CreateMap<BaseUserModel, UserEntity>()
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
                });

            this.CreateMap<SlimUserModel, UserEntity>()
                .IncludeBase<BaseUserModel, UserEntity>()
                .ForMember(dest => dest.Id, src => src.Ignore());

            this.CreateMap<UserModel, UserEntity>()
                .IncludeBase<SlimUserModel, UserEntity>()
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
