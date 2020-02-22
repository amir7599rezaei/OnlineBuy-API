using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineBuy.Data.Models
{
    public class CustomerSmsCode : BaseEntity<string>
    {
        public CustomerSmsCode()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public int SmsCode { get; set; }
        [Required]
        public double DurationCode { get; set; }
        [Required]
        public TimeSpan SentTime { get; set; }        
        public TimeSpan RecievedTime { get; set; }
        [Required]
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }

    }
}
