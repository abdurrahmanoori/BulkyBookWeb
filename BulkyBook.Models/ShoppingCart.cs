using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace BulkyBook.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

         [ValidateNever]
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Range(1, 1000, ErrorMessage = "Please enter a value between 1 and 1000")]
        public int Count { get; set; }

        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]//public ApplicationUser ApplicationUser { get; set; }//[ValidateNever]

        //This property is a place holder. Means in our business logic price will change based on number of item
        //that is why define a new property (Price).
        [NotMapped]
        public double Price { get; set; }
    }
}
