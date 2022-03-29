using Darwin.Contracts.V1.Requests;
using Darwin.Entities;
using Serilog;

namespace Darwin.Extensions;

public static class SerilogExtensions
{
    private const string obfuscateString = "#####";

    public static LoggerConfiguration ObfuscateSensitiveData(this LoggerConfiguration a)
    {
        a.Destructure.ByTransforming<AppUser>(a =>
        {
            a.PasswordHash = a.PasswordHash == null ? null : obfuscateString;
            a.EmailConfirmationToken = a.EmailConfirmationToken == null ? null : obfuscateString;
            a.ResetPasswordToken = a.ResetPasswordToken == null ? null : obfuscateString;
            a.SecurityStamp = a.SecurityStamp == null ? null : obfuscateString;
            return a;
        })
        .Destructure.ByTransforming<IdentityLoginRequest>(a =>
        {
            a.Password = obfuscateString;
            return a;
        })
        .Destructure.ByTransforming<IdentityRegisterRequest>(a =>
        {
            a.Password = obfuscateString;
            a.PasswordConfirm = obfuscateString;
            return a;
        });
        return a;
    }
}