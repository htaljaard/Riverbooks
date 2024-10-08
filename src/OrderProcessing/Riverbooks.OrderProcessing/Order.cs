﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riverbooks.OrderProcessing
{
    internal class Order
    {


        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid UserId { get; private set; }

        public Address ShippingAddress { get; private set; } = default!;

        public Address BillingAddress { get; private set; } = default!;

        private readonly List<OrderItem> _orderItems = new();

        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

        public DateTime DateCreated { get; private set; } = DateTime.Now;

        private void AddOrderItem(OrderItem orderItem) => _orderItems.Add(orderItem);

        public static Order Create(Guid userId, Address shippingAddress, Address billingAddress, List<OrderItem> orderItems)
        {
            var order = new Order
            {
                UserId = userId,
                ShippingAddress = shippingAddress,
                BillingAddress = billingAddress
            };

            foreach (var orderItem in orderItems)
            {
                order.AddOrderItem(orderItem);
            }

            return order;
        }
    }
}
