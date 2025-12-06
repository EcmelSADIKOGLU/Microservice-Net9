using Microservice_Net9_.Order.Application.Contracts.Refit.PaymentService;
using Microservice_Net9_.Order.Application.Contracts.Repositories;
using Microservice_Net9_.Order.Application.Contracts.UnitOfWork;
using Microservice_Net9_.Order.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Microservice_Net9_.Order.Application.BackgroundServices
{
    public class CheckPaymentStatusOrderBackgrounService(IServiceProvider serviceProvider) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = serviceProvider.CreateScope())
            { 
                var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();
                var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                

                while (!stoppingToken.IsCancellationRequested)
                {
                    var pendentOrders = await orderRepository.GetSuspendedOrders();

                    foreach (var order in pendentOrders)
                    {
                        var orderStatusResponse = await paymentService.GetStatusAsync(order.OrderCode);

                        if (orderStatusResponse.isPaid)
                        {
                            await orderRepository.SetStatus(order.OrderCode, orderStatusResponse.PaymentId!.Value, OrderStatus.Paid);
                            orderRepository.Update(order);
                            await unitOfWork.CommitAsync(stoppingToken);
                        }

                    }

                    await Task.Delay(2000, stoppingToken); // Wait for 2 seconds before checking again
                }

               

            }
        }
    }
}
