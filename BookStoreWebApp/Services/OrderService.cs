using BookStoreWebApp.Services.Dtos;
using BookStoreWebApp.Services.Interfaces;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWebApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly DbContext context;

        public OrderService(DbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Guid> AddAsync(Order order)
        {
            OrderValidator validator = new OrderValidator();
            ValidationResult result = validator.Validate(order);

            if (!result.IsValid)
            {
                string errMess = string.Empty;

                foreach (var failure in result.Errors)
                {
                    errMess += $"Property { failure.PropertyName } failed validation. Error was: { failure.ErrorMessage }\n";
                }

                throw new ArgumentException(errMess);
            }

            var id = Guid.NewGuid();
            var entity = new DAL.Entities.Order
            {
                CustomerName = order.CustomerName,
                DateOrderedUtc = order.DateOrderedUtc,
                ExpectedDeliveryDate = order.ExpectedDeliveryDate,
                Id = id
            };

            await context.AddAsync(entity);

            foreach(var lineItem in order.LineItems)
            {
                var lineItemEntity = new DAL.Entities.LineItem
                {
                    BookId = lineItem.BookId,
                    Id = Guid.NewGuid(),
                    BookPrice = lineItem.BookPrice,
                    NumBooks = lineItem.NumBooks,
                    OrderId = id
                };

                await context.AddAsync(lineItemEntity);
            }

            return id;
        }

        public async Task<IEnumerable<OrderInfo>> GetAllAsync()
        {
            var orders = await context.Set<DAL.Entities.Order>()
                                      .Select(item => new OrderInfo
                                      {
                                          Id = item.Id,
                                          CustomerName = item.CustomerName,
                                          DateOrderedUtc = item.DateOrderedUtc,
                                          ExpectedDeliveryDate = item.ExpectedDeliveryDate,
                                      })
                                      .ToListAsync();
            return orders;
        }

        public async Task<OrderInfo> GetByIdAsync(Guid id)
        {
            var order = await context.Set<DAL.Entities.Order>()
                                     .FindAsync(id);

            return new OrderInfo
            {
                CustomerName = order.CustomerName,
                DateOrderedUtc = order.DateOrderedUtc,
                ExpectedDeliveryDate = order.ExpectedDeliveryDate,
                Id = order.Id
            };
        }

        public async Task<IEnumerable<LineItem>> GetOrderLineItemsAsync(Guid orderId)
        {
            var lineItems = await context.Set<DAL.Entities.LineItem>()
                                         .Where(li => li.OrderId == orderId)
                                         .Select(item => new LineItem
                                         {
                                             BookId = item.BookId,
                                             OrderId = item.OrderId,
                                             BookPrice = item.BookPrice,
                                             Id = item.Id,
                                             LineNum = item.LineNum,
                                             NumBooks = item.NumBooks
                                         })
                                         .ToListAsync();

            return lineItems;
        }

        public async Task RemoveAsync(Guid id)
        {
            var order = await context.Set<DAL.Entities.Order>()
                                     .FindAsync(id);

            if (order == null)
            {
                throw new ArgumentException("Order not found.");
            }

            context.Remove(order);
        }
    }
}