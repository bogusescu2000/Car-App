using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace InAndOut.Models
{
    public class Item
    {
        [Key]

        public int Id { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "Name to long")]
        public string Borrower { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "Name to long")]
        public string Lender { get; set; }
        [DisplayName("Item name")]
        [Required]
        [StringLength(10, ErrorMessage = "Name to long")]
        public string ItemName { get; set; }
    }
}
