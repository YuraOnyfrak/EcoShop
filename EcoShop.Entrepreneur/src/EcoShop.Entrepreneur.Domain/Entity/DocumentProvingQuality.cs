using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop.Entrepreneur.Domain.Entity
{
    public class DocumentProvingQuality
    {
        public int Id { get; set; }
        public int IdSupplier { get; set; }
        public string DocumentName { get; set; }
        public DateTime DateOfIssueDocument { get; set; }
        public DateTime DateExpireValidDocument { get; set; }
        public string NameResponsibleOrganisation { get; set; }
        public string FileUrl { get; set; }
        
        public virtual Supplier Supplier { get; set; }
    }
}
