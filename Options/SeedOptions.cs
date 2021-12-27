namespace hello_asp_identity.Options;

public class SeedOptions
{
    public const string SectionName = "Seeds";

    public AdminOptions AdminOptions { get; set; }

}

public class AdminOptions
{
    public string Username { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string AdminRoleName { get; set; }
}