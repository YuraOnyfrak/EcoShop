using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop.Entrepreneur.Domain.Entity
{
    public class Manager 
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public int IdSupplier { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
