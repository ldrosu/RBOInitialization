using System;
using System.Collections.Generic;
using System.Text;

namespace RBOClientLib.Utils
{
    public static class TypeUtil
    {
        public static bool IsNumeric(Type t)
        {
            var code = Type.GetTypeCode(t);
            switch (code)
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }
        public static bool IsString(Type t)
        {
            var code = Type.GetTypeCode(t);
            return code == TypeCode.String;
        }

        public static bool IsBoolean(Type t)
        {
            var code = Type.GetTypeCode(t);
            return code == TypeCode.Boolean;
        }

        public static bool IsData(Type t)
        {
            return IsNumeric(t) || IsString(t) || IsBoolean(t);
        }

        public static bool IsArray(Type t)
        {
            return t.IsArray;
        }

        public static bool IsObject(Type t)
        {
            return (t.IsClass && !(IsArray(t) || IsData(t)));
        }
    }
}
