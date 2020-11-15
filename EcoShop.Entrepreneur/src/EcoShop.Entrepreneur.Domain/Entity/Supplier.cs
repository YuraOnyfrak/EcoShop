using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop.Entrepreneur.Domain.Entity
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SuppliersTrademark { get; set; }
        public string LegalAddress { get; set; }
        public string ActualAddress { get; set; }
        public string WebsiteUrl { get; set; }
        public string Email { get; set; }

        /// <summary>
        /// Код ЄДРПОУ або ІПН
        /// </summary>
        public string Code { get; set; }        
        public string Description { get; set; }
        
        public virtual List<Manager> Managers { get; set; }
        public virtual List<DocumentProvingQuality> Documents { get; set; }
        public virtual List<Photo> Photos { get; set; }
    }
}
