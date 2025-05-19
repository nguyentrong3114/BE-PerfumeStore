using BE_AMPerfume.DAL.Data;
using BE_AMPerfume.DAL.Interfaces;
using BE_AMPerfume.BLL.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BE_AMPerfume.DAL.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// ===========================
// Load cấu hình JWT từ appsettings.json
// ===========================
var jwtSettings = builder.Configuration.GetSection("Jwt");
var secretKey = jwtSettings["Key"] ?? throw new Exception("Jwt:Key is missing in configuration");

// ===========================
// Đăng ký các dịch vụ vào DI container
// ===========================

// 1️ Cấu hình DbContext (MySQL với Pomelo)
builder.Services.AddDbContext<AMPerfumeDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

// 2️ Cấu hình CORS cho phép frontend Next.js gọi API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // ← frontend origin
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // ← để gửi cookie (JWT)
    });
});
// Cấu hình CORS cho phép frontend Next.js gọi API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // ← để gửi cookie (JWT)
    });
});

// ⬅️ Trước UseAuthentication

// 3️ Đăng ký Repository & Service (Dependency Injection)
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartItemRepository, CartItemsRepository>();
builder.Services.AddScoped<ICartService, CartService>();
//Service

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddSingleton<JwtTokenGenerator>();
builder.Services.AddHttpContextAccessor();

// 4️ Đăng ký AutoMapper để map DTO ↔ Model
builder.Services.AddAutoMapper(typeof(AutoMapperUserProfile));
builder.Services.AddAutoMapper(typeof(AutoMapperProduct));
builder.Services.AddAutoMapper(typeof(AutoMapperCart));
builder.Services.AddAutoMapper(typeof(AutoMapperCartItem));

// 5️ Cấu hình JWT Bearer Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Lấy token từ cookie thay vì header Authorization
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["token"];
                return Task.CompletedTask;
            }
        };

        // Cấu hình xác thực token
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ClockSkew = TimeSpan.Zero,

            NameClaimType = ClaimTypes.Email
        };
    });
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

// 6️ Authorization + Controller

builder.Services.AddAuthorization();
builder.Services.AddControllers();



builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(googleOptions =>
{
    googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    googleOptions.CallbackPath = "/signin-google";
})
.AddFacebook(facebookOptions =>
{
    facebookOptions.AppId = builder.Configuration["Authentication:Facebook:AppId"];
    facebookOptions.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"];
    facebookOptions.CallbackPath = "/signin-facebook";
})
.AddGitHub(options =>
{
    options.ClientId = builder.Configuration["Authentication:GitHub:ClientId"];
    options.ClientSecret = builder.Configuration["Authentication:GitHub:ClientSecret"];
    options.CallbackPath = "/signin-github";
    options.Scope.Add("user:email");
});


// 7️ Swagger để test API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ===========================
// Cấu hình middleware pipeline
// ===========================

// Bật CORS
app.UseCors("AllowFrontend");

// Swagger cho môi trường dev
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HTTPS redirect
app.UseHttpsRedirection();

// Phải đặt thứ tự: Authentication → Authorization
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();



// Map các controller
app.MapControllers();

// Chạy app
app.Run();
