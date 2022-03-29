namespace Darwin.Mappings;

using AutoMapper;
using Darwin.Contracts.V1.Requests;
using Darwin.Domain;

public class RequestToDomainProfile : Profile
{
    public RequestToDomainProfile()
    {
        CreateMap<PaginationQuery, PaginationFilter>();
        CreateMap<GetAllUsersQuery, GetAllUsersFilter>();
    }
}