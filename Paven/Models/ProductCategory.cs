using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace Paven.Models
{
    public class ProductCategory
    {
        [Key]        
        public int ProductCatergoryId { get; set; }
                
        public string CategoryName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}