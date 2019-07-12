using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests.Handlers
{
    [TestClass]
    public class SubscriptionHandlerTests
    {
        // Red, Green, Refactor

        [TestMethod]
        public void SouldReturnErrorWhenDocumentExists()
        {
            var handler = new SubscriptionHandler(new FakeStudntRepository(), new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand();
            command.FirstName = "José";
            command.LastName = "LeLé";
            command.Document = "12345678999";
            command.Email = "hello@hello.com";
            command.BarCode = "12345679";
            command.BoletoNumber = "132456789";
            command.PaymentNumber = "132456";
            command.PaidDate = DateTime.Now;
            command.ExpireDate =  DateTime.Now.AddMonths(1);
            command.Total = 60;
            command.TotalPaid = 60;
            command.Payer = "Fazenda";
            command.PayerDocument = "12345687999";
            command.PayerDocumentType = Domain.Enums.EDocumentType.CPF;
            command.PayerEmail = "fazenda@fazenda.com";
            command.Street = "rua vai e vem";
            command.Number = "123";
            command.Neighborhood = "";
            command.City = "ctba";
            command.State = "PR";
            command.Country = "BR";
            command.ZipCode = "13245678";

            handler.Handle(command);
            Assert.AreEqual(false, handler.Valid);
        }
    }
}