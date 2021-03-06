﻿using BookStoreWebApp.DAL.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWebApp.Services.Dtos
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime DateOrderedUtc { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public string CustomerName { get; set; }

        public ICollection<LineItem> LineItems { get; set; }
    }

    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            this.RuleFor(x => x.Id).NotEqual(Guid.Empty);
            this.RuleFor(x => x.CustomerName).Length(1, 50);
        }
    }
}
