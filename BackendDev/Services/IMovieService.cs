using System.Collections.Generic;
using BackendDev.Data.Models;
using BackendDev.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BackendDev.Services
{
  public  interface IMovieService
    {
        public MovieDetailsModel GetMovieDetails(string Id);
        public MoviesPagedListModel GetMoviePage(int Page);

        }
    public class MovieService : IMovieService
    {
        private readonly ContextDataBase _contextData;
        public MovieService(ContextDataBase contextData)
        {
            _contextData = contextData;
        }
       public MovieDetailsModel GetMovieDetails(string Id)
        {
            MovieDetailsModel modelDTO = null;
            foreach (MovieModel movieDetails in _contextData.MovieModels)
            {
                if (Id == movieDetails.Id.ToString())
                {
                    modelDTO = new MovieDetailsModel(movieDetails);
                  
                }
            }
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
            List<MovieModel> moviesList = _contextData.MovieModels.ToList();
            List<List<MovieModel>> movies = Spliter.Split(moviesList,pageCount);
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
