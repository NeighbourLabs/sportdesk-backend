using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using sportdesk_backend.Auth;
using sportdesk_backend.Infra;
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
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

// JWT configuration
var jwtSection = builder.Configuration.GetSection("Jwt");
builder.Services.Configure<JwtSettings>(jwtSection);

var jwtSettings = jwtSection.Get<JwtSettings>()!;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.MapInboundClaims = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
            RoleClaimType = "role"
        };
    });
builder.Services.AddAuthorization();

// Repositories
builder.Services.AddScoped<IAthleteEnrollmentRepository, AthleteEnrollmentRepository>();
builder.Services.AddScoped<IAthleteRepository, AthleteRepository>();
builder.Services.AddScoped<ICoachSportsRepository, CoachSportsRepository>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<ISportRepository, SportRepository>();
builder.Services.AddScoped<ITenantRepository, TenantRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Services
builder.Services.AddScoped<IAthleteEnrollmentService, AthleteEnrollmentService>();
builder.Services.AddScoped<IAthleteService, AthleteService>();
builder.Services.AddScoped<ICoachSportService, CoachSportService>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<ISportService, SportService>();
builder.Services.AddScoped<ITenantService, TenantService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Mappers
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
    options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention());

var app = builder.Build();

app.UseCors();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/health", () => Results.Ok());

app.MapControllers();

app.Run();
