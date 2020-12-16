using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoShop.ApiGateway.Models.CryptoWallet
{
    public class CreateTransactionModel
    {
        public Guid id { get; set; }
        public Guid recipient_id { get; set; }
        public decimal amount { get; set; }
        public Guid sender_id { get; set; }
        public int order_id  { get; set; }
    }
}
