using hello_asp_identity.Installers;
using Serilog;

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
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}