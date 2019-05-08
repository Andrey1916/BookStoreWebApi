using BookStoreWebApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWebApp.DAL.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.VoterName)
                .HasMaxLength(50)
                .IsUnicode(true);

            builder.Property(t => t.Comment)
                .IsUnicode(true);

            builder.HasOne(t => t.Book)
                .WithMany(t => t.Reviews)
                .HasForeignKey(t => t.BookId);
        }
    }
}
