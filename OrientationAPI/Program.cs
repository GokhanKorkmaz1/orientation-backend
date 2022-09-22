using OrientationAPI.Data;
using Microsoft.EntityFrameworkCore;
using OrientationAPI.Business;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrientationAPI.Controllers;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);
var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("Appsettings:Token").Value);
// Add services to the container.
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<UserRepository, UserRepository>();

builder.Services.AddScoped<IDemandService, DemandService>();
builder.Services.AddScoped<DemandRepository, DemandRepository>();

builder.Services.AddScoped<IDecisionService, DecisionService>();
builder.Services.AddScoped<DecisionRepository, DecisionRepository>();

builder.Services.AddScoped<LoginController, LoginController>();


builder.Services.AddControllers()
            .AddJsonOptions(options =>
               options.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddDbContext<OrientationContext>(x =>
    x.UseSqlServer(builder.Configuration.GetConnectionString("OrientationDb")));
builder.Services.AddCors();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();

app.UseCors(x=> x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()/*.AllowCredentials()*/);
app.MapControllers();

app.Run();
