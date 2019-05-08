using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreWebApp.Services.Dtos;
using BookStoreWebApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStoreWebApp.Services
{
    public class ReviewService : IReviewService
    {
        private readonly DbContext context;

        public ReviewService(DbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Guid> AddAsync(Review review)
        {
            if (review.BookId == Guid.Empty)
            {
                throw new ArgumentException("Book id is not set.");
            }

            var id = Guid.NewGuid();
            var entity = new DAL.Entities.Review
            {
                BookId = review.BookId,
                Comment = review.Comment,
                Id = id,
                NumStars = review.NumStars,
                VoterName = review.VoterName,
            };

            await context.AddAsync(entity);
            await context.SaveChangesAsync();

            return id;
        }

        public async Task<IEnumerable<Review>> GetAllAsync()
        {
            return await context.Set<DAL.Entities.Review>()
                                .Select(rev => new Review
                                {
                                    BookId = rev.BookId,
                                    Comment = rev.Comment,
                                    Id = rev.Id,
                                    NumStars = rev.NumStars,
                                    VoterName = rev.VoterName
                                })
                                .ToListAsync();
        }

        public async Task<Review> GetByIdAsync(Guid id)
        {
            var review = await context.Set<DAL.Entities.Review>().FindAsync(id);
            return new Review
            {
                BookId = review.BookId,
                Comment = review.Comment,
                Id = review.Id,
                NumStars = review.NumStars,
                VoterName = review.VoterName
            };
        }

        public async Task Remove(Guid id)
        {
            var review = await context.Set<DAL.Entities.Review>().FindAsync(id);
            context.Remove(review);
            await context.SaveChangesAsync();
        }

        public async Task Update(Review review)
        {
            var rev = await context.Set<DAL.Entities.Review>().FindAsync(review.Id);

            rev.Id = review.Id;
            rev.BookId = review.BookId;
            rev.Comment = review.Comment;
            rev.BookId = review.BookId;
            rev.Comment = review.Comment;

            await context.SaveChangesAsync();
        }
    }
}
