using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Movies_Watchlist_API.Configuration;
using Movies_Watchlist_API.Interfaces;
using Movies_Watchlist_API.Models;
using Movies_Watchlist_API_Managers;
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


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowHost", builder =>
    {
        builder.WithOrigins("https://watchlist-movie.netlify.app")
        .AllowAnyHeader()
        .AllowAnyMethod();

    });

    options.AddPolicy("AllowTestHost", builder =>
    {
        builder.WithOrigins("http://localhost:3000")
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

//app.UseCors();

app.UseCors("AllowHost");
app.UseCors("AllowTestHost");

app.Run();
