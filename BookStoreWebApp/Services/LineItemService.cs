using BookStoreWebApp.Services.Dtos;
using BookStoreWebApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWebApp.Services
{
    public class LineItemService : ILineItemService
    {
        private readonly DbContext context;

        public LineItemService(DbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Guid> AddAsync(LineItem lineItem)
        {
            var id = Guid.NewGuid();
            var entity = new DAL.Entities.LineItem
            {
                BookId = lineItem.BookId,
                BookPrice = lineItem.BookPrice,
                Id = id,
                LineNum = lineItem.LineNum,
                NumBooks = lineItem.NumBooks,
                OrderId = lineItem.OrderId
            };

            await context.AddAsync(entity);
            await context.SaveChangesAsync();
            return id;
        }

        public async Task<IEnumerable<LineItem>> GetAllAsync()
        {
            var lineItems = await context.Set<DAL.Entities.LineItem>()
                                         .Select(li => new LineItem
                                         {
                                             BookId = li.BookId,
                                             BookPrice = li.BookPrice,
                                             Id = li.Id,
                                             LineNum = li.LineNum,
                                             NumBooks = li.NumBooks,
                                             OrderId = li.OrderId
                                         })
                                         .ToListAsync();
            return lineItems;
        }

        public async Task<LineItem> GetByIdAsync(Guid id)
        {
            var lineItem = await context.Set<DAL.Entities.LineItem>()
                                        .FindAsync(id);
            return new LineItem
            {
                BookId = lineItem.BookId,
                BookPrice = lineItem.BookPrice,
                Id = lineItem.Id,
                LineNum = lineItem.LineNum,
                NumBooks = lineItem.NumBooks,
                OrderId = lineItem.OrderId
            };
        }

        public async Task RemoveAsync(Guid id)
        {
            var li = await context.Set<DAL.Entities.LineItem>()
                                  .FindAsync(id);
            if (li != null)
            {
                context.Remove(li);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(LineItem lineItem)
        {
            context.Update(
                new DAL.Entities.LineItem
                {
                    BookId = lineItem.BookId,
                    BookPrice = lineItem.BookPrice,
                    Id = lineItem.Id,
                    LineNum = lineItem.LineNum,
                    NumBooks = lineItem.NumBooks,
                    OrderId = lineItem.OrderId
                });

            await context.SaveChangesAsync();
        }
    }
}
