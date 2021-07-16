using AutoMapper;

namespace RollerStore3rd.Contracts
{
    public class StoreProfile : Profile
    {
        public StoreProfile()
        {
            CreateMap<CreateStore, RollerStore.Domain.Store>()
                .ForMember(dest => dest.Name, memberOptions: opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Address, memberOptions: opt => opt.MapFrom(src => src.Address))
                .ReverseMap();

            CreateMap<RollerStore.Domain.Store, Store>()
               .ForMember(dest => dest.Id, memberOptions: opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Name, memberOptions: opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.Address, memberOptions: opt => opt.MapFrom(src => src.Address));
        }
    }
}
