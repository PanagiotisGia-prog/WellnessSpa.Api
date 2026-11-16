using Microsoft.EntityFrameworkCore;
using WellnessSpa.Api.Data;
using WellnessSpa.Api.DTO;
using WellnessSpa.Api.Model;

namespace WellnessSpa.Api.Endpoint
{
    public static class EndPointWellness
    {
        const string EndpointName = "Wellness";
        
        public static void MapWellness(this WebApplication app)
        {            
            var group = app.MapGroup("/wellness");

            // GET /wellness
            group.MapGet("/", async (WellnessDBContext dbContext) => await dbContext.Wellnesses
                                                                            .Include(wellness => wellness.Treatment)
                                                                            .Include(wellness => wellness.Pressure)
                                                                            .Select(wellness => new ReadWellnessDTO(
                                                                                wellness.Id,
                                                                                wellness.Name,
                                                                                wellness.Treatment!.Name,
                                                                                wellness.Pressure!.Name,
                                                                                wellness.Duration,
                                                                                wellness.Price,
                                                                                wellness.IsCoupled,
                                                                                wellness.BookedDay
                                                                            ))
                                                                            .AsNoTracking()
                                                                            .ToListAsync());

            // GET /wellness/id
            group.MapGet("/{id}", async (int id, WellnessDBContext dbContext) =>
            {
                var wellness = await dbContext.Wellnesses.FindAsync(id);

                return wellness is null ? Results.NotFound() : Results.Ok(new ReadDBWellnessDTO(
                    Id: wellness.Id, 
                    Name: wellness.Name,
                    TreatmentId: wellness.TreatmentId,
                    PressureId: wellness.PressureId,
                    Duration: wellness.Duration,
                    Price: wellness.Price,
                    IsCoupled: wellness.IsCoupled,
                    BookedDay: wellness.BookedDay
                ));
            }).WithName(EndpointName);

            // POST /wellness
            group.MapPost("/", async (CreateWellnessDTO newWellness, WellnessDBContext dbContext) =>
            {
                if (newWellness.BookedDay <= DateTime.Now)
                    return Results.BadRequest("You need to choose a valid day and time");

                var treatment = await dbContext.Treatments.FindAsync(newWellness.TreatmentId);
                if (treatment is null)
                    return Results.BadRequest("Treatment doesn't exist!");

                bool pressureExists = await dbContext.Pressures.AnyAsync(pressue => pressue.Id == newWellness.PressureId);
                if (!pressureExists)
                    return Results.BadRequest("Pressure doesn't exists");

                bool isTaken = await dbContext.Wellnesses.AnyAsync(booking => booking.BookedDay == newWellness.BookedDay);   
                if (isTaken)
                    return Results.Conflict("This time is already taken.");

                Wellness wellness = new() {
                    Name = newWellness.Name,
                    TreatmentId = newWellness.TreatmentId,
                    PressureId = newWellness.PressureId,
                    Duration = treatment.Duration,
                    Price = newWellness.IsCoupled ? treatment.Price * 2 : treatment.Price,
                    IsCoupled = newWellness.IsCoupled,
                    BookedDay = newWellness.BookedDay
                };

                dbContext.Wellnesses.Add(wellness);
                await dbContext.SaveChangesAsync();

                ReadDBWellnessDTO createdWellness = new(
                    Id: wellness.Id,
                    Name: wellness.Name,
                    TreatmentId: wellness.TreatmentId,
                    PressureId: wellness.PressureId,
                    Duration: wellness.Duration,
                    Price: wellness.Price,
                    IsCoupled: wellness.IsCoupled,
                    BookedDay: wellness.BookedDay
                );

                return Results.CreatedAtRoute(EndpointName, new {id = createdWellness.Id}, createdWellness);
            });

            // PUT /wellness/id
            group.MapPut("/{id}", async (int id, UpdateWellnessDTO update, WellnessDBContext dbContext) =>
            {
                var existingWellness = await dbContext.Wellnesses.FindAsync(id);

                if (existingWellness is null)
                    return Results.NotFound();

                if (update.BookedDay <= DateTime.Now)
                    return Results.BadRequest("You need to choose a valid day and time");

                var treatment = await dbContext.Treatments.FindAsync(update.TreatmentId);
                if (treatment is null)
                    return Results.BadRequest("Treatment doesn't exist!");

                bool pressureExists = await dbContext.Pressures.AnyAsync(pressue => pressue.Id == update.PressureId);
                if (!pressureExists)
                    return Results.BadRequest("Pressure doesn't exists");

                bool isTaken = await dbContext.Wellnesses.AnyAsync(booking => booking.BookedDay == update.BookedDay);   
                if (isTaken)
                    return Results.Conflict("This time is already taken.");

                existingWellness.Name = update.Name;
                existingWellness.TreatmentId = update.TreatmentId;
                existingWellness.PressureId = update.PressureId;
                existingWellness.Duration = treatment.Duration;
                existingWellness.Price = update.IsCoupled ? treatment.Price * 2 : treatment.Price;
                existingWellness.IsCoupled = update.IsCoupled;
                existingWellness.BookedDay = update.BookedDay;

                await dbContext.SaveChangesAsync();

                return Results.NoContent();
            });

            // DELETE /wellness/id
            group.MapDelete("/{id}", async (int id, WellnessDBContext dbContext) =>
            {
                await dbContext.Wellnesses
                                        .Where(wellness => wellness.Id == id)
                                        .ExecuteDeleteAsync();

                return Results.NoContent();
            });
        }
    }
}
