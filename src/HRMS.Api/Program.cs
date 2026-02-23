using HRMS.Application.Interfaces.Repositories;
using HRMS.Application.Interfaces.Services;
using HRMS.Application.Services;
using HRMS.Infrastructure.Repositories;
using HRMS.Infrastructure.Database;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// ==========================
// 1️⃣ Add Controllers
// ==========================
builder.Services.AddControllers();

// ==========================
// 2️⃣ Add Swagger / OpenAPI
// ==========================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ==========================
// 3️⃣ Register Services / Repositories
// ==========================

// Example: Read connection string from environment variable
var connectionString = Environment.GetEnvironmentVariable("HRMS_TEST_DB_CONNECTION_STRING")
                      ?? throw new InvalidOperationException("Connection string not set in environment");
// Register Connection 
builder.Services.AddSingleton<IDbConnectionFactory>(_ => 
    new HRMS.Infrastructure.Database.PostgresConnectionFactory(connectionString));
// Register repository (ADO.NET)
builder.Services.AddScoped<IDepartmentRepository, HRMS.Infrastructure.Repositories.Ado.DepartmentRepositoryADO>();
builder.Services.AddScoped<IJobTitleRepository, HRMS.Infrastructure.Repositories.Ado.JobTitleRepository>();
// Register services
builder.Services.AddScoped<IDepartmentService, HRMS.Application.Services.DepartmentService>();
builder.Services.AddScoped<IJobTitleService, HRMS.Application.Services.JobTitleService>();
// ==========================
// Build the app
// ==========================
var app = builder.Build();

// ==========================
// 4️⃣ Configure Middleware / Swagger
// ==========================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "HRMS API v1");
        c.RoutePrefix = string.Empty; // Swagger at root: https://localhost:5001/
    });
}

// Optional: redirect HTTP → HTTPS
app.UseHttpsRedirection();

// ==========================
// 5️⃣ Map Controllers
// ==========================
app.MapControllers();

// ==========================
// 6️⃣ Run the app
// ==========================
app.Run();
