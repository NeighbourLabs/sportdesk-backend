using sportdesk_backend.Infra;
using Microsoft.EntityFrameworkCore;
using sportdesk_backend.Repositories.Implementations;
using sportdesk_backend.Repositories.Interfaces;
using sportdesk_backend.Services.Implementations;
using sportdesk_backend.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAthleteEnrollmentRepository, AthleteEnrollmentRepository>();
builder.Services.AddScoped<IAthleteRepository, AthleteRepository>();
builder.Services.AddScoped<ICoachSportsRepository, CoachSportsRepository>();
builder.Services.AddScoped<IMonthlySessionRepository, MonthlySessionRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<ISportRepository, SportRepository>();
builder.Services.AddScoped<ITenantRepository, TenantRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IAthleteEnrollmentService, AthleteEnrollmentService>();
builder.Services.AddScoped<IAthleteService, AthleteService>();
builder.Services.AddScoped<ICoachSportsService, CoachSportsService>();
builder.Services.AddScoped<IMonthlySessionService, MonthlySessionService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<ISportService, SportService>();
builder.Services.AddScoped<ITenantService, TenantService>();
builder.Services.AddScoped<IUserService, UserService>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));


var app = builder.Build();

app.Run();