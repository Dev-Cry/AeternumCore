using AutoMapper;
using AeternumCore.Data.Entity;
using AeternumCore.Data.DataTransferObjects;

namespace AeternumCore.Data
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUserProfileEntity, ApplicationUserProfileDto>().ReverseMap();
            CreateMap<ApplicationUserEntity, ApplicationUserDto>().ForMember(dest => dest.Profile, opt => opt.MapFrom(src => src.Profile));
            CreateMap<ApplicationRoleEntity, ApplicationRoleDto>().ReverseMap();
            CreateMap<ApplicationRolePermissionEntity, ApplicationRolePermissionDto>().ReverseMap();
            CreateMap<ApplicationRoleClaimEntity, ApplicationRoleClaimDto>().ReverseMap();
            CreateMap<ApplicationAuditLogEntity, ApplicationAuditLogDto>().ReverseMap();
        }
    }
}
