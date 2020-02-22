using OnlineBuy.Common.Enums;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace OnlineBuy.Common.Utility
{
    public static class MyExtensions
    {
        public static string GetDescription(this Enum enu)
        {
            Type type = enu.GetType();
            MemberInfo[] memInfo = type.GetMember(enu.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(EnumDescription), false);

                if (attrs != null && attrs.Length > 0)
                    return ((EnumDescription)attrs[0]).Text;
            }

            return enu.ToString();
        }

        public static string GetTitle(this Enum enu)
        {
            Type type = enu.GetType();
            return Enum.GetName(type, enu);
        }
    }
}
