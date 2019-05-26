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
    public class BookService : IBookService
    {
        private readonly DbContext context;

        public BookService(DbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Guid> AddAsync(Book book)
        {
            BookValidator validator = new BookValidator();
            ValidationResult result = validator.Validate(book);

            if (!result.IsValid)
            {
                string errMess = string.Empty;

                foreach (var failure in result.Errors)
                {
                    errMess += $"Property { failure.PropertyName } failed validation. Error was: { failure.ErrorMessage }\n";
                }

                throw new ArgumentException(errMess);
            }

            var idBook = Guid.NewGuid();
            book.Id = idBook;

            var entity = new DAL.Entities.Book
            {
                Id = idBook,
                ActualPrice = book.ActualPrice,
                Description = book.Description,
                ImageUrl = book.ImageUrl,
                OrgPrice = book.OrgPrice,
                PromotionalText = book.PromotionalText,
                PublishedOn = book.PublishedOn,
                Publisher = book.Publisher,
                Title = book.Title
            };

            await context.AddAsync(entity);

            foreach (var author in book.Authors)
            {
                var _author = context.Set<DAL.Entities.Author>()
                                     .FirstOrDefault(a => a.Name == author);
                if (_author == null)
                {
                    var idAuthor = Guid.NewGuid();

                    context.Add(
                        new DAL.Entities.Author
                        {
                            Id = idAuthor,
                            Name = author
                        }
                        );

                    context.Add(
                        new DAL.Entities.BookAuthor
                        {
                            AuthorId = idAuthor,
                            BookId = idBook
                        }
                        );
                }
                else
                {
                    context.Add(
                        new DAL.Entities.BookAuthor
                        {
                            AuthorId = _author.Id,
                            BookId = idBook
                        }
                        );
                }
            }
            
            await context.SaveChangesAsync();
            return idBook;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            var books = await context.Set<DAL.Entities.Book>()
                                     .Select(b =>
                                     new Book
                                     {
                                         Authors = GetBookAuthors(b.Id).Result.ToList(),
                                         ActualPrice = b.ActualPrice,
                                         Description = b.Description,
                                         Id = b.Id,
                                         ImageUrl = b.ImageUrl,
                                         OrgPrice = b.OrgPrice,
                                         PromotionalText = b.PromotionalText,
                                         PublishedOn = b.PublishedOn,
                                         Publisher = b.Publisher,
                                         Title = b.Title
                                     }
                                     )
                                     .ToListAsync();

            return books;
        }

        public async Task<Book> GetByIdAsync(Guid id)
        {
            var book = await context.Set<DAL.Entities.Book>()
                                    .FirstOrDefaultAsync(b => b.Id == id && !b.SoftDeleted);
            return new Book
            {
                Authors = (await GetBookAuthors(id)).ToList(),
                ActualPrice = book.ActualPrice,
                Description = book.Description,
                Id = book.Id,
                ImageUrl = book.ImageUrl,
                OrgPrice = book.OrgPrice,
                PromotionalText = book.PromotionalText,
                PublishedOn = book.PublishedOn,
                Publisher = book.Publisher,
                Title = book.Title
            };
        }

        public async Task RemoveAsync(Guid id)
        {
            var book = await context.Set<DAL.Entities.Book>()
                                    .FirstOrDefaultAsync(b => b.Id == id && !b.SoftDeleted);
            book.SoftDeleted = true;

            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Book book)
        {
            BookValidator validator = new BookValidator();
            ValidationResult result = validator.Validate(book);

            if (!result.IsValid)
            {
                string errMess = string.Empty;

                foreach (var failure in result.Errors)
                {
                    errMess += $"Property { failure.PropertyName } failed validation. Error was: { failure.ErrorMessage }\n";
                }

                throw new ArgumentException(errMess);
            }

            var bookEntity = new DAL.Entities.Book
            {
                ActualPrice = book.ActualPrice,
                Description = book.Description,
                Id = book.Id,
                ImageUrl = book.ImageUrl,
                OrgPrice = book.OrgPrice,
                PromotionalText = book.PromotionalText,
                PublishedOn = book.PublishedOn,
                Publisher = book.Publisher,
                Title = book.Title
            };
            
            await context.Set<DAL.Entities.BookAuthor>()
                         .Where(ab => ab.BookId == book.Id)
                         .Include(p => p.Author)
                         .ForEachAsync(item => {
                             if (item.Author.Name != "") return;
                         });

            context.Update<DAL.Entities.Book>(bookEntity);
            await context.SaveChangesAsync();
        }

        private async Task<IEnumerable<string>> GetBookAuthors(Guid bookId)
        {
            return await context.Set<DAL.Entities.BookAuthor>()
                                .Where(item => item.BookId == bookId)
                                .Include(p => p.Author)
                                .OrderBy(p => p.Order)
                                .Select(a => a.Author.Name)
                                .ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetAllBookReviews(Guid bookId)
        {
            return await context.Set<DAL.Entities.Review>()
                                .Where(rev => rev.BookId == bookId)
                                .Select(rev => new Review
                                {
                                    BookId = rev.BookId,
                                    Comment = rev.Comment,
                                    Id = rev.Id,
                                    NumStars = rev.NumStars,
                                    VoterName = rev.VoterName
                                })
                                .ToListAsync();
        }
    }
}
