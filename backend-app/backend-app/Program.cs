using System.Text;
using System.Text.Json.Serialization;
using BL.Mapper;
using BL.Services;
using BL.Services.Authentication;
using BL.Services.Interfaces;
using DAL;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MarketPlaceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MarketPlaceDB")));
//Add automapper package
builder.Services.AddAutoMapper(typeof(ProductProfile), typeof(UserProfile), typeof(OrderProfile), typeof(DeliveryProfile), typeof(ProductImageProfile), typeof(ReviewProfile), typeof(MessageProfile), typeof(StatisticsProfile));

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IDeliveryService, DeliveryService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IStatisticsService, StatisticsService>();


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IStatisticsRepository, StatisticsRepository>();


builder.Services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = builder.Configuration["Jwt:Issuer"],
                     ValidAudience = builder.Configuration["Jwt:Audience"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                 };
             });

builder.Services.AddScoped<AuthenticationService, AuthenticationService>();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    //To avoid JsonException because of possible object cycle
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    //For a pretty JSON print
    options.JsonSerializerOptions.WriteIndented = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option=> {
                    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                        {
                            In = ParameterLocation.Header,
                            Description = "Please enter a valid token",
                            Name = "Authorization",
                            Type = SecuritySchemeType.Http,
                            BearerFormat = "JWT",
                            Scheme = "Bearer"
                        });
                        option.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type=ReferenceType.SecurityScheme,
                                        Id="Bearer"
                                    }
                                },
                                new string[]{}
                            }
                        });
});

builder.Services.AddCors(options=> {
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod() ; // Allow 'Authorization' header;
        });

});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
