namespace darwin.Mappings;

using AutoMapper;
using darwin.Contracts.V1.Requests;
using darwin.Domain;

public class RequestToDomainProfile : Profile
{
    public RequestToDomainProfile()
    {
        CreateMap<PaginationQuery, PaginationFilter>();
        CreateMap<GetAllUsersQuery, GetAllUsersFilter>();
    }
}