using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineBuy.Data.Models
{
    public class ProductUnit:BaseEntity<int>
    {
        public ProductUnit()
        {
            Id = new int();
        }

        [Required]
        public string Name { get; set; }
        public ICollection<ProductPrice> ProductPrices { get; set; }
    }
}
