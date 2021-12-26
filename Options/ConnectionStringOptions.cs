namespace hello_asp_identity.Options;

public class ConnectionStringOptions
{
    public const string SectionName = "ConnectionStrings";

    public string Local { get; set; }

    public string Prod { get; set; }

    public string Dev { get; set; }
}