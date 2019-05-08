using BookStoreWebApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWebApp.Services.Dtos
{
    public class LineItem
    {
        public Guid Id { get; set; }
        public int LineNum { get; set; }
        public uint NumBooks { get; set; }
        public decimal BookPrice { get; set; }
        public Guid OrderId { get; set; }
        public Guid BookId { get; set; }
    }
}
