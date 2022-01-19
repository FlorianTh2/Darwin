namespace hello_asp_identity.Services;

public interface ICurrentUserService
{
    string? UserId { get; }
}