namespace Riverbooks.OrderProcessing.EndPoints;

internal record OrderSummary(Guid UserId,
                             DateTime DateCreated,
                             Guid OrderId,
                             decimal Total);
