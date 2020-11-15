using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop.Common.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MessageNamespaceAttribute : Attribute
    {
        public string Namespace { get; }

        public MessageNamespaceAttribute(string @namespace)
        {
            Namespace = @namespace?.ToLowerInvariant();
        }
    }
}
