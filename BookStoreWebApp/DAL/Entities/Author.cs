using BookStoreWebApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWebApp.DAL.Entities
{
    public class Author : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
