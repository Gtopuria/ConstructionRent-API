using ConstructionEquipment.Domain.Order;
using ConstructionEquipment.Domain.Price;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionEquipment.Domain.Invoice
{
    public class InvoiceService : IInvoiceService
    {
        public IInventoryService _inventoryService;
        private IRentOrderService _rentOrderService;
        public InvoiceService(IInventoryService inventoryService,
                              IRentOrderService rentOrderService)
        {
            _inventoryService = inventoryService;
            _rentOrderService = rentOrderService;
        }


        public InvoiceItem CalculatePrice(Equipment equipment, int durationInDays, int thresholdForPremiumPrice, bool oneTimeFee)
        {
            var invoice = new InvoiceItem()
            {
                ItemName = equipment.Name,
                durationInDays = durationInDays
            };
            var calculatedPrice = durationInDays > 0 ? oneTimeFee ? (int)PriceEnum.OneTime : 0 : 0;
            if (durationInDays <= thresholdForPremiumPrice)
            {
                calculatedPrice += durationInDays * (int)PriceEnum.Premium;
                invoice.Amount = calculatedPrice;
                return invoice;
            }

            calculatedPrice += thresholdForPremiumPrice * (int)PriceEnum.Premium;
            calculatedPrice += (durationInDays - thresholdForPremiumPrice) * (int)PriceEnum.Regular;
            invoice.Amount = calculatedPrice;
            return invoice;
        }
        public InvoiceItem CalculateHeavyPrice(Equipment equipment, int durationInDays)
        {
            var invoice = new InvoiceItem()
            {
                ItemName = equipment.Name,
                durationInDays = durationInDays,
                Amount = durationInDays > 0 ? ((int)PriceEnum.OneTime + (durationInDays * (int)PriceEnum.Premium)) : 0
            };
            return invoice;
        }

        public Invoice Generate(Guid orderid)
        {
            var order = _rentOrderService.FindById(orderid);
            var invoice = new Invoice() { Title = $"Invoice {DateTime.Now}", Items = new List<InvoiceItem>() };
            foreach (var item in order.Items)
            {
                var equipment = _inventoryService.FindById(item.EquipmentId);
                switch (equipment.Type)
                {
                    case EquipmentType.Regular:
                        invoice.Items.Add(CalculatePrice(equipment, item.durationInDays, 2, true));
                        invoice.LoyaltyPoints += 1;
                        break;
                    case EquipmentType.Heavy:
                        invoice.Items.Add(CalculateHeavyPrice(equipment, item.durationInDays));
                        invoice.LoyaltyPoints += 2;
                        break;
                    case EquipmentType.Specialized:
                        invoice.Items.Add(CalculatePrice(equipment, item.durationInDays, 3, false));
                        invoice.LoyaltyPoints += 1;
                        break;
                    default:
                        break;
                }
            }
            invoice.TotalPrice = invoice.Items.Aggregate(0, (acc, x) => acc + x.Amount);
            return invoice;
        }

    }
}
