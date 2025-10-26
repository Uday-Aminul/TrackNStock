using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TrackNStock.Api.Data;
using TrackNStock.Api.Mappings;
using TrackNStock.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<TrackNStockDbContext>(dbContextOptions =>
    dbContextOptions.UseSqlServer(builder.Configuration.GetConnectionString("TrackNStockConnectionString")));
builder.Services.AddDbContext<TrackNStockAuthDbContext>(dbContextOptions =>
    dbContextOptions.UseSqlServer(builder.Configuration.GetConnectionString("TrackNStockAuthConnectionString")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ISalesRepository, SalesRepository>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("NZWalks")
    .AddEntityFrameworkStores<TrackNStockAuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequiredLength = 4;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "TrackNStock API v1");
        options.RoutePrefix = string.Empty; // makes Swagger UI the default page
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
