﻿// <auto-generated />
using System;
using BookStoreWebApp.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BookStoreWebApp.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20190515111918_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BookStoreWebApp.DAL.Entities.Author", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("BookStoreWebApp.DAL.Entities.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("ActualPrice")
                        .HasColumnType("decimal(10,3)");

                    b.Property<string>("Description")
                        .IsUnicode(true);

                    b.Property<string>("ImageUrl");

                    b.Property<decimal>("OrgPrice")
                        .HasColumnType("decimal(10,3)");

                    b.Property<string>("PromotionalText")
                        .HasMaxLength(200)
                        .IsUnicode(true);

                    b.Property<DateTime>("PublishedOn");

                    b.Property<string>("Publisher")
                        .IsUnicode(true);

                    b.Property<bool>("SoftDeleted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("Title")
                        .HasMaxLength(100)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("BookStoreWebApp.DAL.Entities.BookAuthor", b =>
                {
                    b.Property<Guid>("AuthorId");

                    b.Property<Guid>("BookId");

                    b.Property<int>("Order");

                    b.HasKey("AuthorId", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("BookAuthors");
                });

            modelBuilder.Entity("BookStoreWebApp.DAL.Entities.LineItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("BookId");

                    b.Property<decimal>("BookPrice")
                        .HasColumnType("decimal(10,3)");

                    b.Property<int>("LineNum")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("NumBooks");

                    b.Property<Guid>("OrderId");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("OrderId");

                    b.ToTable("LineItems");
                });

            modelBuilder.Entity("BookStoreWebApp.DAL.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CustomerName")
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<DateTime>("DateOrderedUtc");

                    b.Property<DateTime>("ExpectedDeliveryDate");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("BookStoreWebApp.DAL.Entities.Review", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("BookId");

                    b.Property<string>("Comment")
                        .IsUnicode(true);

                    b.Property<long>("NumStars");

                    b.Property<string>("VoterName")
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("BookStoreWebApp.DAL.Entities.BookAuthor", b =>
                {
                    b.HasOne("BookStoreWebApp.DAL.Entities.Author", "Author")
                        .WithMany("BookAuthors")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BookStoreWebApp.DAL.Entities.Book", "Book")
                        .WithMany("BookAuthors")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BookStoreWebApp.DAL.Entities.LineItem", b =>
                {
                    b.HasOne("BookStoreWebApp.DAL.Entities.Book", "Book")
                        .WithMany("LineItems")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BookStoreWebApp.DAL.Entities.Order", "Order")
                        .WithMany("LineItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BookStoreWebApp.DAL.Entities.Review", b =>
                {
                    b.HasOne("BookStoreWebApp.DAL.Entities.Book", "Book")
                        .WithMany("Reviews")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
