﻿using BookStoreWebApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWebApp.DAL.Entities
{
    public class Order : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public DateTime DateOrderedUtc { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public string CustomerName { get; set; }

        public ICollection<LineItem> LineItems { get; set; }
    }
}
