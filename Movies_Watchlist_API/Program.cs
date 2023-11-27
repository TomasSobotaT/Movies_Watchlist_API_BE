using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Movies_Watchlist_API.Interfaces;
using Movies_Watchlist_DB.Data;
using Movies_Watchlist_DB.Interfaces;
using Movies_Watchlist_DB.Models;
using Movies_Watchlist_DB.Repositories;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<WatchlistDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WatchlistDbContext")));
builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

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
            Url = new Uri("https://www.tsobota.cz/")
        }
    }));


builder.Services.AddScoped<IMovieRepository<Movie>, MovieRepository>();
builder.Services.AddScoped<IMovieRepository<DeletedMovie>, DeletedMovieRepository>();

builder.Services.AddScoped<IMovieManager<Movie>, MovieManager>();
builder.Services.AddScoped<IMovieManager<DeletedMovie>, DeletedMovieManager>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowHost", builder =>
    {
        builder.WithOrigins("https://watchlist-movie.netlify.app")
        .AllowAnyHeader()
        .AllowAnyMethod();

    });


});


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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseCors("AllowHost");

app.Run();
