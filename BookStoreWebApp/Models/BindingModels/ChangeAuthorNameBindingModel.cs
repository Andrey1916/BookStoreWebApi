using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWebApp.Models.BindingModels
{
    public class ChangeAuthorNameBindingModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "The value must be no more than {1} and no less than {2}.", MinimumLength = 2)]
        public string OldName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The value must be no more than {1} and no less than {2}.", MinimumLength = 2)]
        public string NewName { get; set; }
    }
}
