using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionEquipment.Domain.Invoice
{
    public class InvoiceItem
    {
        public int Id { get; set; }

        public string ItemName { get; set; }

        public int durationInDays { get; set; }

        public int Amount { get; set; }
    }
}
