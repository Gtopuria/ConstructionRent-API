using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionEquipment.Domain
{
    public class InventoryService : IInventoryService
    {

        public static List<Equipment> equipment = new List<Equipment>() {
                new Equipment() { Id = 1, Name = "Caterpillar bulldozer", Type = EquipmentType.Heavy },
                new Equipment() { Id = 2, Name = "KamAZ truck", Type = EquipmentType.Regular },
                new Equipment() { Id = 3, Name = "Komatsu crane", Type = EquipmentType.Heavy },
                new Equipment() { Id = 4, Name = "Volvo steamroller", Type = EquipmentType.Regular },
                new Equipment() { Id = 5, Name = "Bosch jackhammerr", Type = EquipmentType.Specialized },
            };
        public Equipment FindById(int id)
        {

            var order = equipment.Find(x => x.Id == id);
            if (order == null)
            {
                throw new ArgumentException($"No order found with {id}");
            }
            return order;
        }

        public List<Equipment> GetAll()
        {
            return equipment;
        }
    }
}
