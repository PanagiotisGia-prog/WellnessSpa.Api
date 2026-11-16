# Wellness Spa API

A practice REST API for a spa api, built to learn ASP.NET Core, Entity Framework Core and SQLite. It is a portfolio project, not a real application.

A booking links a treatment and a pressure level to a date and time. The treatment defines the duration and price, and the API copies them into the booking when it is created.

## Features

- Full CRUD for bookings
- Read-only lists of treatments and massage pressure levels
- Input validation with DataAnnotations
- Booking rules:
  - the booking date must be in the future
  - the treatment and the pressure level must exist
  - two bookings cannot have the exact same date and time
  - price and duration are taken from the treatment, never from the client
- In case the massage is for a couple then the price is getting doubled
- Database created and seeded automatically on first run
- Proper HTTP status codes: 200, 201, 204, 400, 404, 409

## Tools that I used

- C# 
- .NET 10
- ASP.NET Core minimal APIs
- SQLite
- Entity Framework Core

## Getting started

Requirements: [.NET 10 SDK](https://dotnet.microsoft.com/download)

```bash
git https://github.com/PanagiotisGia-prog/WellnessSpa.Api
```

```bash
dotnet run
```

The SQLite database (`WellnessSpa.db`) is created automatically on startup, including the migrations and seed data.

## Project structure

```
Data/        DbContext, migrations, seed data
Model/       Entities (Wellness, Treatment, Pressure)
DTOs/        Request and response records
EndPoints/   Minimal API endpoint groups
Program.cs   App setup
```

## Limitations

- No authentication yet. Anyone can create or delete bookings. Next step: JWT authentication, with the owner seeing all bookings and customers only their own.
- The conflict check compares the exact start time only.
- No frontend.

## What I learned

Minimal APIs, EF Core relationships and migrations, DTO design (read vs create/update), validation.

## Credits

Panagiotis Ginnou
