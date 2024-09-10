
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;

namespace RiverBooks.Users;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;

    private readonly List<CartItem> _cartItems = new();

    public IReadOnlyCollection<CartItem> CartItems => _cartItems.AsReadOnly();

    public void AddToCart(CartItem cartItem)
    {
        Guard.Against.Null(cartItem, nameof(cartItem));

        var existingBook = _cartItems.FirstOrDefault(x => x.BookId == cartItem.BookId);

        if (existingBook != null)
        {
            existingBook.AdjustQuantity(existingBook.Quantity + cartItem.Quantity);

            //TODO: Update Details if changed. 
            return;
        }
        _cartItems.Add(cartItem);
    }
}
