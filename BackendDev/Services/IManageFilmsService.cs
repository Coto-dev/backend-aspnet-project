using BackendDev.Data.Models;
using BackendDev.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BackendDev.Services
{
    public interface IManageFilmsService
    {
        public Task AddFilm(MovieDetailsModel movieDetailsModelDTO);
        public Task AddGenre(GenreModel GenreModelDto);
        public Task AddGenreToFilm(string IdFilm , string IdGenre);


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
            foreach (MovieModel movieDb in _contextData.MovieModels)
            {
                if (movieDb.Name == movieDetailsModelDTO.Name)
                {
                    throw new ArgumentException("фильм с таким названием уже существует");
                }
            }
      
            await _contextData.MovieModels.AddAsync(new MovieModel(movieDetailsModelDTO));
            await _contextData.SaveChangesAsync();
            
        }
        public async Task AddGenreToFilm(string IdFilm, string IdGenre)
        {

        }
        public async Task AddGenre(GenreModel GenreModelDto)
        {
            foreach (GenreModelBd GenreDb in _contextData.Genres)
            {
                if (GenreDb.Name == GenreModelDto.Name)
                {
                    throw new ArgumentException("такой жанр уже существует");
                }
            }

            await _contextData.Genres.AddAsync(new GenreModelBd(GenreModelDto));
            await _contextData.SaveChangesAsync();
        }
    }
}
