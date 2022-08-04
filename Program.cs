using CityInYourPocket.Interface;
using CityInYourPocket.Models;
using CityInYourPocket.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Donot forgot to add ConnectionStrings as "dbConnection" to the appsetting.json file
builder.Services.AddDbContext<ApplicationDbContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("dbConnection")));

//Connecting Every Interface with it's implementation folder
builder.Services.AddTransient<IUser, UserRepository>();
builder.Services.AddTransient<IService, ServiceRepository>();
builder.Services.AddTransient<IShop, ShopRepository>();
builder.Services.AddTransient<INews, NewsRepository>();
builder.Services.AddTransient<IMarket, MarketRepository>();
builder.Services.AddTransient<IJob, JobRepository>();
builder.Services.AddTransient<ICharity, CharityRepository>();
builder.Services.AddTransient<IBanner, BannerRepository>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//Start File Upload Configurations
builder.Services.Configure<FormOptions>(o =>
{
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});
//End File uploadconfigurations

builder.Services.AddControllers();

//auth
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
//end auth

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();