using ConstructionEquipment.Domain;
using ConstructionEquipment.Domain.Invoice;
using ConstructionEquipment.Domain.Order;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace ConstructionEquipment.UnitTests.Invoice.Services
{
    [TestClass]
    public class InvoiceServiceTests
    {
        private readonly Mock<IInventoryService> _inventoryService = new Mock<IInventoryService>();
        private readonly Mock<IRentOrderService> _rentOrderService = new Mock<IRentOrderService>();

        [TestMethod]
        public void CalculateHeavyEquipmentPrice_returns_correct_value()
        {
            var service = new InvoiceService(_inventoryService.Object, _rentOrderService.Object);

            var equipment = new Equipment() { Name = "KamAZ truck", Type = EquipmentType.Regular };

            var result = service.CalculatePrice(equipment, 1, 2, true);
            result.Amount.Should().Be(100);

        }
    }
}
