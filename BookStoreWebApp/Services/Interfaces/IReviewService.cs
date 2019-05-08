using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWebApp.Services.Interfaces
{
    public interface IReviewService
    {
        Task<Guid> AddAsync(Dtos.Review review);
        Task<Dtos.Review> GetByIdAsync(Guid id);
        Task<IEnumerable<Dtos.Review>> GetAllAsync();
        Task Update(Dtos.Review review);
        Task Remove(Guid id);
    }
}
