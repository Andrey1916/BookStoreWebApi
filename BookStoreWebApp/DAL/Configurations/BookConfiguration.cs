using BookStoreWebApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWebApp.DAL.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(t => t.Title)
                .HasMaxLength(100)
                .IsUnicode(true);

            builder.Property(t => t.Description)
                .IsUnicode(true);

            builder.Property(t => t.Publisher)
                .IsUnicode(true);

            builder.Property(t => t.PromotionalText)
                .HasMaxLength(200)
                .IsUnicode(true);

            builder.Property(t => t.SoftDeleted)
                .HasDefaultValue<bool>(false);

            builder.Property(t => t.ActualPrice)
                .HasColumnType("decimal(10,3)");

            builder.Property(t => t.OrgPrice)
                .HasColumnType("decimal(10,3)");
        }
    }
}
