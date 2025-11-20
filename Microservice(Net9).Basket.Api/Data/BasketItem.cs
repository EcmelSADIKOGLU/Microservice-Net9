namespace Microservice_Net9_.Basket.Api.Data
{
    public class BasketItem
    {
        public Guid CourseId { get; set; }
        public string CourseName { get; set; } = default!;
        public decimal CoursePrice { get; set; }
        public string? ImageUrl { get; set; }
        public Guid UserId { get; set; } //EducatorUserId
        public string EducatorFullName { get; set; } = default!;
        public decimal? PriceByApplyDiscount { get; set; }


        public BasketItem( Guid CourseId, string CourseName, decimal CoursePrice, string? ImageUrl, Guid UserId, string EducatorFullName, decimal? PriceByApplyDiscount)
        {
            this.CourseId = CourseId;
            this.CourseName = CourseName;
            this.CoursePrice = CoursePrice;
            this.ImageUrl = ImageUrl;
            this.UserId = UserId;
            this.EducatorFullName = EducatorFullName;
            this.PriceByApplyDiscount = PriceByApplyDiscount;
        }

    }
}
