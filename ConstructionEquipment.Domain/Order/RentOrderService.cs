using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionEquipment.Domain.Order
{
    public class RentOrderService : IRentOrderService
    {
        public RentOrder FindById(Guid orderGuid)
        {
            if (orderGuid == Guid.Empty)
            {
                throw new ArgumentNullException("No Guid provided");
            }

            var order = RentOrders.orders.Find(x => x.Id == orderGuid);

            if (order == null)
            {
                throw new ArgumentException($"No order found with {orderGuid}");
            }
            return order;
        }

        public List<RentOrder> GetAll()
        {
            return RentOrders.orders;
        }

        public void PlaceOrder(RentOrder order)
        {
            order.Id = Guid.NewGuid();
            RentOrders.orders.Add(order);
        }

        public void Remove(Guid orderGuid)
        {
            RentOrders.orders.RemoveAll(x => x.Id == orderGuid);
        }
    }
}
