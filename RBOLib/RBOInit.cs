using System;
using System.IO;
using RBOLib.Initializations;
using static RBOLib.Utils.TypeUtil;
using static RBOLib.Utils.RegexUtil;

namespace RBOLib
{
    public class RBOInit
    {
        private Initializer initializer = new Initializer();

        public void AddRule(string pattern, SourceTypeEnum source, params object[] parameters)
        {
            string regexPatern = ToRegex(pattern);
            initializer.AddRule(regexPatern, source, parameters);
        }
        
        public T Create<T>()
        {
            return (T)Create(typeof(T), new MemberPath());
        }

        public T[] CreateArray<T>(int n = 1)
        {
            initializer.AddRule("^$", SourceTypeEnum.Value, n);
            return (T[])Create<T[]>();
        }

        private object Create(Type t, MemberPath path)
        {
            object obj;
            if (t.IsArray)
            {
                Type elementType = t.GetElementType();
                int n = initializer.InitializeCount(path);
                Array array = Array.CreateInstance(elementType, n);

                for (int i = array.GetLowerBound(0); i <= array.GetUpperBound(0); i++)
                {
                    if (IsData(elementType))
                        initializer.InitializeElement(path.AddArrayElement(i), array, i);
                    else
                    {
                        obj = Create(elementType, path.AddArrayElement(i));
                        array.SetValue(obj, i);
                    }
                }
                return array;
            }
            else if (IsObject(t))
            {
                obj = Activator.CreateInstance(t);
                foreach (var propertyInfo in obj.GetType().GetProperties())
                {
                    Type propertyType = propertyInfo.PropertyType;

                    if (IsData(propertyType))
                    {
                        initializer.InitializeProperty(path.AddName(propertyInfo.Name), obj, propertyInfo);
                    }
                    else
                    {
                        object val = Create(propertyType, path.AddName(propertyInfo.Name));
                        propertyInfo.SetValue(obj, val);
                    }
                }
                return obj;
            }
            throw new InvalidDataException();
        }
    }
}
