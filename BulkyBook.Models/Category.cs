using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BulkyBook.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string DisplayOrder { get; set; }
        public DateTime? CreatedDate { get; set;  }
        //[Column()]
        //[DisplayFormat(NullDisplayText ="The Value is Null")]
        //[Range(3,20)]
        //[Range(Type.)]
    }
}
