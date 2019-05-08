using BookStoreWebApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWebApp.DAL.Configurations
{
    public class LineItemConfiguration : IEntityTypeConfiguration<LineItem>
    {
        public void Configure(EntityTypeBuilder<LineItem> builder)
        {
            builder.HasKey(t => t.Id);

            // TODO: Check auto-increment
            builder.Property(t => t.LineNum)
                .ValueGeneratedOnAdd();

            builder.HasOne(b => b.Book)
                .WithMany(i => i.LineItems)
                .HasForeignKey(fk => fk.BookId);

            builder.HasOne(b => b.Order)
                .WithMany(i => i.LineItems)
                .HasForeignKey(fk => fk.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(t => t.Order)
                .IsRequired();
        }
    }
}
