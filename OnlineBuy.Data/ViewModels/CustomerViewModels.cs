using OnlineBuy.Common.Messages.Persian;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineBuy.Data.ViewModels
{
    public class CustomerDot
    {
        public class CustomerRegister
        {
            [Required(ErrorMessage = PersianMessages.RequireMessage)]
            public string Name { get; set; }

            [Required(ErrorMessage = PersianMessages.RequireMessage)]
            public string Family { get; set; }

            [Required(ErrorMessage = PersianMessages.RequireMessage)]
            public string Address { get; set; }

            [Required(ErrorMessage = PersianMessages.RequireMessage)]
            public string Tel { get; set; }

            [Required(ErrorMessage = PersianMessages.RequireMessage)]
            public string Mobile { get; set; }

            [Required(ErrorMessage = PersianMessages.RequireMessage)]
            public string UserName { get; set; }
            public string PostalCode { get; set; }
        }

        public class CustomerValidation
        {
            [Required(ErrorMessage = PersianMessages.RequireMessage)]
            public string Mobile { get; set; }           
        }

        public class CustomerSendCode
        {
            public string Mobile { get; set; }            
        }

        public class CustomerRecieveCode
        {            
            public string Mobile { get; set; }          

            [Required(ErrorMessage = PersianMessages.RequireMessage)]
            public int Code { get; set; }
        }
    }
}
