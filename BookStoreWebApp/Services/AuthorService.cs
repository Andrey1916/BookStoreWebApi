using BookStoreWebApp.Services.Dtos;
using BookStoreWebApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWebApp.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly DbContext context;

        public AuthorService(DbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Guid> AddAsync(string author)
        {
            var _author = await context.Set<DAL.Entities.Author>()
                                       .FirstOrDefaultAsync(a => a.Name == author);
            if (_author != null)
            {
                return _author.Id;
            }

            var id = Guid.NewGuid();

            await context.AddAsync(
                new DAL.Entities.Author
                {
                    Id = id,
                    Name = author
                }
                );

            await context.SaveChangesAsync();

            return id;
        }

        public async Task ChangeNameAsync(string oldName, string newName)
        {
            var _old = await context.Set<DAL.Entities.Author>()
                                       .FirstOrDefaultAsync(a => a.Name == oldName);

            if (_old == null)
            {
                throw new ArgumentException("No authors with name " + oldName);
            }

            var _new = await context.Set<DAL.Entities.Author>()
                                       .FirstOrDefaultAsync(a => a.Name == newName);

            if (_new != null)
            {
                throw new ArgumentException("This name is already in use.");
            }

            _old.Name = newName;

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<string>> GetAllAsync()
        {
            var author = await context.Set<DAL.Entities.Author>()
                                      .Select(a => a.Name)
                                      .ToListAsync();

            return author;
        }

        public async Task<IEnumerable<Book>> GetAuthorBooksAsync(string author)
        {
            var _author = await context.Set<DAL.Entities.Author>()
                                               .Include(p => p.BookAuthors)
                                               .FirstOrDefaultAsync(a => a.Name == author);
            if (_author == null)
            {
                throw new ArgumentException("Author not found.");
            }

            var books = await context.Set<DAL.Entities.BookAuthor>()
                                     .Where(ba => ba.AuthorId == _author.Id)
                                     .Include(t => t.Book)
                                     .Select(ba => new Book
                                     {
                                         Id = ba.Book.Id,
                                         ActualPrice = ba.Book.ActualPrice,
                                         Description = ba.Book.Description,
                                         ImageUrl = ba.Book.ImageUrl,
                                         OrgPrice = ba.Book.OrgPrice,
                                         PromotionalText = ba.Book.PromotionalText,
                                         PublishedOn = ba.Book.PublishedOn,
                                         Publisher = ba.Book.Publisher,
                                         Title = ba.Book.Title,
                                         Authors = context.Set<DAL.Entities.BookAuthor>()
                                                          .Where(item => item.BookId == ba.Book.Id)
                                                          .Include(p => p.Author)
                                                          .OrderBy(p => p.Order)
                                                          .Select(a => a.Author.Name)
                                                          .ToList()
                                     }
                                     )
                                     .ToListAsync();

            return books;
        }

        public async Task<string> GetByIdAsync(Guid id)
        {
            var author = await context.Set<Author>().FindAsync(id);
            return author.Name;
        }

        public async Task RemoveAsync(string author)
        {
            var _author = await context.Set<DAL.Entities.Author>()
                                       .FirstOrDefaultAsync(a => a.Name == author);

            if (_author == null)
            {
                return;
            }

            context.Remove(author);
            await context.SaveChangesAsync();
        }
    }
}
