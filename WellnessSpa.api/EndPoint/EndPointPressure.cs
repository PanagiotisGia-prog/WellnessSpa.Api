using Microsoft.EntityFrameworkCore;
using WellnessSpa.Api.Data;
using WellnessSpa.Api.DTO;

namespace WellnessSpa.Api.Endpoint
{
    public static class EndPointPressure
    {
        public static void MapPressure(this WebApplication app)
        {
            var group = app.MapGroup("/pressures");

            // GET /pressures
            group.MapGet("/", async (WellnessDBContext dbContext) 
                => await dbContext.Pressures
                                    .Select(pressure => new ReadPressureDTO(pressure.Id, pressure.Name))
                                    .AsNoTracking()
                                    .ToListAsync()
                                    );
        }
    }
}