using System.IO;
using CleanArchitecture.Application.Mails.models;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Orders.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Common.Interfaces;
    using Domain.Entities;
    using Domain.Enums;
    using Domain.ValueObjects;
    using MediatR;

    public class CreateOrderCommand : IRequest<Guid>
    {
        //purchaser
        public string PurchaserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<OrderPosition> Positions { get; set; }
        public DateTime Date { get; set; }
        
        //location
        public string LocationName { get; set; }
        public string LocationDescription { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public bool IsDefault { get; set; }
        
        //Payment Details
        public bool? NeedInvoice { get; set; }
        public PaymentMethodEnum? PaymentMethod { get; set; }
        
        public Guid FormId { get; set; }
        public decimal DeliveryPrice { get; set; }
        public string Description { get; set; }

        public class Handler : IRequestHandler<CreateOrderCommand, Guid>
        {
            private readonly IApplicationDbContext _applicationDbContext;

            private readonly IEmailService _emailService;

            public Handler(IApplicationDbContext applicationDbContext, IEmailService emailService)
            {
                _applicationDbContext = applicationDbContext;
                _emailService = emailService;
            }

            public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
            {

                var fullPrice = CalculateFullPrice(request.Positions, request.DeliveryPrice, request.IsDefault);
                var entity = new Order()
                {
                    Purchaser = new Purchaser(){Name = request.PurchaserName, Email = request.Email, Phone = request.Phone},
                    Positions = request.Positions,
                    Date = request.Date,
                    Location = new Location()
                    {
                        Name = request.LocationName, Description = request.LocationDescription, Street = request.Street, CityName = request.CityName,
                        CountryName = request.CountryName, IsDefault = request.IsDefault, ZipCode = request.ZipCode
                    },
                    Payment = new PaymentDetails(){PaymentMethod = request.PaymentMethod,NeedInvoice = request.NeedInvoice, IsPaid = false},
                    FullPrice = fullPrice,
                    FormId = request.FormId,
                    DeliveryPrice = request.DeliveryPrice,
                    Description = request.Description
                };
                
                await _applicationDbContext.Orders.AddAsync(entity, cancellationToken);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                var location = request.Street + ", " + request.ZipCode + " " + request.CityName;
                await SendConfirmationMails(request.Email, request.PurchaserName, location, request.Date, request.Positions, fullPrice, request.DeliveryPrice);
                
                
                return entity.Id;
            }

            private async Task SendConfirmationMails(string email, string name, string location, 
                DateTime date, List<OrderPosition> positions, decimal fullPrice, decimal deliveryPrice)
            {
                var ordersText = "";
                foreach (var orderPosition in positions)
                {
                    ordersText += "<tr>" +
                                    "<td style='padding: 15px'>"+orderPosition.Name+"</td>" +
                                    "<td style='padding: 15px'>"+orderPosition.Price+" zł</td>" +
                                    "<td style='padding: 15px'>"+orderPosition.PortionSize+"</td>" +
                                    "<td style='padding: 15px'>"+orderPosition.Amount+"</td>" +
                                    "</tr> ";
                }
                
                
                string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\EmailTemplate.html";
                StreamReader str = new StreamReader(FilePath);
                string MailBody = str.ReadToEnd();
                str.Close();
                MailBody = MailBody
                    .Replace("{Name}", name)
                    .Replace("{Location}", location)
                    .Replace("{Date}", date.ToLocalTime().ToString("dd/MM/yyyy hh:mm"))
                    .Replace("<tr>{row}</tr>", ordersText)
                    .Replace("{TotalPrice}", fullPrice + " zł")
                    .Replace("{Delivery}", deliveryPrice != 0 ? "Dostawa: "+deliveryPrice + " zł" : "");

                
                var mailRequest = new MailRequest
                {
                    ToEmail = email,
                    Subject = "Potwierdzenie zamówienia, Folwark Tumiany",
                    Body = MailBody
                };
                await _emailService.SendEmailAsync(mailRequest);
            }

            private static decimal CalculateFullPrice(List<OrderPosition> orderPositions, decimal deliveryPrice, bool isDefaultLocation)
            {
                decimal fullPrice = 0;
                foreach (var orderPosition in orderPositions)
                {
                    fullPrice += orderPosition.Price * orderPosition.Amount;
                }

                if (!isDefaultLocation)
                {
                    fullPrice += deliveryPrice;
                }

                return fullPrice;
            }
        }
    }
}