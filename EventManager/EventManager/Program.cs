using EventManager.Kafka;
using EventManager.Mapper;
using EventManager.Service.CategoryService;
using EventManager.Service.CommentService;
using EventManager.Service.EventService;
using EventManager.Service.Images;
using EventManager.Service.RequestService;
using EventManager.Service.UserService;
using EventManager.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModelHolder.Context;
using Serilog;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EventManagerDbContext>(options =>
    options.UseNpgsql(Environment.GetEnvironmentVariable("DB_CONNECTION")));

var log = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddAutoMapper(typeof(GeneralMapper));

builder.Services.AddScoped<HelperMethods>();
builder.Services.AddScoped<EventManagerProducer>();

builder.Services.AddScoped<IAdminCategoryService, AdminCategoryService>();
builder.Services.AddScoped<IUserCategoryService, UserCategoryService>();

builder.Services.AddScoped<IAdminUserService, AdminUserService>();
builder.Services.AddScoped<IPrivateUserService, PrivateUserService>();

builder.Services.AddScoped<IAdminImageService, AdminImagesService>();
builder.Services.AddScoped<IUserImagesService, UserImagesService>();

builder.Services.AddScoped<IAdminCommentService, AdminCommentService>();
builder.Services.AddScoped<IUserCommentService, UserCommentService>();

builder.Services.AddScoped<IAdminEventService, AdminEventService>();
builder.Services.AddScoped<IUserEventService, UserEventService>();

builder.Services.AddScoped<IAdminRequestService, AdminRequestService>();
builder.Services.AddScoped<IUserRequestService, UserRequestService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<EventManagerDbContext>();
    db.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
