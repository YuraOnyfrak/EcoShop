using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EcoShop.Common.Common.Extensions
{
     public static class StringExtension
    {
        public static string Underscore(this string value)
           => string.Concat(value.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString()));

        public static string GetNameOptions(this string value)
            => value.Substring(0, value.IndexOf('O')).ToLowerInvariant();

    }
}
