using AutoMapper;

namespace RollerStore3rd.Contracts
{
    public class RollerProfile : Profile
    {
        public RollerProfile()
        {
            CreateMap<CreateRoller, RollerStore.Domain.Roller>()
                .ForMember(dest => dest.Name, memberOptions: opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Price, memberOptions: opt => opt.MapFrom(src => src.Price))
                .ReverseMap();

            CreateMap<RollerStore.Domain.Roller, Roller>()
               .ForMember(dest => dest.Id, memberOptions: opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.StoreId, memberOptions: opt => opt.MapFrom(src => src.StoreId))
               .ForMember(dest => dest.Name, memberOptions: opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.Price, memberOptions: opt => opt.MapFrom(src => src.Price));
        }
    }
}
