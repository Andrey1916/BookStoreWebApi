using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWebApp.Models.ViewModels
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public DateTime DateOrderedUtc { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public string CustomerName { get; set; }
        //TODO:sdfsdf
        public ICollection<LineItemViewModel> LineItems { get; set; }
    }
}
