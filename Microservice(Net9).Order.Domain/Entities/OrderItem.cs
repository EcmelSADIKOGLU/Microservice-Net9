namespace Microservice_Net9_.Order.Domain.Entities
{
    public class OrderItem : BaseEntity<int>
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = default!;
        public decimal UnitPrice { get; set; }

        public Guid OrderId { get; set; }
        public Order Order { get; set; } = default!;


        public void SetItem(Guid productId, string productName, decimal unitPrice)
        {
            if (string.IsNullOrEmpty(productName))
            {
                throw new ArgumentNullException(nameof(productName), "ProductName can not be empty.");
            }

            if (unitPrice <= 0)
            {
                throw new ArgumentNullException(nameof(unitPrice), "Unit Price can not be less than or equal to zero.");
            }

            this.ProductId = productId;
            this.ProductName = productName;
            this.UnitPrice = unitPrice;
        }

        public void UpdatePrice(decimal unitPrice)
        {
            if (unitPrice <= 0)
            {
                throw new ArgumentNullException(nameof(unitPrice),"Unit Price can not be less than or equal to zero.");
            }

            this.UnitPrice = unitPrice;
        }

        public void ApplyDiscount(float discountPercent)
        {
            if (discountPercent < 0 || discountPercent > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(discountPercent), "Discount percent must be between 0 and 100.");
            }
            UnitPrice -= (UnitPrice * (decimal)discountPercent) / 100;
        }

        public bool IsSameItem(Guid productId)
        {
            return this.ProductId == productId;
        }




    }
}
