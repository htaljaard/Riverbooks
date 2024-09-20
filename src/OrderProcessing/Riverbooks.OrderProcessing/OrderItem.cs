using Ardalis.GuardClauses;

namespace Riverbooks.OrderProcessing
{
    internal class OrderItem
    {
        public OrderItem(Guid bookId, int quantity, decimal unitPrice, string description)
        {
            BookdId = Guard.Against.Default(bookId);
            Quantity = Guard.Against.NegativeOrZero(quantity, nameof(quantity));
            UnitPrice = Guard.Against.NegativeOrZero(unitPrice, nameof(unitPrice));
            Description = Guard.Against.NullOrEmpty(description, nameof(description));
        }


        public OrderItem()
        {
            //EF    
        }
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid BookdId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public string Description { get; private set; }
    }
}