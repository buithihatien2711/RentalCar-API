using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RentalCar.API.Mapping;
using RentalCar.Data;
using RentalCar.Data.Repositories;
using RentalCar.Data.Repositoriess;
using RentalCar.Data.Seed;
using RentalCar.Service;
using RentalCar_API.RentalCar.Service;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var connectionString = builder.Configuration.GetConnectionString("Default");
// Add services to the container.

services.AddControllers().AddNewtonsoftJson();;

services.AddCors(o =>
    o.AddPolicy("CorsPolicy", builder =>
        builder.WithOrigins("*")
            .AllowAnyHeader()
            .AllowAnyMethod()));

// services.AddCors(o =>
//     o.AddPolicy("CorsPolicy", builder =>
//         builder.AllowAnyOrigin()
//             .AllowAnyHeader()
//             .AllowAnyMethod()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

// var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
// services.AddDbContext<DataContext>(
//     dbContextOptions => dbContextOptions
//         .UseMySql(connectionString, serverVersion)
//         .LogTo(Console.WriteLine, LogLevel.Information)
//         .EnableSensitiveDataLogging()
//         .EnableDetailedErrors()
// );

services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(connectionString));

/////
services.AddDbContext<DataContext>();
/////

services.AddScoped<ITokenService, TokenService>();

services.AddScoped<ICarService, CarService>();
services.AddScoped<ICarRepository, CarRepository>();

services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IUserService, UserService>();

services.AddScoped<ICarModelRepository, CarModelRepository>();
services.AddScoped<ICarModelService, CarModelService>();

services.AddScoped<ICarReviewRepository, CarReviewRepository>();
services.AddScoped<ICarReviewService, CarReviewService>();

services.AddScoped<ICarStatusRepository, CarStatusRepository>();
services.AddScoped<ICarStatusService, CarStatusService>();

services.AddScoped<IBookingRepository, BookingRepository>();
services.AddScoped<IBookingService, BookingService>();

services.AddScoped<IUserReviewRepository, UserReviewRepository>();
services.AddScoped<IUserReviewService, UserReviewService>();

services.AddScoped<IAdvertPhotoRepository, AdvertPhotoRepository>();
services.AddScoped<IAdvertPhotoService, AdvertPhotoService>();

services.AddScoped<INotificationRepository, NotificationRepository>();
services.AddScoped<INotificationService, NotificationService>();

services.AddScoped<IUploadImgService, UploadImgService>();

services.AddScoped<IPaymentService, PaymentService>();
services.AddScoped<IPaymentRepository, PaymentRepository>();

services.AddAutoMapper(typeof(AutoMappingConfiguration).Assembly);

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"]))
        };
    });

// services.AddControllers().AddJsonOptions(x =>
//                 x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
);
var app = builder.Build();

// //
// var service = (IServiceScopeFactory)app.Services.GetService(typeof(IServiceScopeFactory));
// using(var db = service.CreateScope().ServiceProvider.GetService<DataContext>())
// {
//     try
//     {
//         db.Database.Migrate();
//     }
//     catch (System.Exception ex)
//     {

//     }
// }

using (var scope_migrate = app.Services.CreateScope())
{
    try
    {
        var dataContext = scope_migrate.ServiceProvider.GetRequiredService<DataContext>();
        dataContext.Database.Migrate();
    }
    catch (System.Exception ex)
    {

    }
}
/////

using var scope = app.Services.CreateScope();
var serviceProvider = scope.ServiceProvider;
try
{
    var context = serviceProvider.GetRequiredService<DataContext>();
    context.Database.Migrate();
    Seed.SeedRole(context);
}
catch (Exception ex)
{
    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "Migration Failed");
}

try
{
    var context = serviceProvider.GetRequiredService<DataContext>();
    context.Database.Migrate();
    Seed.SeedStatus(context);
}
catch (Exception ex)
{
    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "Migration Failed");
}

try
{
    var context = serviceProvider.GetRequiredService<DataContext>();
    context.Database.Migrate();
    Seed.SeedLocation(context);
}
catch (Exception ex)
{
    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "Migration Failed");
}

try
{
    var context = serviceProvider.GetRequiredService<DataContext>();
    context.Database.Migrate();
    Seed.SeedModelBrand(context);
}
catch (Exception ex)
{
    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "Migration Failed");
}

try
{
    var context = serviceProvider.GetRequiredService<DataContext>();
    context.Database.Migrate();
    Seed.SeedTransmission(context);
}
catch (Exception ex)
{
    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "Migration Failed");
}

try
{
    var context = serviceProvider.GetRequiredService<DataContext>();
    context.Database.Migrate();
    Seed.SeedFuelType(context);
}
catch (Exception ex)
{
    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "Migration Failed");
}

try
{
    var context = serviceProvider.GetRequiredService<DataContext>();
    context.Database.Migrate();
    Seed.SeedTypeRegister(context);
}
catch (Exception ex)
{
    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "Migration Failed");
}

try
{
    var context = serviceProvider.GetRequiredService<DataContext>();
    context.Database.Migrate();
    Seed.SeedUser(context);
}
catch (Exception ex)
{
    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "Migration Failed");
}

try
{
    var context = serviceProvider.GetRequiredService<DataContext>();
    context.Database.Migrate();
    Seed.SeedRoleUser(context);
}
catch (Exception ex)
{
    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "Migration Failed");
}
/////

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
if (app.Environment.IsDevelopment())
{

}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();