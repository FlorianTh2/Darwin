using hello_asp_identity.Domain;
using hello_asp_identity.Installers;

var builder = WebApplication.CreateBuilder(args);

// add services
builder.Services.InstallOptions(builder.Configuration);
builder.Services.InstallProjectServices();
builder.Services.InstallDb(builder.Configuration);
builder.Services.InstallIdentity();
builder.Services.InstallMvc();
builder.Services.InstallSwagger();
var app = builder.Build();

// http-request pipeline
app.InstallRequestPipeline();
app.Run();