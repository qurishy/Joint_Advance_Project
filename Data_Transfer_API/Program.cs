using Data_Transfer_API.DATA;
using Data_Transfer_API.DATA.Service_Db;
using Data_Transfer_API.Repository;
using Data_Transfer_API.Repository.Profile;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.Configure<MongoDb_Setting>(
    builder.Configuration.GetSection("MongoDB")
    );

builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    var settings = builder.Configuration.GetSection("MongoDB").Get<MongoDb_Setting>();

    return new MongoClient(settings?.ConnectionString);
});

builder.Services.AddScoped<IMongoService, MongoService>();

builder.Services.AddScoped(typeof(IRepository<>),typeof (Repository<>));

builder.Services.AddScoped<IUser_Service, User_Service>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
