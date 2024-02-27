using CVManagerSystem.Core.Base;
using CVManagerSystem.Data;
using CVManagerSystem.Data.DataContext;
using CVManagerSystem.Data.DataContext.DbIdentity;
using CVManagerSystem.Services.Implementations;
using CVManagerSystem.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContextFactory<DataContextFactory>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDataContext>((option) =>
{
    option.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("WebApplication4Context"));
});

builder.Services.AddDefaultIdentity<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDataContext>()
    .AddTokenProvider("CVManagerSystem", typeof(DataProtectorTokenProvider<ApplicationUser>));
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings
    // options.Lockout.MaxFailedAccessAttempts = config.MaxFailedAccessAttempts;
    options.Lockout.AllowedForNewUsers = true;

    // User settings
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
});


builder.Services.AddScoped<IIdentityServices, IdentityService>();
builder.Services.AddScoped<ICVServices, CVServices>();
builder.Services.AddScoped<IResponseDto, ResponseDto>();

builder.Services.AddCors(options => {
    options.AddPolicy("CORSPolicy", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed((hosts) => true));
});

var app = builder.Build();

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

app.Run();
