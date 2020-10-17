using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TestProject.Common.Exceptions
{
    public class DShopException : Exception
    {
        public DShopException()
        {
        }

        public DShopException(string message) : base(message)
        {
        }

        public DShopException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DShopException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
