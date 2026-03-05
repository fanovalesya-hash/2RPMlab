using System;
using System.Collections.Generic;
using System.Text;

namespace SOLID_Fundamentals
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class OrderStorage
    {
        private readonly List<Order> _orders = new();   
        public void Add(Order order)
        {
            _orders.Add(order);
            Console.WriteLine($"Order {order.Id} added");
        }
        public Order? GetById(int orderId)
        {
            return _orders.FirstOrDefault(o => o.Id == orderId);
        }
        public List<Order> GetAll()
        {
            return new List<Order>(_orders);
        }
    }

    public class OrderProcessing
    {
        public void Process(Order order)
        {
            if (order.TotalAmount <= 0)
                throw new Exception("Invalid order amount");

            ProcessPayment(order.PaymentMethod, order.TotalAmount);
            UpdateInventory(order.Items);
            SendEmail(order.CustomerEmail, $"Order {order.Id} processed");
            LogToDatabase($"Order {order.Id} processed at {DateTime.Now}");
            GenerateReceipt(order);
        }
        private void ProcessPayment(string paymentMethod, decimal amount) { }
        private void UpdateInventory(List<string> items) { }
        private void SendEmail(string to, string message) { }
        private void LogToDatabase(string message) { }
        private void GenerateReceipt(Order order) { }

    }

    public class REportServ
    {
        public void GenerateMonthlyReport(List<Order> orders)
        {
            decimal totalRevenue = orders.Sum(o => o.TotalAmount);
            int totalOrders = orders.Count;
            Console.WriteLine($"Monthly Report: {totalOrders} orders, Revenue: {totalRevenue:C}");
        }
        public void ExportToExcel(List<Order> orders, string filePath)
        {
            Console.WriteLine($"Exporting orders to {filePath}");
        }

    }

    public class OrderProcessor
    {
        private readonly OrderStorage _storage;
        private readonly OrderProcessing _ordproc;
        private readonly REportServ _reportserv;

        public OrderProcessor()
        {
            _storage = new OrderStorage();
            _ordproc = new OrderProcessing();
            _reportserv = new REportServ();
        }

        public void AddOrder(Order order)
        {
            _storage.Add(order);
        }
        public void ProcessOrder(int orderId)
        {
            var order = _storage.GetById(orderId);
            if (order != null)
            {
                Console.WriteLine($"Processing order {orderId}");
                _ordproc.Process(order);
            }
        }
        public void GenerateMonthlyReport()
        {
            var allOrders = _storage.GetAll();
            _reportserv.GenerateMonthlyReport(allOrders);
        }
        public void ExportToExcel(string filePath)
        {
            var allOrders = _storage.GetAll();
            _reportserv.ExportToExcel(allOrders, filePath);
        }
    }
}
