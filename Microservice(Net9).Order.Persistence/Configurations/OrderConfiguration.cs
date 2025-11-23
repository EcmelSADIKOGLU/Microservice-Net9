using Microsoft.EntityFrameworkCore;
using _Order = Microservice_Net9_.Order.Domain.Entities.Order;


namespace Microservice_Net9_.Order.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<_Order>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<_Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.OrderCode).IsRequired().HasMaxLength(10);

            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.BuyerId).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.AddressId).IsRequired();
            builder.Property(x => x.TotalPrice).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(x => x.DiscountPercent).IsRequired(false).HasColumnType("Float");
            builder.Property(x => x.PaymentId).IsRequired(false);

            builder.HasMany(x => x.OrderItems)
                   .WithOne(x => x.Order)
                   .HasForeignKey(x => x.OrderId);
                   //.OnDelete(DeleteBehavior.Cascade)

            builder.HasOne(x => x.Address)
                   .WithMany()
                   .HasForeignKey(x => x.AddressId)
                   .IsRequired();
                   //.OnDelete(DeleteBehavior.Restrict)
        }
    }
}
