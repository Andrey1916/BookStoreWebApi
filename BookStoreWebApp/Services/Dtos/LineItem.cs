using BookStoreWebApp.DAL.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWebApp.Services.Dtos
{
    public class LineItem
    {
        public Guid Id { get; set; }
        public int LineNum { get; set; }
        public uint NumBooks { get; set; }
        public decimal BookPrice { get; set; }
        public Guid OrderId { get; set; }
        public Guid BookId { get; set; }
    }

    public class LineItemValidator : AbstractValidator<LineItem>
    {
        public LineItemValidator()
        {
            this.RuleFor(x => x.Id).NotEqual(Guid.Empty);
            this.RuleFor(x => x.BookPrice).GreaterThanOrEqualTo(0);
            this.RuleFor(x => x.OrderId).NotEqual(Guid.Empty);
            this.RuleFor(x => x.BookId).NotEqual(Guid.Empty);
        }
    }
}
