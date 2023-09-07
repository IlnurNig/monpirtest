using AutoMapper;
using monpirtest.Model;
using monpirtest.DTO;

namespace monpirtest.Mapper;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<ObjectPir, ObjectPirDto>();
        CreateMap<ObjectPirDto, ObjectPir>();

        CreateMap<Project, ProjectDto>();
        CreateMap<ProjectDto, Project>();

        CreateMap<DocumentationPd, DocumentationPdDto>();
        CreateMap<DocumentationPdDto, DocumentationPd>();

        CreateMap<DocumentationRd, DocumentationRdDto>();
        CreateMap<DocumentationRdDto, DocumentationRd>();
    }
}