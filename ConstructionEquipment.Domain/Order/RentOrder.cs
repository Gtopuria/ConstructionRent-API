using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionEquipment.Domain.Order
{
    public class RentOrder
    {
        public Guid Id { get; set; }

        public List<RentedEquipment> Items { get; set; }
    }
}
