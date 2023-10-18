using System.Text;
using JwtAuth.Constants;
using JwtAuth.Models.Settings;
using JwtAuth.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<TokenGeneratorService>().AddScoped<AuthService>().AddScoped<AccountService>();

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(SwaggerConstants.SecurityDefinitionName, new OpenApiSecurityScheme
    {
        Name = SwaggerConstants.SecuritySchemeName,
        Type = SecuritySchemeType.ApiKey,
        Scheme = SwaggerConstants.SecurityScheme,
        In = ParameterLocation.Header,
        Description = SwaggerConstants.SwaggerAuthorizationDescription
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = SwaggerConstants.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)));

var jwtSettings = builder.Configuration.GetRequiredSection(nameof(JwtSettings)).Get<JwtSettings>()!;
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = jwtSettings.ValidateIssuer,
            ValidIssuer = jwtSettings.Issuer,
            ValidateAudience = jwtSettings.ValidateAudience,
            ValidAudience = jwtSettings.Audience,
            ValidateLifetime = jwtSettings.ValidateLifeTime,
            ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
        };
    });

var app = builder.Build();

app.UseSwaggerUI();
app.UseSwagger();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();