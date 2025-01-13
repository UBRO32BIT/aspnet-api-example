using EventManagement_BusinessObjects;
using EventManagement_Repositories.Interfaces;
using EventManagement_Repositories;
using EventManagement_Services.Interfaces;
using EventManagement_Services;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using Microsoft.Extensions.Logging;

namespace EventManagement_ODataExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IEventService, EventService>();
            builder.Services.AddScoped(typeof(BaseDAO<>));
            builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            builder.Services.AddDbContext<EventManagementDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var modelBuilder = new ODataConventionModelBuilder();
            var events = modelBuilder.EntitySet<Event>("Events");
            events.EntityType.HasKey(e => e.Id);
            builder.Services.AddControllers().AddOData(
    options => options.Select().Filter().OrderBy().Expand().Count().SetMaxTop(null).AddRouteComponents(
        "odata",
        modelBuilder.GetEdmModel()));

            var app = builder.Build();

            app.UseRouting();

            app.UseEndpoints(endpoints => endpoints.MapControllers());

            app.Run();
        }
    }
}
