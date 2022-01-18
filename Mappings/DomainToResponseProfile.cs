namespace hello_asp_identity.Mappings;

using AutoMapper;

public class DomainToResponseProfile : Profile
{
    public DomainToResponseProfile()
    {
        CreateMap<DateTime, string>().ConvertUsing(a => a.ToString("o"));
        // CreateMap<Project, ProjectResponse>();
    }
}