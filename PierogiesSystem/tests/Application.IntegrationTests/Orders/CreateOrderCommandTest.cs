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

    public class CreateOrderCommandTest
    {
        [Test]
        public async Task CreateOrderCommand_ValidFields_Success()
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
            var order = await FindAsync<Order>(id);

            order.Should().NotBeNull();
            order.Purchaser.Name.Should().Be(command.PurchaserName);
            order.Purchaser.Email.Should().Be(command.Email);
            order.Purchaser.Phone.Should().Be(command.Phone);
            order.Positions.Should().NotBeNull();
            order.Location.Name.Should().Be(command.LocationName);
            order.Location.Description.Should().Be(command.LocationDescription);
            order.Payment.NeedInvoice.Should().Be(command.NeedInvoice);
            order.Payment.PaymentMethod.Should().Be(command.PaymentMethod);
            order.Description.Should().Be(command.Description);
            order.DeliveryPrice.Should().Be(command.DeliveryPrice);
            order.FullPrice.Should().Be(70);
        }
    }
}