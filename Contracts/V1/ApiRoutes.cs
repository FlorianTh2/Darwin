namespace hello_asp_identity.Contracts;

public static class ApiRoutes
{
    public const string Root = "api";

    public const string Version = "v1";

    public const string Base = Root + "/" + Version;

    public static class Account
    {
        public const string Register = Base + "/register";

        public const string RegisterConfirm = Base + "/registerconfirm"
    }
}