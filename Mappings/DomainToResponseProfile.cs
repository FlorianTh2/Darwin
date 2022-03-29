namespace Darwin.Mappings;

using AutoMapper;
using Darwin.Contracts.V1.Responses;
using Darwin.Domain.Results;
using Darwin.Entities;
using Darwin.Extensions;

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