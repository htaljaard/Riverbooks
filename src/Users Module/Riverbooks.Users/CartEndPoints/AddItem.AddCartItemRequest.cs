namespace RiverBooks.Users.CartEndPoints
{
    public record AddCartItemRequest(Guid BookId, int Quantity);
}
