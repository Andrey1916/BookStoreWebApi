using BookStoreWebApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWebApp.DAL.Entities
{
    public class Review : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string VoterName { get; set; }
        public uint NumStars { get; set; }
        public string Comment { get; set; }
        public Guid BookId { get; set; }
        public Book Book { get; set; }
    }
}
