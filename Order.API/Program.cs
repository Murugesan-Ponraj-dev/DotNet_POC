using FluentValidation.AspNetCore;
using MongoDB.Driver;
using Order.API.Common;
using Order.Application.Services;
using Order.Domain.Common;
using Order.Domain.Repositories;
using Order.Domain.Services;
using Order.Infrastructure.Mapper;
using Order.Infrastructure.Repositories;
using Order.Infrastructure.ResourceManagerService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IResourceManager, ResourceProvider>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(typeof(MappingProfile));
// Add connection string
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection(AppsettingConstant.MONGODB_SETTING));
// Register mongoDb configuration as a singleton object
builder.Services.AddSingleton<IMongoDatabase>(options => {
    var settings = builder.Configuration.GetSection(AppsettingConstant.MONGODB_SETTING).Get<MongoDbSettings>();
    var client = new MongoClient(settings.ConnectionString);
    return client.GetDatabase(settings.DatabaseName);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
