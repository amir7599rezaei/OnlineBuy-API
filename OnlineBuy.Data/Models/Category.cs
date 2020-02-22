using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineBuy.Data.Models
{
    public class Category : BaseEntity<string>
    {
        public Category()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Name { get; set; }
        public ICollection<Product> Products{ get; set; }        
    }
}
