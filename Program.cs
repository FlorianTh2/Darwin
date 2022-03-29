using Darwin.Installers;
using Serilog;

#pragma warning disable 8625, 8603, 8618, 1998, 8601

Log.Logger = SerilogInstaller.BootstrapSerilog();
Log.Information("Starting up");

var builder = WebApplication.CreateBuilder(args);

try
{
    // add services
    builder.Services.InstallOptions(builder.Configuration);
    builder.Services.InstallProjectServices();
    builder.Services.InstallDb(builder.Configuration);
    builder.Services.InstallIdentity(builder.Configuration);
    builder.Services.InstallJwt(builder.Configuration);
    builder.Services.InstallMvc();
    builder.Services.InstallAutomapper();
    builder.Services.InstallSwagger();
    builder.Services.InstallCors();
    builder.Host.InstallSerilog();

    var app = builder.Build();
    app.InstallCreateMigrateSeedDb();

    // http-request pipeline
    app.InstallRequestPipeline();
    app.Run();
}
catch (Exception ex)
{
    // exception handling for "default exception" when host ist interrupted while db migrate
    string type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal))
    {
        throw;
    }
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}