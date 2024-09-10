
using Ardalis.GuardClauses;

namespace RiverBooks.Users;

public class CartItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid BookId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public string Description { get; set; } = string.Empty;

    public CartItem(Guid bookId, string description, int quantity, decimal unitPrice)
    {
        Quantity = Guard.Against.NegativeOrZero(quantity, nameof(quantity));
        UnitPrice = Guard.Against.NegativeOrZero(unitPrice, nameof(unitPrice));
        Description = Guard.Against.NullOrEmpty(description, nameof(description));
        BookId = Guard.Against.Default(bookId, nameof(bookId));
    }

    public void AdjustQuantity(int quantity)
    {
        Quantity = Guard.Against.NegativeOrZero(quantity, nameof(quantity));
    }
}