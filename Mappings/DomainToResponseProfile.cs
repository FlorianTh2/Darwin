namespace hello_asp_identity.Mappings;

using AutoMapper;
using hello_asp_identity.Extensions;

public class DomainToResponseProfile : Profile
{
    public DomainToResponseProfile()
    {
        CreateMap<DateTime, string>().ConvertUsing(a => a.ToIso8601String());
        // CreateMap<Project, ProjectResponse>();
    }
}