using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWebApp.Services.Interfaces
{
    public interface IBookService
    {
        Task<Guid> AddAsync(Dtos.Book book);
        Task<IEnumerable<Dtos.Book>> GetAllAsync();
        Task<Dtos.Book> GetByIdAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(Dtos.Book book);
        Task<IEnumerable<Dtos.Review>> GetAllBookReviews(Guid bookId);
    }
}
