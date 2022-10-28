using BackendDev.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendDev.Services
{
  public  interface IMovieService
    {
        public Task<IActionResult> GetMovieDetails(string Id);
        public Task<IActionResult> GetMoviePage(string Page);
    }
    public class MovieService : IMovieService
    {
        private readonly ContextDataBase _contextData;
        public MovieService(ContextDataBase contextData)
        {
            _contextData = contextData;
        }
       public async Task<IActionResult>GetMovieDetails(string Id)
        {
            return null;
        }
        public Task<IActionResult> GetMoviePage(string Page) 
        {
            return null;
        }


    }
}
