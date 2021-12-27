namespace hello_asp_identity.Installers;

public static class RequestPipelineInstaller
{
    public static void InstallRequestPipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(a =>
            {
                a.SwaggerEndpoint("swagger/v1/swagger.json", "hello-asp-identity (V1)");
                if (app.Environment.IsDevelopment())
                {
                    a.RoutePrefix = string.Empty;
                }
            });
        }
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
    }
}