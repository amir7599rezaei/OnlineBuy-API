using OnlineBuy.Common.Messages.Persian;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineBuy.Data.ViewModels
{
    public class ProductViewModel
    {
        public class ProductRegister
        {

            [Required(ErrorMessage = PersianMessages.RequireMessage)]
            public string Name { get; set; }

            public string Title { get; set; }

            public bool IsExits { get; set; }

            [Required(ErrorMessage = PersianMessages.RequireMessage)]
            public string Provider { get; set; }

            public string BarcodeNumber { get; set; }

            public string Description { get; set; }

            //[Required(ErrorMessage = PersianMessages.RequireMessage)]
            public DateTime ProductDate { get; set; }
            //[Required(ErrorMessage = PersianMessages.RequireMessage)]
            public DateTime ExpireDate { get; set; }

            [Required(ErrorMessage = PersianMessages.RequireMessage)]
            public string CategoryId { get; set; }

            public ICollection<ProductPriceRegister> ProductPriceRegisters { set; get; }
        }


        public class ProductPriceRegister
        {

            [Required(ErrorMessage = PersianMessages.RequireMessage)]
            public int ProductUnitId { get; set; }

            public double PrimaryPrice { get; set; }

            public double FinalPrice { get; set; }

            [Required(ErrorMessage = PersianMessages.RequireMessage)]
            public double ShowPrice { get; set; }

            public double OffPrice { get; set; }

            public int OffPercent { get; set; }
        }

        public class CartRegister
        {                        
            public ICollection<ProductOrder> ProductOrders { get; set; }
        }

        public class ProductOrder
        {
            public string ProductId { get; set; }
            public int ProductUnitId { get; set; }
            public int CartCount { get; set; }            
        }

        public class ImgaeRegister
        {
            public string ProductId { get; set; }
            public string Image { get; set; }
        }

    }

}
