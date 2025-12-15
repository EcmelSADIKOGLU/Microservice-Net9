namespace Microservice_Net9_.Web.Pages.Basket.ViewModels
{
    public record BasketPageViewModel
    {
        public List<BasketViewModelItem> Items { get; set; } = [];

        private decimal TotalPrice { get; set; }

        private decimal? TotalPriceByDiscountRate { get; set; }
        public string? Coupon { get; set; }
        public float? DiscountRate { get; set; }

        public bool IsApplyDiscountCoupon => DiscountRate is > 0 && !string.IsNullOrEmpty(Coupon);

        public bool HasItem => Items.Count > 0;


        public decimal GetTotalPrice()
        {
            return IsApplyDiscountCoupon ? TotalPriceByDiscountRate!.Value : TotalPrice;
        }


        public void SetPrice(decimal totalPrice, decimal? totalPriceByDiscountRate)
        {
            TotalPrice = totalPrice;
            TotalPriceByDiscountRate = totalPriceByDiscountRate;
        }

        public static BasketPageViewModel Empty()
        {
            return new BasketPageViewModel() 
            {
                Coupon = null,
                DiscountRate = null,
                TotalPrice = 0,
                TotalPriceByDiscountRate = null,
                Items = new List<BasketViewModelItem>()
            };
        }
    }

    public record BasketViewModelItem(
        Guid Id,
        string? PictureUrl,
        string Name,
        decimal Price,
        decimal? PriceWithDiscountRate);
}
