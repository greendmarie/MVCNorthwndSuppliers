using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVCNorthwndCRUD.Models
{
    public class SupplierPO
    {

        //Declaring all object properties for supplier.
        public int SupplierId { get; set; }

        [Display(Name = "Contact Name")]
        [StringLength(30)]
        [Required]
        public string ContactName { get; set; }

        [Display(Name = "Contact Title")]
        [StringLength(30)]
        public string ContactTitle { get; set; }

        [Display(Name = "Zip Code")]
        [StringLength(10, MinimumLength = 5, ErrorMessage ="Zip code must be between 5 and 10 characters long!")]
        [Range(0, 9999999999)]
        [Required]
        public string PostalCode { get; set; }

        [StringLength(50)]
        [Required]
        public string Country { get; set; }

        [Display(Name = "Phone Number")]
        [StringLength(15,MinimumLength =7, ErrorMessage ="Phone number must be between 7 and 15 characters long!")]
        [Range(0,999999999999999)]
        [Required]
        public string PhoneNumber { get; set; }


    }
}