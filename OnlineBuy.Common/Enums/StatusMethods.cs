using OnlineBuy.Common.Messages.Persian;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBuy.Common.Enums
{
    public enum StatusMethods
    {
        [EnumDescription(PersianMessages.SuccessRegister)]
        SuccessRegister = 1,

        [EnumDescription(PersianMessages.DuplicateRegister)]
        DuplicateRegister,

        [EnumDescription(PersianMessages.NotSuccessRequest)]
        OperationFailed,

        [EnumDescription(PersianMessages.SuccessSmsCode)]
        SuccessSmsCode,

        [EnumDescription(PersianMessages.WrongCodeOrMobile)]
        WrongCodeOrMobile,

        [EnumDescription(PersianMessages.PastTime)]
        PastTime,

        [EnumDescription(PersianMessages.SuccessRecieveCode)]
        SuccessRecieveCode,

        [EnumDescription(PersianMessages.SuccessCreateToken)]
        SuccessCreateToken,

        [EnumDescription(PersianMessages.FailedSmsCode)]
        FailedSmsCode
    }
}
