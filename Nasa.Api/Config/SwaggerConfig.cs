namespace Nasa.Api.Config
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddRegistration(this IServiceCollection services) {

            var basepath = System.AppDomain.CurrentDomain.BaseDirectory;
            var xmlPath = Path.Combine(basepath, "Nasa.Api.xml");
            var xmlPath2 = Path.Combine(basepath, "Nasa.Data.xml");

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Nasa API V1",
                    Version = "v1",
                });
                c.IncludeXmlComments(xmlPath);
                c.IncludeXmlComments(xmlPath2);
            });

            return services;
        }

        public static IApplicationBuilder AddRegistration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nasa Api v1"); });

            return app;
        }
    }
}
