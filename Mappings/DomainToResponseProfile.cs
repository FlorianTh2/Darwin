namespace hello_asp_identity.Mappings;

using AutoMapper;
using hello_asp_identity.Contracts.V1.Responses;
using hello_asp_identity.Domain.Results;
using hello_asp_identity.Entities;
using hello_asp_identity.Extensions;

public class DomainToResponseProfile : Profile
{
    public DomainToResponseProfile()
    {
        CreateMap<DateTime, string>().ConvertUsing(a => a.ToIso8601String());
        CreateMap<AppUser, UserResponse>();
        CreateMap<RegisterResult, RegisterResponse>();
        CreateMap<RegisterResult, RegisterResponse>();

        CreateMap<List<string>, IEnumerable<ErrorModelResponse>>()
            .ConvertUsing((List<string> a) =>
                a.Select((string b) => new ErrorModelResponse() { Message = b }));

        CreateMap<AuthResult, AuthResponse>();
        CreateMap<PasswordResetByAdminResult, PasswordResetByAdminResponse>();
    }
}