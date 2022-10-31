using BackendDev.Data.Models;
using BackendDev.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BackendDev.Services
{
    public interface IFavoriteMoviesService
    {
        public ActionResult<MoviesListModel> GetMoviesList(string userName);
        public Task<IActionResult> AddFavorite(string userName,Guid id);
        public Task<IActionResult> DeleteFavorite(string userName,Guid id);
    }
    public class FavoriteMoviesService : IFavoriteMoviesService
    {
        private ContextDataBase _contextData;
        public FavoriteMoviesService(ContextDataBase contextData)
        {
            _contextData = contextData;
        }
        public ActionResult<MoviesListModel> GetMoviesList(string userName)
        {
            return null;
        }
        public async Task<IActionResult> AddFavorite(string userName, Guid id)
        {
            return null;
        }
        public async Task<IActionResult> DeleteFavorite(string userName, Guid id)
        {
            return null;
        }
    }
}
