using WellnessSpa.Api.Data;
using WellnessSpa.Api.Endpoint;

namespace WellnessSpa.Api
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddValidation();

            builder.AddWellnessDB();

            var app = builder.Build();

            app.MapWellness();
            app.MapTreatment();
            app.MapPressure();

            app.MigrateDB();

            app.Run();
        }
    }
}