using Microsoft.EntityFrameworkCore;
using WellnessSpa.Api.Data;
using WellnessSpa.Api.DTO;

namespace WellnessSpa.Api.Endpoint
{
    public static class EndPointTreatment
    {
        public static void MapTreatment(this WebApplication app)
        {
            var group = app.MapGroup("/treatments");

            // GET /treatments
            group.MapGet("/", async (WellnessDBContext dbContext) => 
                    await dbContext.Treatments
                                    .Select(treatment => new ReadTreatmentDTO(treatment.Id, treatment.Name, treatment.Duration, treatment.Price)) 
                                    .AsNoTracking()
                                    .ToListAsync()
                                    );
        }
    }
}