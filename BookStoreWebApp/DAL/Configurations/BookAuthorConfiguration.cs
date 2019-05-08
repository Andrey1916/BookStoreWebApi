using BookStoreWebApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWebApp.DAL.Configurations
{
    public class BookAuthorConfiguration : IEntityTypeConfiguration<BookAuthor>
    {
        public void Configure(EntityTypeBuilder<BookAuthor> builder)
        {
            builder.HasKey(t => new { t.AuthorId, t.BookId });

            builder.HasOne(t => t.Author)
                .WithMany(t => t.BookAuthors)
                .HasForeignKey(t => t.AuthorId);

            builder.HasOne(t => t.Book)
                .WithMany(t => t.BookAuthors)
                .HasForeignKey(t => t.BookId);
        }
    }
}
