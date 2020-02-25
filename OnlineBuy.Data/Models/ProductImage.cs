using System;
using System.Collections.Generic;
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

        public string ProductId { get; set; }
        public Product Product { get; set; }
    }
}
