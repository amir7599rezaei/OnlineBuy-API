using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineBuy.Data.Models
{
    public class Customer : BaseEntity<string>
    {
        public Customer() : base()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Family { get; set; }
        [Required]
        public string UserName { get; set; }
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        public string Tel { get; set; }
        public string Mobile { get; set; }
        public string PostalCode { get; set; }
        [Required]
        public bool IsActive { get; set; }        
        public byte[] SaltPassword { get; set; }
        public byte[] HashPassword { get; set; }
        public ICollection<Product> Products { get; set; }        
    }
}
