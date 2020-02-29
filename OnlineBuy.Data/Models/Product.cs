using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineBuy.Data.Models
{
    public class Product : BaseEntity<string>
    {
        public Product() : base()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Name { get; set; }
        
        public string Title { get; set; }

        public bool IsExits { get; set; }

        [Required]
        public string Provider { get; set; }

        public string BarcodeNumber { get; set; }

        public string Description { get; set; }
        
        public DateTime ProductDate { get; set; }        
        public DateTime ExpireDate { get; set; }

        [Required]
        public string CategoryId { get; set; }
        public Category Category { get; set; }
        
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }

        public ICollection<ProductPrice> ProductPrices { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
    }
}
