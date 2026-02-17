using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ãÔÑæÚ_ÞÈá_ÇáÔÛá;
using ãÔÑæÚ_ÞÈá_ÇáÔÛá.Authorizetion;
using ãÔÑæÚ_ÞÈá_ÇáÔÛá.Data;
using ãÔÑæÚ_ÞÈá_ÇáÔÛá.Filters;
using ãÔÑæÚ_ÞÈá_ÇáÔÛá.Middlewares;
using ãÔÑæÚ_ÞÈá_ÇáÔÛá.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllers(options => {
    options.Filters.Add<LogActiviryActionFilter>();
    options.Filters.Add<PermissionBaseFilters>();
});


builder.Services.Configure<AttachmentsOpetions>(builder.Configuration.GetSection("Attachments"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IWeatherServices,WetherForCastServices>();
builder.Services.AddAuthorization(opetions => {
    opetions.AddPolicy("adminsOnly", builder =>
    {
        builder.RequireRole("admin","Superuser");
    });
    });
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
var Jwtopetionss = builder.Configuration.GetSection("Jwt").Get<JwtOpetions>();
builder.Services.AddSingleton(Jwtopetionss);
builder.Services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer=true,
        ValidIssuer = Jwtopetionss.Issure,
        ValidateAudience=true,
        ValidAudience= Jwtopetionss.Audience,
        ValidateLifetime=true,
        ValidateIssuerSigningKey=true,
        IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Jwtopetionss.SigningKey))
    };


});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<RateLimitaing>();
app.UseMiddleware<ProfilingMideelware>();

app.MapControllers();

app.Run();
