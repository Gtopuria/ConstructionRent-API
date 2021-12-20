using ConstructionEquipment.Domain.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionEquipment.Domain.Invoice
{
    public interface IInvoiceService
    {
        Invoice Generate(Guid orderId);
    }
}
