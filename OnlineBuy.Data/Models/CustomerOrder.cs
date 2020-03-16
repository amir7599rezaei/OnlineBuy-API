using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBuy.Data.Models
{
    public class CustomerOrder:BaseEntity<string>
    {
        public CustomerOrder()
        {
            Id = Guid.NewGuid().ToString();
        }

        public Product Product { get; set; }
        public string ProductId { get; set; }

        public Customer Customer { get; set; }
        public string CustomerId { get; set; }

        public ProductUnit ProductUnit { get; set; }
        public int ProductUnitId { get; set; }

        public int CartCount { get; set; }
        public double FinalPrice { get; set; }
        public DateTime OrderDate { get; set; }

        public int? StatusCode { get; set; }
        public string StatusText { get; set; }
    }
}
