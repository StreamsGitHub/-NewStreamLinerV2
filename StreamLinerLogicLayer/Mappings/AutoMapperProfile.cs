using AutoMapper;
using StreamLinerEntitiesLayer.Entities;
using StreamLinerViewModelLayer.ModelDTO;

namespace StreamLinerLogicLayer.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Projects, ProjectDto>();
            CreateMap<ProjectDto, Projects>();
            // CreateMap<Field, FieldDTO>();
            CreateMap<Field, FieldDTO>()
                .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.Type.Type))
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.TypeId));

            CreateMap<FieldDTO, Field>();
            CreateMap<getDocumentsDTO, Document>();
            CreateMap<Document, getDocumentsDTO>();

            CreateMap<MeetingRoom, MeetingRoomDTO>();
            CreateMap<MeetingRoomDTO, MeetingRoom>();

            CreateMap<ApplicationUser, UsersDtoResult>();
            CreateMap<UsersDtoResult , ApplicationUser>();

            CreateMap<getfolderDTO, Folder>();
            CreateMap<Folder, getfolderDTO>();

        }
    }
}
