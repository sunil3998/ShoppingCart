using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ShoppingCart.Service.CardsApi;
using ShoppingCart.Service.CartApi.Data;
using ShoppingCart.Service.CartApi.Extentions;
using ShoppingCart.Service.CartApi.Service;
using ShoppingCart.Service.CartApi.Service.IService;
using ShoppingCart.Service.CartApi.Utility;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;
var Services = builder.Services;

// Add services to the container.

builder.Services.AddControllers();
Services.AddHttpContextAccessor();
Services.AddScoped<BackendApiAuthenticationHttpClientHandler>();
Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(Configuration.GetConnectionString("connString"))
);
Services.AddScoped<IProductService, ProductService>();
Services.AddScoped<ICouponService, CouponService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer authorization string as follow: 'Bearer Generated-JWT-Token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { new OpenApiSecurityScheme
        {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            new string[] {}
        }
    });
});

IMapper mapper = MappingConfig.RegisterMap().CreateMapper();
Services.AddSingleton(mapper);
Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


Services.AddHttpClient("Product", u => u.BaseAddress = 
new Uri(Configuration["ServiceUrls:ProductApi"])).AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>();
Services.AddHttpClient("Coupon", x => x.BaseAddress =
new Uri(Configuration["ServiceUrls:CouponApi"])).AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>();
builder.AddAppAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();
builder.Services.AddHttpContextAccessor();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
ApplyMigration();
app.Run();


void ApplyMigration()
{
    using (var scope = app.Services.CreateScope())
    {
        var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        if (_db.Database.GetPendingMigrations().Count() > 0)
        {
            _db.Database.Migrate();
        }
    }
}