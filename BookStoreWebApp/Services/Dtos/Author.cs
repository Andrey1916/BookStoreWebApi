using BookStoreWebApp.DAL.Interfaces;
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
}
