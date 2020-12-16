using EcoShop.Marketplace.Application.DTO.Entrepreneur;
using RestEase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EcoShop.Marketplace.Application.Services.Entrepreneur
{
    public interface IEntrepreneurService
    {
        [AllowAnyStatusCode]
        [Get("/Supplier/get-supplier/{id}")]
        Task<EntrepreneurDto> GetSupplierByIdAsync([Path] Guid id);
    }
}
