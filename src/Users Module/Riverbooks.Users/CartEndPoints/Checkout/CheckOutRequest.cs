namespace RiverBooks.Users.CartEndPoints.Checkout
{
    internal record CheckOutRequest(Guid ShippingAddressId, Guid BillingAddressId);
    
}