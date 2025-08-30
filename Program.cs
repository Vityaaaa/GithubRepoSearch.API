using FluentValidation;
using GithubRepoSearch.AutoMapper.Profiles;
using GithubRepoSearch.Services.Auth;
using GithubRepoSearch.Services.BookmarkSessionStore;
using GithubRepoSearch.Services.Github;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowedOrigins",
        policy =>
        {
            string[] methods = builder.Configuration["CORS:Methods"].Split(",");
            string[] origins = builder.Configuration["CORS:Origins"].Split(",");
            string[] headers = builder.Configuration["CORS:Headers"].Split(",");

            policy.WithOrigins(origins)
                  .WithMethods(methods)
                  .WithHeaders(headers);
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

// Register custom services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IGithubService, GithubService>();
builder.Services.AddSingleton<IBookmarkSessionStore, InMemoryBookmarkSessionStore>();

// JWT Authentication
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"])),
    };
});

// Configure Fluent Validation
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

// Configure Mapping
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<AutoMapperGutHubRepoConfig>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowedOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
