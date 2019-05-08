using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWebApp.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<Guid> AddAsync(string author);
        Task<IEnumerable<string>> GetAllAsync();
        Task<IEnumerable<Dtos.Book>> GetAuthorBooksAsync(string author);
        Task<string> GetByIdAsync(Guid id);
        Task RemoveAsync(string author);
        Task ChangeNameAsync(string oldName, string newName);
    }
}
