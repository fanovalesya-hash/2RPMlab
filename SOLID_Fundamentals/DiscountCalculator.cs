using static SOLID_Fundamentals.DiscountCalculator;

namespace SOLID_Fundamentals
{
    public interface IDiscount
    {
        decimal CalculateDiscount(decimal orderAmount);
    }

    public class RegularDiscount : IDiscount
    {
        public decimal CalculateDiscount(decimal orderAmount)
        {
            return orderAmount * 0.05m;
        }
    }
    public class PremiumDiscount : IDiscount
    {
        public decimal CalculateDiscount(decimal orderAmount)
        {
            return orderAmount * 0.10m;
        }
    }
    public class VIPDiscount : IDiscount
    {
        public decimal CalculateDiscount(decimal orderAmount)
        {
            return orderAmount * 0.15m;
        }
    }
    public class StudentDiscount : IDiscount
    {
        public decimal CalculateDiscount(decimal orderAmount)
        {
            return orderAmount * 0.08m;
        }
    }
    public class SeniorDiscount : IDiscount
    {
        public decimal CalculateDiscount(decimal orderAmount)
        {
            return orderAmount * 0.07m;
        }
    }
    public class NoDiscount: IDiscount
    {
        public decimal CalculateDiscount(decimal orderAmount)
        {
            return orderAmount * 0m;
        }
    }

    public interface IShipping
    {
        public decimal CalculateShippingCost(decimal weight, string destination);
    }

    public class StandardShipping : IShipping
    {
        public decimal CalculateShippingCost(decimal weight, string destination)
        {
            return 5.00m + (weight * 0.5m);
        }
    }
    public class ExpressShipping : IShipping
    {
        public decimal CalculateShippingCost(decimal weight, string destination)
        {
            return 15.00m + (weight * 1.0m);
        }
    }
    public class OvernightShipping : IShipping
    {
        public decimal CalculateShippingCost(decimal weight, string destination)
        {
            return 25.00m + (weight * 2.0m);
        }
    }
    public class InternationalShipping : IShipping
    {
        public decimal CalculateShippingCost(decimal weight, string destination)
        {
            return destination switch
            {
                "USA" => 30.00m,
                "Europe" => 35.00m,
                "Asia" => 40.00m,
                _ => 50.00m
            };
        }
    }

    public class UnknownShipping : IShipping
    {
        public decimal CalculateShippingCost(decimal weight, string destination)
        {
            return 0m;
        }
    }

    public class DiscountCalculator
    {
        

        private readonly Dictionary<string, IDiscount> _discount;
        private readonly Dictionary<string, IShipping> _shipping;
        public DiscountCalculator()
        {
            _discount = new Dictionary<string, IDiscount>(StringComparer.OrdinalIgnoreCase)
            {
                ["Regular"] = new RegularDiscount(),
                ["Premium"] = new PremiumDiscount(),
                ["VIP"] = new VIPDiscount(),
                ["Student"] = new StudentDiscount(),
                ["Senior"] = new SeniorDiscount()
            };
            _shipping = new Dictionary<string, IShipping>(StringComparer.OrdinalIgnoreCase)
            {
                ["Standard"] = new StandardShipping(),
                ["Express"] = new ExpressShipping(),
                ["Overnight"] = new OvernightShipping(),
                ["International"] = new InternationalShipping()
            };
        }
        public decimal CalculateDiscount(string customerType, decimal orderAmount)
        {
            var strategy = _discount.TryGetValue(customerType, out var s)
            ? s
            : new NoDiscount();
            return strategy.CalculateDiscount(orderAmount);
        }
        public decimal CalculateShippingCost(string shippingMethod, decimal weight, string destination)
        {
            var strategy = _shipping.TryGetValue(shippingMethod, out var s)
            ? s
            : new UnknownShipping();
            return strategy.CalculateShippingCost(weight, destination);
        }

    }
}
