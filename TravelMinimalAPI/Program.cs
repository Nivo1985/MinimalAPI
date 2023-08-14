using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TravelMinimalAPI.DbContexts;
using TravelMinimalAPI.Extensions;
using TravelMinimalAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TravelDbContext>(x=> x.UseSqlite(
        builder.Configuration["ConnectionStrings:DishesDBConnectionString"]));
builder.Services.AddScoped<UtilService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddProblemDetails();

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();
builder.Services.AddAuthorizationBuilder().AddPolicy("UEAdmin", policyBuilder =>
    policyBuilder.RequireRole("Admin").RequireClaim("Region", "EU"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("TokenAuthNZ", 
        new()
        {
            Name = "Authorization",
            Description = "Token-based authentication and authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            In = ParameterLocation.Header
        });
    options.AddSecurityRequirement(new()
    {
        {   
            new ()
            {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "TokenAuthNZ" }
            }, new List<string>()}
    }); 
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
}
app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();
app.RegisterEndpoints();

using (var scope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope())
{
    var context = scope?.ServiceProvider.GetRequiredService<TravelDbContext>();
    context?.Database.EnsureDeleted();
    context?.Database.Migrate();
}
app.Run();
// test