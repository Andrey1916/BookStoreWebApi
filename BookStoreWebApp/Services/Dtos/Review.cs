using BookStoreWebApp.DAL.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWebApp.Services.Dtos
{
    public class Review
    {
        public Guid Id { get; set; }
        public string VoterName { get; set; }
        public uint NumStars { get; set; }
        public string Comment { get; set; }
        public Guid BookId { get; set; }
    }

    public class ReviewValidator : AbstractValidator<Review>
    {
        public ReviewValidator()
        {
            this.RuleFor(x => x.Id).NotEqual(Guid.Empty);
            this.RuleFor(x => x.VoterName).MaximumLength(50);
            this.RuleFor(x => x.BookId).NotEqual(Guid.Empty);
        }
    }
}
