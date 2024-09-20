namespace RiverBooks.Users.CartEndPoints.AddItem
{
    public record CartItemDTO(Guid Id, Guid BookId, string Description, int Quantity, decimal UnitPrice);
}


