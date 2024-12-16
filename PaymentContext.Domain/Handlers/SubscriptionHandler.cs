using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : 
        Notifiable, 
        IHandler<CreateBoletoSubscriptionCommand>
        //IHandler<CreatePayPalSubscriptionCommand>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository studentRepository, IEmailService emailService)
        {
            _studentRepository = studentRepository;
            _emailService = emailService;
        }

        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            // Fail fast validations
            command.Validate();
            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar o cadastro.");
            }

            //Verificar se documento está cadastrado.
            if (_studentRepository.DocumentExists(command.Document))
            {
                AddNotifications(command);
                return new CommandResult(false, "CPF já está em uso.");
            }

            //Verificar email cadastrado.
            if (_studentRepository.EmailExists(command.Email))
            {
                AddNotifications(command);
                return new CommandResult(false, "Email já está em uso.");
            }

            //Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.Street, command.Country, command.ZipCode);

            //Gerar as entidades.
            var student = new Student(name, email, document);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(
                command.BarCode, 
                command.BoletoNumber, 
                command.PaidDate, 
                command.ExpireDate, 
                command.Total, 
                command.TotalPaid, 
                address, 
                new Document(command.PayerDocument, command.PayerDocumentType), 
                command.Payer, 
                email
            );

            //Relacionamentos
            subscription.AddPayment( payment );
            student.AddSubscription( subscription );

            //Agrupar as validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            // Salvar informações
            _studentRepository.CreateSubscription(student);

            // Enviar email de boas vindas
            _emailService.SendEmail(student.Name.ToString(), student.Email.Address, "Bem vindo", "Assinatura criada com sucesso.");

            //Retornar as informações.

            return new CommandResult(true, "Assinatura realizada com sucesso");
        }
    }
}
