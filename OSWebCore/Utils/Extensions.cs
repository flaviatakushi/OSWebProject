using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

namespace OSWebCore
{
    public static class Extensions
    {
        public static void AddRange<T>(this IList<T> list, IList<T> items)
        {
            items.ForEach(x => list.Add(x));
        }

        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            if (action == null)
                throw new ArgumentNullException("action");

            foreach (var item in collection)
                action(item);
        }

        public static bool ToBool(this object value)
        {
            return value.ToBool(CultureInfo.InvariantCulture);
        }

        public static bool ToBool(this object value, IFormatProvider provider)
        {
            return Convert.ToBoolean(value, provider);
        }

        public static long ToLong(this object value)
        {
            return value.ToLong(CultureInfo.InvariantCulture);
        }

        public static long ToLong(this object value, IFormatProvider provider)
        {
            return Convert.ToInt64(value, provider);
        }

        public static long? ToNullableLong(this object value)
        {
            return value.ToNullableLong(CultureInfo.InvariantCulture);
        }

        public static long? ToNullableLong(this object value, IFormatProvider provider)
        {
            if (value == null)
                return null;

            return Convert.ToInt64(value, provider) as long?;
        }

        public static byte ToByte(this object value)
        {
            return value.ToByte(CultureInfo.InvariantCulture);
        }

        public static byte ToByte(this object value, IFormatProvider provider)
        {
            return Convert.ToByte(value, provider);
        }

        public static string ToString(this object value, IFormatProvider provider)
        {
            return Convert.ToString(value, provider);
        }

        public static DateTime ToDateTime(this object value)
        {
            return value.ToDateTime(CultureInfo.InvariantCulture);
        }

        public static DateTime ToDateTime(this object value, IFormatProvider provider)
        {
            return Convert.ToDateTime(value, provider);
        }

        public static DateTime? ToNullableDateTime(this object value)
        {
            return value.ToNullableDateTime(CultureInfo.InvariantCulture);
        }

        public static DateTime? ToNullableDateTime(this object value, IFormatProvider provider)
        {
            if (value == null)
                return null;

            return Convert.ToDateTime(value, provider) as DateTime?;
        }

        public static IEnumerable<T> GetEnumerator<T>(this IDataReader reader, Func<IDataRecord, T> generator)
        {
            if (generator == null)
                throw new ArgumentNullException("generator");

            while (reader.Read())
                yield return generator(reader);
        }
    }
}