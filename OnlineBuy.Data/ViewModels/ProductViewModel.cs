using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineBuy.Data.ViewModels
{
    public class ProductViewModel
    {
        [Required]
        public string Name { get; set; }

        public string Title { get; set; }

        public bool IsExits { get; set; }

        [Required]
        public string Provider { get; set; }

        public string BarcodeNumber { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime ProductDate { get; set; }
        [Required]
        public DateTime ExpireDate { get; set; }

        [Required]
        public string CategoryId { get; set; }

        [Required]
        public string ProductUnitId { get; set; }

        public double PrimaryPrice { get; set; }

        public double FinalPrice { get; set; }

        [Required]
        public double ShowPrice { get; set; }

        public double OffPrice { get; set; }

        public int OffPercent { get; set; }
    }
}
