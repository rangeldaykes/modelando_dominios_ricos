using System;
using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler :
        Notifiable,
        IHandler<CreateBoletoSubscriptionCommand>,
        IHandler<CreatePayPalSubscriptionCommand>
    {
        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository repository,  IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            // Fail Fast Validations
            command.Validate();
            
            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar sua assinatura");
            }

            // Verificar se Documento já está cadastrado
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já esta em uso");

            // Verificar se Email já está cadastrado
            if (_repository.EmailExists(command.Email))
                AddNotification("Email", "Este E-mail já está em uso");

            // Gerar VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, Domain.Enums.EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(
                command.Street, 
                command.Number, 
                command.Neighborhood, 
                command.City, 
                command.State, 
                command.Country, 
                command.ZipCode);

            // Gerar Entidades,0
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(
                command.BarCode, 
                command.BoletoNumber, 
                command.PaidDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid,
                command.Payer,
                new Document(command.PayerDocument, command.PayerDocumentType),
                address,
                email);

            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Agrupar as Validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            // Checar as Notificações
            if (Invalid)
                return new CommandResult(false, "Não foi possível realizar sua assinatura");

            // Salvar Informações
            _repository.CreateSubscription(student);

            // Enviar E-mail de boas vindas
            _emailService.Send(
                student.Name.ToString(), 
                student.Email.Address, 
                "Bem vindo!", 
                "Sua assinatura foi criada!");

            // Retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso!");
        }

        public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
        {
            // Fail Fast Validations
            /* 
            command.Validate();
            
            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar sua assinatura");
            } 
            */

            // Verificar se Documento já está cadastrado
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já esta em uso");

            // Verificar se Email já está cadastrado
            if (_repository.EmailExists(command.Email))
                AddNotification("Email", "Este E-mail já está em uso");

            // Gerar VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, Domain.Enums.EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(
                command.Street, 
                command.Number, 
                command.Neighborhood, 
                command.City, 
                command.State, 
                command.Country, 
                command.ZipCode);

            // Gerar Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new PayPalPayment(
                command.TransactionCode,
                command.PaidDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid,
                command.Payer,
                new Document(command.PayerDocument, command.PayerDocumentType),
                address,
                email);

            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Agrupar as Validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            // Salvar Informações
            _repository.CreateSubscription(student);

            // Enviar E-mail de boas vindas
            _emailService.Send(
                student.Name.ToString(), 
                student.Email.Address, 
                "Bem vindo!", 
                "Sua assinatura foi criada!");

            // Retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso!");
        }
    }
}