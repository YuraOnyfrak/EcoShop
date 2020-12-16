using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop.Marketplace.Application.Options
{
    public class ElasticSearchOptions
    {
        public int Port { get; set; }
        public string Address { get; set; }
        public int CountNode { get; set; }
        public string MainNodeName { get; set; }
        public string DefaultIndex { get; set; }
    }
}
