using CatsAPI.Data;
using CatsAPI.Jobs;
using CatsAPI.Mappings;
using CatsAPI.Services;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Cats API",
        Version = "v1",
        Description = "API for fetching and managing cat images from TheCatAPI. Supports background job processing with Hangfire.\n\nContact: Spyros Avramiotis (spyros.avramiotis@outlook.com)"
    });
});

builder.Services.AddDbContext<CatsDbContext>(options =>
 options.UseSqlServer(builder.Configuration.GetConnectionString("CatsAPIConnectionString")));

builder.Services.AddHangfire(config => config
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("CatsAPIConnectionString"), new SqlServerStorageOptions
    {
        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
        QueuePollInterval = TimeSpan.Zero,
        UseRecommendedIsolationLevel = true,
        DisableGlobalLocks = true
    }));

builder.Services.AddHangfireServer();

builder.Services.AddHttpClient();

builder.Services.AddScoped<ICatService, SqlCatService>();
builder.Services.AddScoped<ICatApiService, SqlCatApiService>();
builder.Services.AddSingleton<IJobStatusService, JobStatusService>();
builder.Services.AddScoped<CatFetchJob>();

builder.Services.AddAutoMapper(cfg => {},typeof(AutoMapperProfiles));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseHangfireDashboard("/hangfire");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
