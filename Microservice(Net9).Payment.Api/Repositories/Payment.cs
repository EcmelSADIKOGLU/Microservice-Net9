using MassTransit;

namespace Microservice_Net9_.Payment.Api.Repositories
{
    public class Payment
    {
        // public string? Error { get; set; } Transaction ile ilgili tablo oluşturup oraya yerleştirilir.
        public Guid Id { get; set; }
        public Guid BuyerId { get; set; }
        public string OrderCode { get; set; } = default!;
        public DateTime CreatedTime { get; set; }
        public decimal Amount { get; set; }

        public PaymentStatus Status { get; set; }

        public Payment(Guid buyerId, string orderCode, decimal amount)
        {
            Create(buyerId, orderCode, amount);
        }

        public void Create(Guid buyerId, string orderCode, decimal amount)
        {
            Id = NewId.NextSequentialGuid();
            BuyerId = buyerId;
            OrderCode = orderCode;
            Amount = amount;
            CreatedTime = DateTime.UtcNow;
            Status = PaymentStatus.Pending;
        }

        public void SetPaymentStatus(PaymentStatus status)
        {
            Status = status;
        }


    }

    public enum PaymentStatus
    {
        Success,
        Failed,
        Pending
    }
}
