using ConstructionEquipment.Domain.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionEquipment.Domain.Invoice
{
    public class Invoice
    {
        public string Title { get; set; }
        public List<InvoiceItem> Items { get; set; }
        public int TotalPrice { get; set; }
        public int LoyaltyPoints { get; set; }
    }
}
