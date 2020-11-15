using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop.Entrepreneur.Domain.Entity
{
    public class Photo
    {
        public int Id { get; set; }
        public int IdSupplier { get; set; }
        public string FileUrl { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
