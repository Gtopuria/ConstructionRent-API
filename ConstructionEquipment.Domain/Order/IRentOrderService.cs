using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionEquipment.Domain.Order
{
    public interface IRentOrderService
    {
        void PlaceOrder(RentOrder order);

        List<RentOrder> GetAll();

        void Remove(Guid orderGuid);

        RentOrder FindById(Guid orderGuid);
    }
}
