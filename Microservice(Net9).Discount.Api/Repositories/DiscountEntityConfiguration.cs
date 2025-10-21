
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using _Discount = Microservice_Net9_.Discount.Api.Features.Discounts.Discount;

namespace Microservice_Net9_.Discount.Api.Repositories
{
    public class DiscountEntityConfiguration : IEntityTypeConfiguration<_Discount>
    {
        public void Configure(EntityTypeBuilder<_Discount> builder)
        {
            builder.ToCollection("discounts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.UserId).HasElementName("user_id");

            builder.Property(x => x.Rate).HasElementName("rate");
            builder.Property(x => x.Code).HasElementName("code").HasMaxLength(10);

            builder.Property(x => x.CreateTime).HasElementName("createTime");
            builder.Property(x => x.UpdateTime).HasElementName("updateTime");
            builder.Property(x => x.ExpireTime).HasElementName("expireTime");
        }
    }
}
