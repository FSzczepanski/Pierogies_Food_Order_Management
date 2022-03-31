namespace CleanArchitecture.Application.IntegrationTests.Orders
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.Orders.Commands;
    using Domain.Entities;
    using Domain.Enums;
    using Domain.ValueObjects;
    using FluentAssertions;
    using NUnit.Framework;
    using static Testing;

    public class UpdateOrderCommandTest
    {
        [Test]
        public async Task UpdateOrderCommand_ValidFields_Success()
        {
            var command = new CreateOrderCommand()
            {
                PurchaserName = "TEST",
                Email = "TEST@TEST.PL",
                Phone = "123123123",
                Positions = new List<OrderPosition>()
                {
                    new OrderPosition(){Name = "test",Amount = 1,Price = 10,Vat = 0,PortionSize = "test"},
                    new OrderPosition(){Name = "test",Amount = 4,Price = 10,Vat = 0,PortionSize = "test"},
                },
                Date = DateTime.Now,
                LocationName = "test",
                LocationDescription = "test",
                Street = "test",
                ZipCode = "test",
                CityName = "test",
                CountryName = "test",
                IsDefault = false,
                NeedInvoice = false,
                PaymentMethod = PaymentMethodEnum.OnPlace,
                FormId = Guid.Empty,
                Description = "test",
                DeliveryPrice = 20
            };

            var id = await SendAsync(command);

            var updateCommand = new UpdateOrderCommand()
            {
                Id = id,
                PurchaserName = "TEST2",
                Email = "TEST2@TEST.PL",
                Phone = "1231231323",
                Positions = new List<OrderPosition>()
                {
                    new OrderPosition(){Name = "test2",Amount = 2,Price = 11,Vat = 0,PortionSize = "test2"},
                    new OrderPosition(){Name = "test2",Amount = 4,Price = 11,Vat = 0,PortionSize = "test2"},
                },
                Date = DateTime.Now,
                LocationName = "test2",
                LocationDescription = "test2",
                Street = "test2",
                ZipCode = "test2",
                CityName = "test2",
                CountryName = "test2",
                IsDefault = true,
                FormId = Guid.Empty,
                Description = "test2",
                DeliveryPrice = 21
            };

            await SendAsync(updateCommand);
            
            var order = await FindAsync<Order>(id);

            order.Should().NotBeNull();
            order.Purchaser.Name.Should().Be(updateCommand.PurchaserName);
            order.Purchaser.Email.Should().Be(updateCommand.Email);
            order.Purchaser.Phone.Should().Be(updateCommand.Phone);
            order.Positions.Should().NotBeNull();
            order.Location.Name.Should().Be(updateCommand.LocationName);
            order.Location.Description.Should().Be(updateCommand.LocationDescription);
            order.Description.Should().Be(updateCommand.Description);
            order.DeliveryPrice.Should().Be(updateCommand.DeliveryPrice);
            order.FullPrice.Should().Be(87);
        }
    }
}