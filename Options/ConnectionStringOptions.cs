namespace hello_asp_identity.Options;

public class ConnectionStringOptions
{
    public const string SectionName = "ConnectionStrings";

    public string PostgresLocal { get; set; }

    public string PostgresProd { get; set; }
}