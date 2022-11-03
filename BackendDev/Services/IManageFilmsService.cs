using System.Linq;
using BackendDev.Data;
using BackendDev.Data.Models;
using BackendDev.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendDev.Services
{
    public interface IManageFilmsService
    {
        public Task AddFilm(MovieDetailsModel movieDetailsModelDTO);
        public Task AddGenre(GenreModel GenreModelDto);
        public Task AddGenreToFilm(Guid IdFilm, Guid IdGenre);
        public Task<bool> CheckToken(string token);

    }
    public class ManageFilmsService: IManageFilmsService
    {
        private readonly ContextDataBase _contextData;
        public ManageFilmsService(ContextDataBase contextData)
        {
            _contextData = contextData;
        }
        public async Task AddFilm(MovieDetailsModel movieDetailsModelDTO)
        {

            await _contextData.MovieModels.AddAsync(new MovieModel(movieDetailsModelDTO));
            await _contextData.SaveChangesAsync();
            
        }
        public async Task AddGenreToFilm(Guid IdGenre, Guid IdFilm)
        {
            var movieDb = await _contextData.MovieModels.FirstOrDefaultAsync(x=>x.Id == IdFilm);
            if (movieDb != null)
            {
                //_contextData.Entry(movieDb).State = EntityState.Modified;
                var GenreToFilm = await _contextData.Genres.FirstOrDefaultAsync(x => x.Id == IdGenre);
                if (GenreToFilm != null)
                {
                    movieDb.MovieGenres.Add(GenreToFilm);
                  //  _contextData.Entry(GenreToFilm).State = EntityState.Modified;
                    await _contextData.SaveChangesAsync();
                }
                else
                    throw new ArgumentException("не удалось найти жанр с таким id");
                
            }
            else
                throw new ArgumentException("не удалось найти фильм с таким id");
            
        }
        public async Task AddGenre(GenreModel GenreModelDto)
        {
            if (_contextData.Genres.Any(x=>x.Name == GenreModelDto.Name))
            {
                throw new ArgumentException("такой жанр уже существует");
            }

            await _contextData.Genres.AddAsync(new GenreModelBd(GenreModelDto));
            await _contextData.SaveChangesAsync();
        }
        public async Task<bool> CheckToken(string token)
        {
            var FindedToken = await _contextData.InvalidTokens.FirstOrDefaultAsync(x => x.JWTToken == token);
            if (FindedToken != null)
                return false;
            else return true;
        }
    }
}
