using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineBuy.Data.Models
{
    public class ProductPrice : BaseEntity<string>
    {
        public ProductPrice()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Name { get; set; }
        
        public double PrimaryPrice { get; set; }

        public double FinalPrice { get; set; }

        [Required]
        public double ShowPrice { get; set; }

        public double OffPrice { get; set; }

        public int OffPercent { get; set; }

        [Required]
        public string ProductId { get; set; }
        public Product Product { get; set; }

        [Required]
        public int ProductUnitId { get; set; }
        public ProductUnit ProductUnit { get; set; }

    }
}
