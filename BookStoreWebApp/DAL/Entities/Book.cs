using BookStoreWebApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWebApp.DAL.Entities
{
    public class Book : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishedOn { get; set; }
        public string Publisher { get; set; }
        public decimal OrgPrice { get; set; }
        public decimal ActualPrice { get; set; }
        public string PromotionalText { get; set; }
        public string ImageUrl { get; set; }
        public bool SoftDeleted { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<LineItem> LineItems { get; set; }
    }
}
