using hello_asp_identity.Domain;
using hello_asp_identity.Installers;
using hello_asp_identity.Services;

var builder = WebApplication.CreateBuilder(args);

// add services
builder.Services.InstallOptions(builder.Configuration);
builder.Services.InstallProjectServices();
builder.Services.InstallDb(builder.Configuration);
builder.Services.InstallIdentity(builder.Configuration);
builder.Services.InstallJwt(builder.Configuration);
builder.Services.InstallMvc();
builder.Services.InstallAutomapper();
builder.Services.InstallSwagger();

// builder.Logging.ClearProviders();
// builder.Logging.AddConsole();

var app = builder.Build();
app.InstallCreateMigrateSeedDb();

// http-request pipeline
app.InstallRequestPipeline();
app.Run();