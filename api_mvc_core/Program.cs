using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using api_mvc_core.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
   options.OutputFormatters.RemoveType<SystemTextJsonOutputFormatter>();
   options.OutputFormatters.Add(new SystemTextJsonOutputFormatter
           (new JsonSerializerOptions(JsonSerializerDefaults.Web)
           {
              ReferenceHandler = ReferenceHandler.IgnoreCycles,
              //ContractResolver=new CamelCasePropertyNamesContractResolver()
              PropertyNameCaseInsensitive = false,
              PropertyNamingPolicy = null,
              WriteIndented = true,
           }));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(b =>
{
   b.AddPolicy("bloods", b =>
   b.AllowAnyHeader()
       .AllowAnyOrigin()
       .AllowAnyMethod()
   );
});

builder.Services.AddDbContextPool<Dbbloods>(op => {
   op.UseSqlServer(builder.Configuration.GetConnectionString("Default")
      );
   op.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors("Dbbloods");

app.Run();
