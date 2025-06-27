using Microsoft.EntityFrameworkCore;
using ChatAppAPI.Data;


namespace ChatAppAPI
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      // Add services to the container.
      builder.Services.AddControllers();
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen();

      // PostgreSQL configuration
      builder.Services.AddDbContext<AppDbContext>(options =>
          options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

      var allowedOrigins = builder.Configuration.GetValue<string>("AllowedOrigins")!.Split(",");

      builder.Services.AddCors(options =>
      {
        options.AddDefaultPolicy(policy =>
        {
          policy.WithOrigins(allowedOrigins).AllowAnyHeader().AllowAnyMethod();
        });
      });

      var app = builder.Build();

      // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI();
      }

      app.UseHttpsRedirection();

      app.UseCors();

      app.UseAuthorization();


      app.MapControllers();

      app.Run();
    }
  }
}
