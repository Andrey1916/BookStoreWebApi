using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWebApp.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Guid> AddAsync(Dtos.Order order);
        Task<IEnumerable<Dtos.OrderInfo>> GetAllAsync();
        Task<Dtos.OrderInfo> GetByIdAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task<IEnumerable<Dtos.LineItem>> GetOrderLineItemsAsync(Guid orderId);
    }
}
