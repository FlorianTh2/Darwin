namespace hello_asp_identity.Installers;

public static class RequestPipelineInstaller
{
    public static void InstallRequestPipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
    }
}