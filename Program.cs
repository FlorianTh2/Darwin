using hello_asp_identity.Domain;
using hello_asp_identity.Installers;
using hello_asp_identity.Services;

var builder = WebApplication.CreateBuilder(args);

Log.Information("Starting web host");

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