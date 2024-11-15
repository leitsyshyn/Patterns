//Реалізувати адаптер для зовнішньої платіжної служби, щоб можна було використовувати
//її в системі, яка початково була розроблена для внутрішнього платіжного процесора.

using System;
using System.Text;

namespace Adapter
{
    public interface IPaymentProcessor
    {
        void ProcessPayment(decimal amount);
        bool VerifyTransaction(string transactionId);
    }

    public class BasicPaymentProcessor : IPaymentProcessor
    {
        public void ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Обробка внутрішнього платежу на суму ${amount}.");
        }

        public bool VerifyTransaction(string transactionId)
        {
            Console.WriteLine($"Перевірка внутрішнього ідентифікатора транзакції: {transactionId}");
            return true;
        }
    }

    public class ExternalPaymentService
    {
        public void MakePayment(decimal amount)
        {
            Console.WriteLine($"Обробка зовнішнього платежу на суму ${amount}.");
        }

        public bool CheckTransactionStatus(string transactionCode)
        {
            Console.WriteLine($"Перевірка статусу зовнішнього коду транзакції: {transactionCode}");
            return true;
        }
    }

    public class PaymentAdapter : IPaymentProcessor
    {
        private readonly ExternalPaymentService _externalService;

        public PaymentAdapter(ExternalPaymentService externalService)
        {
            _externalService = externalService;
        }

        public void ProcessPayment(decimal amount)
        {
            _externalService.MakePayment(amount);
        }

        public bool VerifyTransaction(string transactionId)
        {
            return _externalService.CheckTransactionStatus(transactionId);
        }
    }

    //ClientCode
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            IPaymentProcessor inHouseProcessor = new BasicPaymentProcessor();
            inHouseProcessor.ProcessPayment(100.00m);
            inHouseProcessor.VerifyTransaction("ABC123");

            Console.WriteLine("\nВикористання зовнішньої платіжної служби з адаптером:");

            ExternalPaymentService externalService = new ExternalPaymentService();
            IPaymentProcessor adaptedExternalService = new PaymentAdapter(externalService);
            adaptedExternalService.ProcessPayment(200.00m);
            adaptedExternalService.VerifyTransaction("XYZ789");

            Console.ReadKey();
        }
    }
}
