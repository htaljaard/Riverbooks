namespace RiverBooks.Users.CartEndPoints
{
    public record CartItemDTO(Guid Id, Guid BookId, string Description, int Quantity, decimal UnitPrice);
}


