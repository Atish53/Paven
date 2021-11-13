using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Paven.Models
{
    public class Product
    {
        //Keys

        [Key]
        public int ProductId { get; set; } //Primary Key...
        public int ProductCategoryId { get; set; } //Foreign Key... 

        //Product Details

        public string ProductName { get; set; }
        public int ProductStock { get; set; }
        public double ProductPrice { get; set; }

        public byte[] ProductImage { get; set; }

        public bool isActive { get; set; }

        //Virtual Classes

        public virtual ProductCategory ProductCategory { get; set; }
        public virtual List<SaleDetail> SaleDetails { get; set; }


    }
}