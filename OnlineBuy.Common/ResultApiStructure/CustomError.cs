using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBuy.Common.ResultApiStructure
{
    public class CustomError
    {
        public bool IsAuthenticate { get; }
        public string Message { get; }


        public CustomError(string message)
        {
            IsAuthenticate = false;
            Message = message;
        }
    }
}
