using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWebApp.Services.Interfaces
{
    public interface ILineItemService
    {
        Task<Guid> AddAsync(Dtos.LineItem lineItem);
        Task<IEnumerable<Dtos.LineItem>> GetAllAsync();
        Task<Dtos.LineItem> GetByIdAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(Dtos.LineItem lineItem);
    }
}
