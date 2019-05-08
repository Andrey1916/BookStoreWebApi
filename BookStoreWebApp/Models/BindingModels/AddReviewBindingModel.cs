using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWebApp.Models.BindingModels
{
    public class AddReviewBindingModel
    {
        [MaxLength(50)]
        public string VoterName { get; set; }
        public uint NumStars { get; set; }
        public string Comment { get; set; }
        public Guid BookId { get; set; }
    }
}
