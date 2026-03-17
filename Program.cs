using sportdesk_backend.Infra;
using Microsoft.EntityFrameworkCore;
using sportdesk_backend.Mappers;
using sportdesk_backend.Models;
using sportdesk_backend.Dtos;
using sportdesk_backend.Repositories.Implementations;
using sportdesk_backend.Repositories.Interfaces;
using sportdesk_backend.Services.Implementations;
using sportdesk_backend.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddScoped<IAthleteEnrollmentRepository, AthleteEnrollmentRepository>();
builder.Services.AddScoped<IAthleteRepository, AthleteRepository>();
builder.Services.AddScoped<ICoachSportsRepository, CoachSportsRepository>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<ISportRepository, SportRepository>();
builder.Services.AddScoped<ITenantRepository, TenantRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IAthleteEnrollmentService, AthleteEnrollmentService>();
builder.Services.AddScoped<IAthleteService, AthleteService>();
builder.Services.AddScoped<ICoachSportService, CoachSportService>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<ISportService, SportService>();
builder.Services.AddScoped<ITenantService, TenantService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IMapperBase<Athlete, AthleteDto>, AthleteMapper>();
builder.Services.AddScoped<IMapperBase<AthleteEnrollment, AthleteEnrollmentDto>, AthleteEnrollmentMapper>();
builder.Services.AddScoped<IMapperBase<CoachSport, CoachSportDto>, CoachSportMapper>();
builder.Services.AddScoped<IMapperBase<Enrollment, EnrollmentDto>, EnrollmentMapper>();
builder.Services.AddScoped<IMapperBase<Payment, PaymentDto>, PaymentMapper>();
builder.Services.AddScoped<IMapperBase<Sport, SportDto>, SportMapper>();
builder.Services.AddScoped<IMapperBase<User, UserDto>, UserMapper>();
builder.Services.AddScoped<TenantMapper>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

app.UseCors();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/health", () => Results.Ok());

app.MapControllers();

app.Run();
