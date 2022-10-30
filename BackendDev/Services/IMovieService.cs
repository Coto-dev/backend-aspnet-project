using System.Collections.Generic;
using BackendDev.Data.Models;
using BackendDev.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendDev.Services
{
  public  interface IMovieService
    {
        public Task<ActionResult<MovieDetailsModel>> GetMovieDetails(Guid Id);
        public MoviesPagedListModel GetMoviePage(int Page);

        }
    public class MovieService : IMovieService
    {
        private readonly ContextDataBase _contextData;
        public MovieService(ContextDataBase contextData)
        {
            _contextData = contextData;
        }
       public async Task<ActionResult<MovieDetailsModel>> GetMovieDetails(Guid Id)
        {
            var modelDTO = new MovieDetailsModel(await _contextData.MovieModels.Where(x => x.Id == Id).Include(x=>x.MovieGenres).Include(x=>x.Reviews).ThenInclude(x=>x.User).FirstOrDefaultAsync());
            if (modelDTO != null)
            return modelDTO;
            else throw new ArgumentException("Фильм с таким Id не существует");
        }
        public MoviesPagedListModel GetMoviePage(int Page) 
        {
            MoviesPagedListModel modelDTO = null;
            const int pageSize = 6;
            var AmountFilms = _contextData.MovieModels.Count();
            int pageCount;
            if (AmountFilms % pageSize == 0)
                pageCount = AmountFilms / pageSize;
            else
                pageCount = (AmountFilms / pageSize) + 1;
                PageInfoModel pageInfo = new PageInfoModel(pageSize, pageCount, Page);
            List<MovieModel> moviesList = _contextData.MovieModels.Include(x => x.MovieGenres).Include(x => x.Reviews).ToList();
            List<List<MovieModel>> movies = Spliter.Split(moviesList,pageSize);
            var movieElementModels = movies[Page -1].Select(x => new MovieElementModel(x)).ToList();
            try
            {
                 modelDTO = new MoviesPagedListModel(movieElementModels, pageInfo);
            }
            catch
            {
                throw new ArgumentException("Что-то пошло не так при создании MoviesPagedListModel");
            }
            return modelDTO;
        }

    }
}
