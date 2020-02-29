using OnlineBuy.Common.Messages.Persian;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineBuy.Data.ViewModels
{
    public class ProductUnitViewModel
    {
        public class ProductUnitRegister
        {
            [Required(ErrorMessage = PersianMessages.RequireMessage)]
            public string Name { get; set; }
        }
    }
}
