using BookStoreWebApp.DAL.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWebApp.Services.Dtos
{
    public class Author
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class AuthorValidator : AbstractValidator<Author>
    {
        public AuthorValidator()
        {
            this.RuleFor(x => x.Id).NotEqual(Guid.Empty);
            this.RuleFor(x => x.Name).Length(1, 50);
        }
    }
}
