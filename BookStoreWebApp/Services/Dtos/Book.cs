using BookStoreWebApp.DAL.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWebApp.Services.Dtos
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishedOn { get; set; }
        public string Publisher { get; set; }
        public decimal OrgPrice { get; set; }
        public decimal ActualPrice { get; set; }
        public string PromotionalText { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<string> Authors { get; set; }
    }

    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            this.RuleFor(x => x.Id).NotEqual(Guid.Empty);
            this.RuleFor(x => x.Title).MaximumLength(100);
            this.RuleFor(x => x.OrgPrice).GreaterThanOrEqualTo(0);
            this.RuleFor(x => x.ActualPrice).GreaterThanOrEqualTo(0);
            this.RuleFor(x => x.PromotionalText).MaximumLength(200);
        }
    }
}
