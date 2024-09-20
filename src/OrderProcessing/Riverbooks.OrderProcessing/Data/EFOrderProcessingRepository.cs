
using Microsoft.EntityFrameworkCore;

namespace Riverbooks.OrderProcessing.Data
{
    internal class EFOrderProcessingRepository : IOrderRepository
    {
        private readonly OrderProcessingDBContext _dbContext;

        public EFOrderProcessingRepository(OrderProcessingDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
        }

        public async Task<List<Order>> ListAsync()
        {
            return await _dbContext.Orders
                .Include(o => o.OrderItems)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}