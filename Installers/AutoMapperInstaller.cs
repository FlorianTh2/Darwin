public static class AutomapperInstaller
{
    public static void InstallAutomapper(this IServiceCollection services)
    {
        services.AddAutoMapper(config => { }, typeof(Program));
    }
}