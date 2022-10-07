using AutoMapper;
using BallTalkAPI.Data;
using BallTalkAPI.Interfaces;
using BallTalkAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BallTalkContext>();
AddRepositories(builder);
AddMapper(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

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