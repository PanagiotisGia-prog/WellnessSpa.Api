using Microsoft.EntityFrameworkCore;
using WellnessSpa.Api.Model;

namespace WellnessSpa.Api.Data
{
    public static class DataExtension
    {
        public static void MigrateDB(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbcontext = scope.ServiceProvider
                .GetRequiredService<WellnessDBContext>();
            dbcontext.Database.Migrate(); 
        }

        public static void AddWellnessDB(this WebApplicationBuilder builder)
        {
            var connString = builder.Configuration.GetConnectionString(name: "WellnessDB");

            builder.Services.AddSqlite<WellnessDBContext>(connectionString: connString,
                optionsAction: options => options.UseSeeding((dbContext, _) =>
                {
                    if (!dbContext.Set<Treatment>().Any())
                    {
                        dbContext.Set<Treatment>().AddRange(
                            new Treatment { Name = "Relaxing Massage",  Duration = 60, Price = 30 },
                            new Treatment { Name = "Relaxing Massage",  Duration = 30, Price = 20 },
                            new Treatment { Name = "Aromatherapy",      Duration = 60, Price = 40 },
                            new Treatment { Name = "Aromatherapy",      Duration = 30, Price = 30 },
                            new Treatment { Name = "Deep Tissue",       Duration = 60, Price = 35 },
                            new Treatment { Name = "Deep Tissue",       Duration = 30, Price = 25 },
                            new Treatment { Name = "Ayurvedic Massage", Duration = 90, Price = 60 },
                            new Treatment { Name = "Hot & Cold Stones", Duration = 90, Price = 70 },
                            new Treatment { Name = "Thai Massage",      Duration = 60, Price = 50 },
                            new Treatment { Name = "Balinese Head",     Duration = 45, Price = 30 },
                            new Treatment { Name = "Indian Head",       Duration = 30, Price = 20 },
                            new Treatment { Name = "Foot Massage",      Duration = 30, Price = 20 },
                            new Treatment { Name = "Facial Treatment",  Duration = 60, Price = 50 },
                            new Treatment { Name = "Body Treatment",    Duration = 60, Price = 50 } 
                        );
                    }

                    if (!dbContext.Set<Pressure>().Any())
                    {
                        dbContext.Set<Pressure>().AddRange(
                            new Pressure { Name = "Soft"    },
                            new Pressure { Name = "Medium"  },
                            new Pressure { Name = "Hard"    }
                        );
                    } 

                    dbContext.SaveChanges();
                }
            ));
        }
    }
}