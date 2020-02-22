using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBuy.Common.Enums
{
    public class EnumDescription: Attribute
    {
        public string Text;
        public EnumDescription(string text)
        {
            Text = text;
        }
    }
}
