using AutoMapper;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using System.Net;
using WasteManagementSystem.API.ExceptionHandler;
using WasteManagementSystem.Business.DTO;
using WasteManagementSystem.Business.Services.BaseService;
using WasteManagementSystem.Business.Services.UserService;
using WasteManagementSystem.Business.Services.WasteTypeService;
using WasteManagementSystem.Business.Validation;
using WasteManagementSystem.Data.Context;
using WasteManagementSystem.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(GlobalDtoValidationFilter));
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

// Register Dto validation classes
builder.Services.AddScoped<IValidateDto<UserTypeDto>, UserTypeDtoValidation>();
builder.Services.AddScoped<IValidateDto<WasteTypeDto>, WasteTypeDtoValidation>();

// Register service and repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IBaseService<,>), typeof(BaseService<,>));

builder.Services.AddScoped<IUserTypeService, UserTypeService>();
builder.Services.AddScoped<IWasteTypeService,WasteTypeService>();
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
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
