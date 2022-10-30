using BackendDev.Data.Models;
using BackendDev.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BackendDev.Services
{
    public interface IReviewService
    {
        Task<bool> CheckToken(HttpRequest httpRequest);
        public  Task AddReviewToMovie(Guid movieId, ReviewModifyModel reviewModifyModelDTO);
        public Task EditReview(Guid movieId, Guid reviewId, ReviewModifyModel reviewModifyModelDTO);
        public  Task DeleteReview(Guid movieId, Guid reviewIdDTO);

    }
    public class ReviewService : IReviewService
    {
        private readonly ContextDataBase _contextData;
        public ReviewService(ContextDataBase contextDataBase)
        {
              _contextData = contextDataBase;
        }

        public async Task AddReviewToMovie(Guid movieId, ReviewModifyModel reviewModifyModelDTO)
        {

           
        }
        public async Task EditReview(Guid movieId, Guid reviewId, ReviewModifyModel reviewModifyModelDTO)
        {
            
        }

        public async Task DeleteReview(Guid movieId, Guid reviewIdDTO)
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
