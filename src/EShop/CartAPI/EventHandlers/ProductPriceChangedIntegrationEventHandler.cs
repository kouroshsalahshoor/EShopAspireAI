using MassTransit;
using ServiceDefaults.Messaging.Events;

namespace CartAPI.EventHandlers;

public class ProductPriceChangedIntegrationEventHandler(ICartService service)
    : IConsumer<ProductPriceChangedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<ProductPriceChangedIntegrationEvent> context)
    {
        // find products on basket and update price
        await service.UpdateCartItemProductPrices(context.Message.ProductId, context.Message.Price);
    }
}
