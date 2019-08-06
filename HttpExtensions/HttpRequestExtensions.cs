using System;
using System.ComponentModel;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace HttpExtensions
{
    public static class HttpRequestExtensions
    {
        public static T GetQueryString<T>(this HttpRequest request, string key, T defaultValue)
        {
            if (request.Query is null)
                return defaultValue;

            var match = request.Query.FirstOrDefault(kv => String.Compare(kv.Key, key, StringComparison.OrdinalIgnoreCase) == 0);
            if (string.IsNullOrWhiteSpace(match.Value))
                return defaultValue;

            try
            {
                var typeConverter = TypeDescriptor.GetConverter(typeof(T));
                return (T)typeConverter.ConvertFromString(match.Value);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
    }
}
