using AutoMapper;
using BallTalkAPI.Auth;
using BallTalkAPI.Auth.Entities;
using BallTalkAPI.Data;
using BallTalkAPI.Entities;
using BallTalkAPI.Filters;
using BallTalkAPI.Interfaces;
using BallTalkAPI.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(config =>
{
    config.Filters.Add<UnhandledExceptionFilterAttribute>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<BallTalkContext>();
AddRepositories(builder);
AddAuth(builder);
AddMapper(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

var dbSeeder = app.Services.CreateScope().ServiceProvider.GetRequiredService<AuthDbSeeder>();
await dbSeeder.SeedAsync();

app.Run();

static void AddMapper(WebApplicationBuilder builder)
{
    var mapperConfig = new MapperConfiguration(mc =>
    {
        mc.AddProfile(new MappingProfile());
    });
    var mapper = mapperConfig.CreateMapper();
    builder.Services.AddSingleton<IMapper>(mapper);
}

static void AddRepositories(WebApplicationBuilder builder)
{
    builder.Services.AddScoped<ITopicRepository, TopicRepository>();
    builder.Services.AddScoped<IPostRepository, PostRepository>();
    builder.Services.AddScoped<ICommentRepository, CommentRepository>();
}

static void AddAuth(WebApplicationBuilder builder)
{
    builder.Services.AddIdentity<User, IdentityRole>()
        .AddEntityFrameworkStores<BallTalkContext>()
        .AddDefaultTokenProviders();

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters.ValidAudience = builder.Configuration["JWT:ValidAudience"];
            options.TokenValidationParameters.ValidIssuer = builder.Configuration["JWT:ValidIssuer"];
            options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]));
        });

    builder.Services.AddSingleton<IJwtTokenService, JwtTokenService>();
    builder.Services.AddScoped<AuthDbSeeder>();

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy(PolicyNames.ResourceOwner, policy => policy.Requirements.Add(new ResourceOwnerRequirement()));
    });

    builder.Services.AddSingleton<IAuthorizationHandler, ResourceOwnerAuthorizationHandler>();
    JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
}