using BackendDev.Data.Models;
using BackendDev.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendDev.Services
{
    public interface IReviewService
    {
        Task<bool> CheckToken(HttpRequest httpRequest);
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
                movie.Reviews.Add(reviewModel);
                /*_contextData.Entry(movie).State = EntityState.Modified;
                _contextData.Entry(reviewModel).State = EntityState.Modified;*/
                await _contextData.ReviewModels.AddAsync(reviewModel);
                await _contextData.SaveChangesAsync();
            }
               
            else throw new ArgumentException("Такой пользователя не существует");
            
           
        }
        public async Task EditReview(Guid movieId, Guid reviewId, ReviewModifyModel reviewModifyModelDTO, string UserName)
        {
            
        }

        public async Task DeleteReview(Guid movieId, Guid reviewIdDTO, string UserName)
        {

        }

        public Task<Boolean> CheckToken(HttpRequest httpRequest)
        {
            var token = httpRequest.Headers["Authorization"];
            foreach (InvalidToken InvToken in _contextData.InvalidTokens)
            {
                if (InvToken.JWTToken == token)
                {
                    return Task.FromResult(false);
                }
                else return Task.FromResult(true);
            }
            return Task.FromResult(true);
        }
    }
}
