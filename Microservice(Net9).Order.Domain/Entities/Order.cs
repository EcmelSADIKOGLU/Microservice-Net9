using MassTransit;
using System.Text;

namespace Microservice_Net9_.Order.Domain.Entities
{
    public class Order : BaseEntity<Guid>
    {
        public string OrderCode { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
        public Guid BuyerId { get; set; }
        public OrderStatus Status { get; set; }
        public int AddressId { get; set; }
        public float? DiscountRate { get; set; }
        public decimal TotalPrice { get; set; }

        public Guid? PaymentId { get; set; }

        public Address Address { get; set; } = default!;


        public List<OrderItem> OrderItems { get; set; } = new();


        public static string GenerateOrderCode()
        {
            var random = new Random();
            var orderCode = new StringBuilder(10);
            for (int i = 0; i < 10; i++)
            {
                orderCode.Append(random.Next(0, 10));
            }

            return orderCode.ToString();
        }

        public static Order CreateUnpaidOrder(Guid buyerId, float? discountPercent, int addressId)
        {
            var order = new Order
            {
                Id = NewId.NextSequentialGuid(),
                OrderCode = GenerateOrderCode(),
                BuyerId = buyerId,
                CreatedDate = DateTime.UtcNow,
                Status = OrderStatus.WaitingForPayment,
                AddressId = addressId,
                DiscountRate = discountPercent,

            };

            return order;
        }

        public void AddOrderItem(Guid productId, string productName, decimal unitPrice)
        {
            OrderItem item = new OrderItem();
            item.SetItem(productId, productName, unitPrice);
            OrderItems.Add(item);
            CalculateTotalPrice();
        }


        public void MarkAsPaid(Guid paymentId)
        {
            this.Status = OrderStatus.Paid;
            this.PaymentId = paymentId;
        }

        public void CancelOrder()
        {
            this.Status = OrderStatus.Cancelled;
        }

        public void ApplyDiscount(float discountPercent)
        {
            if (discountPercent < 0 || discountPercent > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(discountPercent), "Discount percent must be between 0 and 100.");
            }
            this.DiscountRate = discountPercent;
            CalculateTotalPrice();
        }

        private void CalculateTotalPrice()
        {
            TotalPrice = OrderItems.Sum(item => item.UnitPrice);
            if (DiscountRate.HasValue && DiscountRate.Value > 0)
            {
                TotalPrice -= (TotalPrice * (decimal)DiscountRate!.Value) / 100;
            }


        }


        
    }
}
