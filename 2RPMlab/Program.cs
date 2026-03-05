using SOLID_Fundamentals;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("=== Тест всех классов лабораторной ===\n");

        // 1. Тестируем банковскую часть (Bank.cs)
        Console.WriteLine("Тест банковских счетов:");
        var savings = new SavingAccount();
        savings.Deposit(500);
        Console.WriteLine($"Сберегательный счёт: {savings.Balance}");

        savings.Withdraw(150);  // должно пройти
        Console.WriteLine($"После снятия: {savings.Balance}");

        var fixedDeposit = new FixedDepositAccount(1000, new DateTime(2026, 12, 31));
        fixedDeposit.Deposit(200);
        Console.WriteLine($"Срочный вклад: {fixedDeposit.Balance}");

        // Попытка снять до срока — должна быть ошибка
        try
        {
            fixedDeposit.Withdraw(100);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка снятия: {ex.Message}");
        }

        Console.WriteLine();

        // 2. Тест скидок и доставки (DiscountCalculator.cs)
        Console.WriteLine("Тест скидок и доставки:");
        var calc = new DiscountCalculator();

        decimal discount = calc.CalculateDiscount("VIP", 2000);
        Console.WriteLine($"Скидка для VIP на 2000: {discount}");

        decimal shipping = calc.CalculateShippingCost("International", 5, "Europe");
        Console.WriteLine($"Доставка International в Europe: {shipping}");

        Console.WriteLine();

        // 3. Тест операций с заказами (OrderOperations.cs)
        Console.WriteLine("Тест операций с заказами:");
        var customer = new CustomerPortal();
        var order = new Order { Id = 1 };
        customer.CreateOrder(order);
        customer.UpdateOrder(order);
        customer.DeleteOrder(1);

        var manager = new OrderManager();
        manager.ProcessPayment(order);
        manager.BackupDatabase();

        Console.WriteLine();

        // 4. Тест обработки заказов (OrderProcessor.cs)
        Console.WriteLine("Тест обработки заказов:");
        var processor = new OrderProcessor();

        var newOrder = new Order
        {
            Id = 100,
            TotalAmount = 1500,
            PaymentMethod = "Card",
            Items = new List<string> { "Phone", "Case" },
            CustomerEmail = "user@example.com",
            CustomerPhone = "+79991234567"
        };

        processor.AddOrder(newOrder);
        processor.ProcessOrder(100);
        processor.GenerateMonthlyReport();
        processor.ExportToExcel("orders.xlsx");

        Console.WriteLine();

        // 5. Тест сервисов уведомлений (Services.cs)
        Console.WriteLine("Тест уведомлений:");
        var email = new EmailService();
        var sms = new SmsService();

        var orderSvc = new OrderService(email, sms);
        orderSvc.PlaceOrder(newOrder);

        var notifySvc = new NotificationService(email);
        notifySvc.SendPromotion("user@example.com", "Скидка 30% на следующую покупку!");

        Console.WriteLine("\n=== Тестирование завершено ===\n");
        Console.WriteLine("Нажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}