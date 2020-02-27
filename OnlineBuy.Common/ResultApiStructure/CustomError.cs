using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBuy.Common.ResultApiStructure
{
    public class CustomError
    {
        public string Status { get; }
        public string Message { get; }
        public int Code { get; set; }


        public CustomError(string message)
        {
            Status = "FailedAuthentication";
            Code = -1;
            Message = message;
        }
    }
}
