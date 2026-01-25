using AutoMapper;
using InsuranceAdministration.Core.DTOs.Soldier;
using InsuranceAdministration.Core.Entities.SoldierEntities;
namespace InsuranceAdministration.Core.MappingProfiles.Soldiers
{
    public class SoldierLeavesProfile : Profile
    {
        public SoldierLeavesProfile()
        {
            CreateMap<Soldier, SoldierLeavesDto>()
                .ForMember(dest => dest.SoldierId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.SoldierName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Assignment, opt => opt.MapFrom(src => src.Assignment))
                .ForMember(dest => dest.Leaves, opt => opt.MapFrom(src => src.Leaves));
        }
    }
}
