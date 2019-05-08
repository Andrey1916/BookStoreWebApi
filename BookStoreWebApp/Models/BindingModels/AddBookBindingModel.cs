using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWebApp.Models.BindingModels
{
    public class AddBookBindingModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishedOn { get; set; }
        public string Publisher { get; set; }
        public decimal OrgPrice { get; set; }
        public decimal ActualPrice { get; set; }
        public string PromotionalText { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<string> Authors { get; set; }
    }
}
