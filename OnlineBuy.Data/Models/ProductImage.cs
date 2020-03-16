using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineBuy.Data.Models
{
    public class ProductImage : BaseEntity<string>
    {
        public ProductImage()
        {
            Id = Guid.NewGuid().ToString();
        }

        public byte[] Content { get; set; }
        public string FormatContent { get; set; }

        [Required]
        public string ProductId { get; set; }
        public Product Product { get; set; }
    }
}
