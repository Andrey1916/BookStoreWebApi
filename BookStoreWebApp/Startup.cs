﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStoreWebApp.DAL;
using BookStoreWebApp.Services;
using BookStoreWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using FluentValidation.AspNetCore;
using BookStoreWebApp.Services.Dtos;
using FluentValidation;

namespace BookStoreWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                    .AddFluentValidation();

            services.AddTransient<IValidator<Author>, AuthorValidator>();
            services.AddTransient<IValidator<Book>, BookValidator>();
            services.AddTransient<IValidator<LineItem>, LineItemValidator>();
            services.AddTransient<IValidator<Order>, OrderValidator>();
            services.AddTransient<IValidator<OrderInfo>, OrderInfoValidator>();
            services.AddTransient<IValidator<Review>, ReviewValidator>();

            services.AddAutoMapper();

            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddSwaggerGen(c =>
               c.SwaggerDoc("v1", new Info
               {
                   Title = "Book Store API",
                   Version = "v1",
                   Description = "Book Store API"
               })
                );

            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<ILineItemService, LineItemService>();

            services.AddScoped<DbContext, DataContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Book Store API");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
