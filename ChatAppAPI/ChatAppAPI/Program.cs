using Microsoft.EntityFrameworkCore;
using ChatAppAPI.Data;
using ChatAppAPI.Repositories.Interfaces;
using ChatAppAPI.Repositories.Implementations;
using ChatAppAPI.Services.Interfaces;
using ChatAppAPI.Services.Implementations;
using ChatAppAPI.Hubs;


namespace ChatAppAPI
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      // Add services to the container.
      builder.Services.AddControllers();
      builder.Services.AddSignalR();

      // PostgreSQL configuration
      builder.Services.AddDbContext<AppDbContext>(options =>
          options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

      // Repository and Service registrations
      builder.Services.AddScoped<IMessageRepository, MessageRepository>();
      builder.Services.AddScoped<IMessageService, MessageService>();

      // Swagger configuration
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen();

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

      app.MapHub<ChatHub>("/chathub");

      app.MapControllers();

      app.Run();
    }
  }
}
