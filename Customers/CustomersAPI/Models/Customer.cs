using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CustomersAPI.Models
{
    public class Customer
    {
        public long Id { get; set; }

        [Display(Name = "Name")]
        public string? Name { get; set; }

        [Display(Name = "Address")]
        public string? Address { get; set; }

        [Display(Name = "Contact No.")]
        public string? ContactNo { get; set; }

        [Display(Name = "Email Address")]
        public string? Email { get; set; }

        [Display(Name = "Active?")]
        public bool IsActive { get; set; }
    }
}
