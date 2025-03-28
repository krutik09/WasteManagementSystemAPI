using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Net;
using System.Text;
using WasteManagementSystem.Business.Auth;
using WasteManagementSystem.Business.DTO;
using WasteManagementSystem.Business.Services.BaseService;
using WasteManagementSystem.Business.Services.OrderService;
using WasteManagementSystem.Business.Services.StatusService;
using WasteManagementSystem.Business.Services.UserService;
using WasteManagementSystem.Business.Services.WasteTypeService;
using WasteManagementSystem.Business.Services.WasteUnitService;
using WasteManagementSystem.Business.Validation;
using WasteManagementSystem.Data.Context;
using WasteManagementSystem.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(GlobalDtoValidationFilter)); // register filter service for DTO validation
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add postgres db context
builder.Services.AddDbContext<WmsContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("WMS")));

// Add auto mapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);
var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new WasteManagementSystem.Business.Mappers.AutoMapper());
});
var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

// Add authentication
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("....very...very..secret.key..unknown..")),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
    };
});

// Register Dto validation classes
builder.Services.AddScoped<IValidateDto<UserTypeDto>, UserTypeDtoValidation>();
builder.Services.AddScoped<IValidateDto<WasteTypeDto>, WasteTypeDtoValidation>();

// Register service and repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IBaseService<,>), typeof(BaseService<,>));

builder.Services.AddScoped<IUserTypeService, UserTypeService>();
builder.Services.AddScoped<IWasteTypeService,WasteTypeService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IWasteUnitService, WasteUnitService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IStatusService, StatusService>();
// Register global exception handler
//builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
//builder.Services.AddProblemDetails();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.UseExceptionHandler(options =>
{
    options.Run(async context =>
    {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Response.ContentType = "application/json";
        var exception = context.Features.Get<IExceptionHandlerFeature>();
        if (exception != null)
        {
            var message = $"{exception.Error.Message}";
            await context.Response.WriteAsync(message).ConfigureAwait(false);
        }
    });
});
app.Use(async (context, next) =>
{
    Console.WriteLine("Incoming Request Headers:");
    foreach (var header in context.Request.Headers)
    {
        Console.WriteLine($"{header.Key}: {header.Value}");
    }

    await next();
});
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
