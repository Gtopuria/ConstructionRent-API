using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionEquipment.Domain
{
    public interface IInventoryService
    {
        List<Equipment> GetAll();

        Equipment FindById(int id);

    }
}
