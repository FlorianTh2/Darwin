namespace hello_asp_identity.Contracts.V1;

public static class ApiRoutes
{
    public const string Root = "api";

    public const string Version = "v1";

    public const string Base = Root + "/" + Version;

    public static class Identity
    {
        public const string SubBase = Base + "/" + "identity";

        public const string Register = SubBase + "/register";
        public const string RegisterConfirm = SubBase + "/register_confirm";
        public const string Login = SubBase + "/login";
        public const string RefreshAccessToken = SubBase + "/refresh";
        public const string PasswordReset = SubBase + "/password_reset";
        public const string PasswordResetConfirm = SubBase + "/password_reset/confirm";
        public const string PasswordUpdate = SubBase + "/password_update";
        public const string PasswordUpdateConfirm = SubBase + "/password_update/confirm";
    }

    public static class User
    {
        public const string SubBase = Base + "/" + "users";

        public const string GetAll = SubBase;
        public const string Get = SubBase + "/{userId}";
        public const string Update = SubBase + "/{userId}";
        public const string Delete = SubBase + "/{userId}";
    }
}