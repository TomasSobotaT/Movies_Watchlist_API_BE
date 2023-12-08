using AutoMapper;
using Movies_Watchlist_API.Models;
using Movies_Watchlist_DB.Models;

namespace Movies_Watchlist_API.Configuration
{
    public class AutomapperConfiguration : Profile
    {
        public AutomapperConfiguration() 
        {
            CreateMap<BaseMovieDto, Movie>();
            CreateMap<BaseMovieDto, DeletedMovie>();
            CreateMap<BaseMovieDto, TestMovie>();
            CreateMap<BaseMovieDto, TestDeletedMovie>();

            CreateMap<TestMovie, BaseMovieDto>();
            CreateMap<Movie, BaseMovieDto>();
            CreateMap<TestDeletedMovie, BaseMovieDto>();
            CreateMap<DeletedMovie, BaseMovieDto>();

        }
    }
}
