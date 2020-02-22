using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBuy.Common.ResultApiStructure
{
    public class ReturnApiMessages
    {
        public string Status { get; set; }
        public string Title { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public string IdentifyCode { get; set; }
        public string Token { get; set; }
        public bool IsRecievedCode { get; set; }
        public bool IsRegistered { get; set; }
        public int SmsCode { get; set; }
        public double DurationTime { get; set; }
    }
}
