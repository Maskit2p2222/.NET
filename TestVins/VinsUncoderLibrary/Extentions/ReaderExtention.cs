using System;
using System.Data;

namespace VinsUncoderLibrary.Extentions
{
    public static class ReaderExtention
    {
        public static T GetValue<T>(this IDataReader reader, string column)
        {
            try
            {
                var value = reader[column];
                if (!ReferenceEquals(value, DBNull.Value) && value != null)
                {
                    Type type = typeof(T);
                    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {

                    }
                    return default(T);
                }
                else
                {
                    return default(T);
                }

            }
            catch (Exception e)
            {
                return default(T);
            }
        }

    }
}
