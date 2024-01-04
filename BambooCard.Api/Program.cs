
using BambooCard.Application.Profiles;
using BambooCard.Application.Services;
using BambooCard.Core.Config;
using BambooCard.Core.Interfaces;
using BambooCard.Infrastructure.Services;

namespace BambooCard.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Bind and register configuration
            builder.Services.Configure<HackerNewsSettings>(builder.Configuration.GetSection("HackerNewsSettings"));

            // Resolve settings for immediate use
            var hackerNewsConfig = builder.Configuration.GetSection("HackerNewsSettings").Get<BambooCard.Core.Config.HackerNewsSettings>();

            // Add services to the container.

            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddHttpClient<IHackerNewsService, HackerNewsService>(c =>
            {
                c.BaseAddress = new Uri(hackerNewsConfig.BaseUrl);
            });

            builder.Services.AddScoped<HackerNewsApplicationService>();
            builder.Services.AddHttpClient<IHackerNewsService, HackerNewsService>();

            builder.Services.AddScoped<HackerNewsApplicationService>();
            builder.Services.AddMemoryCache();

            builder.Services.AddControllers();
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

            app.Run();
        }
    }
}
