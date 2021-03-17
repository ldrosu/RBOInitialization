using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static RBOClientLib.Utils.TypeUtil;

namespace RBOClientLib.Clients
{
    public static class RBOInitTemplate
    {

        public static T Create<T>()
        {
            return (T)Create(typeof(T));
        }

        public static T[] CreateArray<T>()
        {
            return Create<T[]>();
        }

        private static object Create(Type t)
        {
            object obj;
            if (t.IsArray)
            {
                Type elementType = t.GetElementType();
                Array array = Array.CreateInstance(elementType, 1);

                if (IsArray(elementType)||IsObject(elementType))
                {
                    obj = Create(elementType);
                    array.SetValue(obj, 0);
                }                
                return array;
            }
            else if (IsObject(t))
            {
                obj = Activator.CreateInstance(t);
                foreach (var propertyInfo in obj.GetType().GetProperties())
                {
                    Type propertyType = propertyInfo.PropertyType;

                    if (IsArray(propertyType) || IsObject(propertyType))
                    {
                        object val = Create(propertyType);
                        propertyInfo.SetValue(obj, val);
                    }
                }
                return obj;
            }
            throw new InvalidDataException();
        }
    }
}
