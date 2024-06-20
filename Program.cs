using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Dagagino.Data;
using Dagagino.Interfaces;
using Dagagino.Middlewares;
using Dagagino.Models;
using Dagagino.Repository;
using Dagagino.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers()
 .AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

/* -------------------------------------------------------------------------- */
/*                        Add Authentication to Swagger                       */
/* -------------------------------------------------------------------------- */
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


/* -------------------------------------------------------------------------- */
/*                            Read Database Details                           */
/* -------------------------------------------------------------------------- */
builder.Services.Configure<DagaginoDBSettings>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.AddSingleton<IDagaginoDBSettings>(sp => sp.GetRequiredService<IOptions<DagaginoDBSettings>>().Value);


/* -------------------------------------------------------------------------- */
/*                           Authentication Handler                           */
/* -------------------------------------------------------------------------- */
var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();

builder.Services.AddSingleton(jwtOptions!);
builder.Services.AddSingleton<JwtSecurityTokenHandler>();

builder.Services.AddAuthentication()
            .AddJwtBearer(
                JwtBearerDefaults.AuthenticationScheme,
                options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidAudience = jwtOptions!.Audience,
                        ValidateIssuer = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtOptions.SigningKey)
                        ),
                    };
                }
            );

/* -------------------------------------------------------------------------- */
/*                                Token Service                               */
/* -------------------------------------------------------------------------- */
builder.Services.AddSingleton<TokenService>();
builder.Services.AddSingleton<HashingService>();

/* -------------------------------------------------------------------------- */
/*                   Initialize Application Database Context                  */
/* -------------------------------------------------------------------------- */
builder.Services.AddSingleton<AppDBContext>();

/* -------------------------------------------------------------------------- */
/*                           Initialize Repositories                          */
/* -------------------------------------------------------------------------- */
builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISystemLookupRepository, SystemLookupRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

/* -------------------------------------------------------------------------- */
/*                             Set Our Middlewares                            */
/* -------------------------------------------------------------------------- */
app.UseMiddleware<ProfilingMiddleware>();
app.UseMiddleware<RateLimitMiddleware>();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();
