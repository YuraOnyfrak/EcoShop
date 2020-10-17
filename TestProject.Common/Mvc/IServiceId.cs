using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.Common.Mvc
{
    public interface IServiceId
    {
        string Id { get; }
    }

    public class ServiceId : IServiceId
    {
        private static readonly string UniqueId = $"{Guid.NewGuid():N}";

        public string Id => UniqueId;
    }
}
