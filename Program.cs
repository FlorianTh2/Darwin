using hello_asp_identity.Installers;

var builder = WebApplication.CreateBuilder(args);

// add services
builder.Services.InstallMvc();
builder.Services.InstallSwagger();
var app = builder.Build();

// http-request pipeline
app.InstallRequestPipeline();
app.Run();
