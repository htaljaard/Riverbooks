namespace RiverBooks.Users.CartEndPoints.AddItem
{
    public record AddCartItemRequest(Guid BookId, int Quantity);
}
