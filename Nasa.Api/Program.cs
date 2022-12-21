using Nasa.Api.Config;
using Nasa.Api.Mapper;
using Nasa.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
SwaggerConfig.AddRegistration(builder.Services);
builder.Services.AddScoped<IAsteroidesService, AsteroidesServiceNasa>();
builder.Services.AddScoped<IAsteroidesMapper, AsteroidesMapper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/

SwaggerConfig.AddRegistration(app);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
