﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop.Entrepreneur.Application.Messages.Commands.Supplier
{
    public class UpdateSupplierCommand : IRequest
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
    }
}
