using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WeazorApi;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ILocationService, LocationService>(); //Add location service as singleton
                                                                    //because we don't need to read the same json
                                                                    //at every user request.

// Add services to the container
//Setup CORS
builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "WeazorApi", Version = "v1" });
});

//Add caching
builder.Services.AddMemoryCache();



var app = builder.Build();
app.UseCors(options => options.AllowAnyOrigin());
// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WeazorApi v1"));
}
app.UseHttpsRedirection();
app.MapControllers();

app.Run();