namespace hello_asp_identity.Mappings;

using AutoMapper;
using hello_asp_identity.Contracts.V1.Requests;
using hello_asp_identity.Domain;

public class RequestToDomainProfile : Profile
{
    public RequestToDomainProfile()
    {
        CreateMap<PaginationQuery, PaginationFilter>();
        CreateMap<GetAllUsersQuery, GetAllUsersFilter>();
    }
}