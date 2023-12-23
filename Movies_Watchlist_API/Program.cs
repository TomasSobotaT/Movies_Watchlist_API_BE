using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Movies_Watchlist_API.Configuration;
using Movies_Watchlist_API.Interfaces;
using Movies_Watchlist_API.JwtBearer;
using Movies_Watchlist_API.Models;
using Movies_Watchlist_API_Managers;
using Movies_Watchlist_DB.Data;
using Movies_Watchlist_DB.Interfaces;
using Movies_Watchlist_DB.Models;
using Movies_Watchlist_DB.Repositories;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<WatchlistDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WatchlistDbContext")));
builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddIdentity<MovieUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;

}).AddEntityFrameworkStores<WatchlistDbContext>();



builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = false,
        //ValidIssuer = builder.Configuration["Jwt:Issuer"],
        //ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
    options.SwaggerDoc("movies", new OpenApiInfo
    {
        Version = "v1",
        Title = "Movies Watchlist",
        Description = "Rest API pro Movies Watchlist in React",
        Contact = new OpenApiContact
        {
            Name = "Kontakt",
            Url = new Uri("https://www.tsobota.cz")
        }
    }));
builder.Services.AddAutoMapper(typeof(AutomapperConfiguration));


builder.Services.AddScoped<IMovieRepository<Movie>, MovieRepository<Movie>>();
builder.Services.AddScoped<IMovieRepository<DeletedMovie>, MovieRepository<DeletedMovie>>();
builder.Services.AddScoped<IMovieRepository<TestMovie>, MovieRepository<TestMovie>>();
builder.Services.AddScoped<IMovieRepository<TestDeletedMovie>, MovieRepository<TestDeletedMovie>>();


builder.Services.AddScoped<IMovieManager<Movie,BaseMovieDto>, MovieManager<Movie,BaseMovieDto>>();
builder.Services.AddScoped<IMovieManager<TestMovie, BaseMovieDto>, MovieManager<TestMovie, BaseMovieDto>>();
builder.Services.AddScoped<IMovieManager<TestDeletedMovie, BaseMovieDto>, MovieManager<TestDeletedMovie, BaseMovieDto>>();
builder.Services.AddScoped<IMovieManager<DeletedMovie, BaseMovieDto>, MovieManager<DeletedMovie, BaseMovieDto>>();
builder.Services.AddScoped<TokenService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", builder =>
    {
        builder.WithOrigins("https://watchlist-movie.netlify.app", "https://watchlistmovies.netlify.app", "http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
 
    });
});
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowHost", builder =>
//    {
//        builder.WithOrigins("https://watchlist-movie.netlify.app")
//        .AllowCredentials()
//        .AllowAnyHeader()
//        .AllowAnyMethod();

//    });

//    options.AddPolicy("AllowTestHost", builder =>
//    {
//        builder.WithOrigins("https://watchlistmovies.netlify.app")
//        .AllowCredentials()
//        .AllowAnyHeader()
//        .AllowAnyMethod();


//    });

//});
//builder.Services.ConfigureApplicationCookie(options =>
//{
//    options.Cookie.HttpOnly = true;
//    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Použijte Always pro HTTPS
//    options.Cookie.SameSite = SameSiteMode.None; // Mùže být potøeba nastavit na None pro CORS
//});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
	options.SwaggerEndpoint("movies/swagger.json", "Movies Watchlist v1");
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors("MyCorsPolicy");
//app.UseCors("AllowTestHost");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.UseCors();



app.Run();
