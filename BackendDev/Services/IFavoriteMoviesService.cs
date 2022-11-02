using BackendDev.Data.Models;
using BackendDev.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendDev.Services
{
    public interface IFavoriteMoviesService
    {
        public MoviesListModel GetMoviesList(string userName);
        public Task AddFavorite(string userName,Guid id);
        public Task DeleteFavorite(string userName,Guid id);
    }
    public class FavoriteMoviesService : IFavoriteMoviesService
    {
        private ContextDataBase _contextData;
        public FavoriteMoviesService(ContextDataBase contextData)
        {
            _contextData = contextData;
        }
        public  MoviesListModel GetMoviesList(string userName)
        {
           var user =  _contextData.Users.Where(x => x.UserName == userName).Include(x=>x.UserMovies).ThenInclude(x=>x.MovieGenres).Include(x=>x.UserMovies).ThenInclude(x => x.Reviews).FirstOrDefault();
            if (user != null)
            {
                var movies = user.UserMovies.ToList();
                MoviesListModel moviesDTO = new MoviesListModel(movies);
                return moviesDTO;
            }
            else throw new ArgumentException("такого пользователя не существует");
        }
        public async Task AddFavorite(string userName, Guid id)
        {
            var user = _contextData.Users.Where(x => x.UserName == userName).FirstOrDefault();
            if (user != null)
            {
                var movie = await _contextData.MovieModels.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (movie != null)
                {
                    user.UserMovies.Add(movie);
                    await _contextData.SaveChangesAsync();
                }
                else throw new ArgumentException("такого фильма не существует");
            }
            else throw new ArgumentException("такого пользователя не существует");
        }
        public async Task DeleteFavorite(string userName, Guid id)
        {
            var user = _contextData.Users.Where(x => x.UserName == userName).Include(x => x.UserMovies).ThenInclude(x => x.MovieGenres).Include(x => x.UserMovies).ThenInclude(x => x.Reviews).FirstOrDefault();
            if (user != null)
            {
                var movie = await _contextData.MovieModels.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (movie != null)
                {
                    user.UserMovies.Remove(movie);
                    await _contextData.SaveChangesAsync();
                }

                else throw new ArgumentException("такого фильма не существует");
            }
            else throw new ArgumentException("такого пользователя не существует");
        }
    }
}
