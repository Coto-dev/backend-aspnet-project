using BackendDev.Data;
using BackendDev.Data.Models;
using BackendDev.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendDev.Services
{
    public interface IReviewService
    {
        public  Task<bool> CheckToken(string token);
        public  Task AddReviewToMovie(Guid movieId, ReviewModifyModel reviewModifyModelDTO, string UserName);
        public Task EditReview(Guid movieId, Guid reviewId, ReviewModifyModel reviewModifyModelDTO, string UserName);
        public  Task DeleteReview(Guid movieId, Guid reviewIdDTO, string UserName);

    }
    public class ReviewService : IReviewService
    {
        private readonly ContextDataBase _contextData;
        public ReviewService(ContextDataBase contextDataBase)
        {
              _contextData = contextDataBase;
        }

        public async Task AddReviewToMovie(Guid movieId, ReviewModifyModel reviewModifyModelDTO, string UserName)
        {
            var user = _contextData.Users.Where(x=>x.UserName == UserName).FirstOrDefault();
            if (user != null)
            {
                 var reviewModel = new ReviewModelBd(reviewModifyModelDTO, user);
                var movie = await _contextData.MovieModels.Where(x => x.Id == movieId).FirstOrDefaultAsync();
                if (movie != null)
                {
                    movie.Reviews.Add(reviewModel);
                    await _contextData.ReviewModels.AddAsync(reviewModel);
                    await _contextData.SaveChangesAsync();
                }
                else throw new ArgumentException("фильма с таким id не существует");

            }

            else throw new ArgumentException("Такого пользователя не существует");

        }
        public async Task EditReview(Guid movieId, Guid reviewId, ReviewModifyModel reviewModifyModelDTO, string UserName)
        {

            var ReviewDb = _contextData.ReviewModels.Include(x => x.User).FirstOrDefault(x => x.Id == reviewId);
            if (ReviewDb == null)
                throw new ArgumentException("отзыва с таким айди не существует");

            if (ReviewDb.User.UserName != UserName)
                throw new ArgumentException("У вас нет прав на редактирование");

                _contextData.Attach(ReviewDb);
                ReviewDb.updateModel(reviewModifyModelDTO);
                await _contextData.SaveChangesAsync();

        }

        public async Task DeleteReview(Guid movieId, Guid reviewId, string UserName)
        {

            var ReviewDb = await _contextData.ReviewModels.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == reviewId);
            if (ReviewDb == null)
                throw new ArgumentException("отзыва с таким айди не существует");

            if (ReviewDb.User.UserName != UserName)
                throw new ArgumentException("У вас нет прав на редактирование");

            var movie = await _contextData.MovieModels.Where(x => x.Id == movieId).Include(x => x.UserMovies).ThenInclude(x => x.Reviews).FirstOrDefaultAsync();
            if (movie != null)
            {
                var review = await _contextData.ReviewModels.Where(x => x.Id == reviewId).FirstOrDefaultAsync();
                if (review != null)
                {
                    _contextData.ReviewModels.Remove(review);
                     movie.Reviews.Remove(review);
                    await _contextData.SaveChangesAsync();
                }

                else throw new ArgumentException("такого фильма не существует");
            }
            else throw new ArgumentException("такого пользователя не существует");
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
