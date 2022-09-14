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
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<UserRepository, UserRepository>();

builder.Services.AddTransient<IDemandService, DemandService>();
builder.Services.AddTransient<DemandRepository, DemandRepository>();

builder.Services.AddTransient<IDocumentService, DocumentService>();
builder.Services.AddTransient<DocumentRepository, DocumentRepository>();

builder.Services.AddTransient<IDecisionService, DecisionService>();
builder.Services.AddTransient<DecisionRepository, DecisionRepository>();

builder.Services.AddTransient<LoginController, LoginController>();


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
