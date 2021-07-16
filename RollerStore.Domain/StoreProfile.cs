using AutoMapper;

namespace RollerStore.Domain
{
    public class StoreProfile : Profile
    {
        public StoreProfile()
        {
            CreateMap<Store, RollerStore.Data.Entities.StoreEntity>()
                .ForMember(dest => dest.Id, memberOptions: opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, memberOptions: opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Address, memberOptions: opt => opt.MapFrom(src => src.Address))
                .ReverseMap();
        }
    }
}
