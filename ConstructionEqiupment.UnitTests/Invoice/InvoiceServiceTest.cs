
using ConstructionEquipment.Domain;
using ConstructionEquipment.Domain.Invoice;
using ConstructionEquipment.Domain.Order;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace ConstructionEqiupment.UnitTests
{
    [TestClass]
    public class InvoiceServiceTests
    {
        private readonly Mock<IInventoryService> _inventoryService = new Mock<IInventoryService>();
        private readonly Mock<IRentOrderService> _rentOrderService = new Mock<IRentOrderService>();


        public InvoiceServiceTests()
        {
            _rentOrderService.Setup(x => x.FindById(It.IsAny<Guid>())).Returns(orderWithOneItem);
            _inventoryService.Setup(x => x.FindById(1)).Returns(regularEquipment);
            _inventoryService.Setup(x => x.FindById(2)).Returns(heavyEquipment);
            _inventoryService.Setup(x => x.FindById(3)).Returns(specializedEquipment);
        }

        private readonly Equipment regularEquipment = new Equipment()
        {
            Id = 1,
            Name = "KamazTruckr",
            Type = EquipmentType.Regular
        };

        private readonly Equipment heavyEquipment = new Equipment()
        {
            Id = 2,
            Name = "Caterpillar bulldozer",
            Type = EquipmentType.Heavy
        };

        private readonly Equipment specializedEquipment = new Equipment()
        {
            Id = 2,
            Name = "Bosch jackhammer",
            Type = EquipmentType.Specialized
        };

        private readonly RentOrder orderWithOneItem = new RentOrder()
        {
            Id = Guid.NewGuid(),
            Items = new List<RentedEquipment>() {
                new RentedEquipment() { durationInDays = 1 , EquipmentId = 1, Id =1 }
            }
        };


        private readonly RentOrder orderWithMultipleItems = new RentOrder()
        {
            Id = Guid.NewGuid(),
            Items = new List<RentedEquipment>() {
                new RentedEquipment() { durationInDays = 1 , EquipmentId = 1, Id =1 },
                new RentedEquipment() { durationInDays = 3 , EquipmentId = 2, Id =2 },
                new RentedEquipment() { durationInDays = 4 , EquipmentId = 3, Id =2 }
            }
        };


        [TestMethod]
        public void GenerateInvoice_should_calculate_loyalty_points()
        {
            _rentOrderService.Setup(x => x.FindById(It.IsAny<Guid>())).Returns(orderWithOneItem);
            var service = new InvoiceService(_inventoryService.Object, _rentOrderService.Object);
            var result = service.Generate(Guid.NewGuid());
            result.LoyaltyPoints.Should().Be(1);
        }

        [TestMethod]
        public void GenerateInvoice_should_calculate_total_price()
        {
            _rentOrderService.Setup(x => x.FindById(It.IsAny<Guid>())).Returns(orderWithOneItem);
            var service = new InvoiceService(_inventoryService.Object, _rentOrderService.Object);
            var result = service.Generate(Guid.NewGuid());
            result.TotalPrice.Should().Be(160);
        }

        [TestMethod]
        public void GenerateInvoice_with_multiple_items_should_calculate_loyalty_points()
        {
            _rentOrderService.Setup(x => x.FindById(It.IsAny<Guid>())).Returns(orderWithMultipleItems);
            var service = new InvoiceService(_inventoryService.Object, _rentOrderService.Object);
            var result = service.Generate(Guid.NewGuid());
            result.LoyaltyPoints.Should().Be(4);
        }

        [TestMethod]
        public void GenerateInvoice_with_multiple_items_should_calculate_price()
        {
            _rentOrderService.Setup(x => x.FindById(It.IsAny<Guid>())).Returns(orderWithMultipleItems);
            var service = new InvoiceService(_inventoryService.Object, _rentOrderService.Object);
            var result = service.Generate(Guid.NewGuid());
            result.TotalPrice.Should().Be(660);
        }

        [TestMethod]
        public void GenerateInvoice_with_multiple_items_should_add_items_to_invoice()
        {
            _rentOrderService.Setup(x => x.FindById(It.IsAny<Guid>())).Returns(orderWithMultipleItems);
            var service = new InvoiceService(_inventoryService.Object, _rentOrderService.Object);
            var result = service.Generate(Guid.NewGuid());
            result.Items.Count.Should().Be(3);
        }


        [TestMethod]
        [DataRow(1, 2, 160)]
        [DataRow(3, 2, 260)]
        [DataRow(0, 2, 0)]
        public void CalculateRegularEquipmentPrice_renting_for_one_day(int durationInDays, int thresholdForPremiumPrice, int expected)
        {
            var service = new InvoiceService(_inventoryService.Object, _rentOrderService.Object);
            var oneTimeFee = true;
            var equipment = new Equipment() { Name = "KamAZ truck", Type = EquipmentType.Regular };
            var result = service.CalculatePrice(equipment, durationInDays, thresholdForPremiumPrice, oneTimeFee);
            result.Amount.Should().Be(expected);

        }


        [TestMethod]
        [DataRow(1, 3, 60)]
        [DataRow(4, 3, 220)]
        [DataRow(0, 3, 0)]
        public void CalculateSpecialEquipmentPrice_renting_for_zero_days(int durationInDays, int thresholdForPremiumPrice, int expected)
        {
            var service = new InvoiceService(_inventoryService.Object, _rentOrderService.Object);
            var oneTimeFee = false;
            var equipment = new Equipment() { Name = "Bosch jackhammer", Type = EquipmentType.Specialized };
            var result = service.CalculatePrice(equipment, durationInDays, thresholdForPremiumPrice, oneTimeFee);
            result.Amount.Should().Be(expected);

        }


        [TestMethod]
        [DataRow(1, 160)]
        [DataRow(4, 340)]
        [DataRow(0, 0)]
        public void CalculateHeavyEquipmentPrice_renting_for_zero_days(int durationInDays, int expected)
        {
            var service = new InvoiceService(_inventoryService.Object, _rentOrderService.Object);
            var equipment = new Equipment() { Name = "Caterpillar bulldozer", Type = EquipmentType.Heavy };
            var result = service.CalculateHeavyPrice(equipment, durationInDays);
            result.Amount.Should().Be(expected);

        }
    }
}
