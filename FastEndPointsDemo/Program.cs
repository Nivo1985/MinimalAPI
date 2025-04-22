using FastEndpoints;
using FastEndpoints.Swagger; 


var bld = WebApplication.CreateBuilder();
bld.Services.AddFastEndpoints().SwaggerDocument();

var app = bld.Build();
app.UseFastEndpoints().UseSwaggerGen();
app.Run();