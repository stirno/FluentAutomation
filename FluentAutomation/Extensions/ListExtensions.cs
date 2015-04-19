using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.Extensions
{
    public static class ListExtensions
    {
        public static void MergeWith<T>(this List<T> list, T primary, T secondary)
        {
            secondary.MergeWith(primary);
            list.Add(secondary);
        }

        public static void MergeWith<T>(this T primary, T secondary)
        {
            foreach (var pi in typeof(T).GetProperties())
            {
                var priValue = pi.GetGetMethod().Invoke(primary, null);
                var secValue = pi.GetGetMethod().Invoke(secondary, null);
                if (secValue != null && (priValue == null || (pi.PropertyType.IsValueType && priValue.Equals(Activator.CreateInstance(pi.PropertyType)))))
                {
                    pi.GetSetMethod().Invoke(primary, new object[] { secValue });
                }
            }
        }
    }
}