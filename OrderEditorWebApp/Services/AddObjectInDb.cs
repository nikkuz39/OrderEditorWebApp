using Microsoft.EntityFrameworkCore;
using OrderEditorWebApp.Models;
using System;
using System.Threading.Tasks;

namespace OrderEditorWebApp.Services
{
    public class AddObjectInDb
    {
        public async Task AddProvider(Provider provider, ApplicationContext db)
        {
            db.Providers.Add(provider);
            await db.SaveChangesAsync();
        }

        public async Task AddOrder(ViewModelOrder viewModel, ApplicationContext db)
        {
            Provider provider = await db.Providers.FirstOrDefaultAsync(i => i.Id == viewModel.Provider.Id);

            Order order = new Order()
            {
                Number = viewModel.Order.Number,
                Date = viewModel.Order.Date,
                ProviderId = provider.Id,
            };

            db.Providers.Attach(provider);
            db.Orders.Add(order);
            await db.SaveChangesAsync();
        }

        public async Task AddItem(ViewModelOrder viewModel, ApplicationContext db)
        {
            Order order = await db.Orders.FirstOrDefaultAsync(i => i.Id == viewModel.Order.Id);
            
            OrderItem item = new OrderItem()
            {
                Name = viewModel.OrderItem.Name,
                Quantity = viewModel.OrderItem.Quantity,
                Unit = viewModel.OrderItem.Unit,
                OrderId = order.Id,
            };

            db.Orders.Attach(order);
            db.OrderItems.Add(item);
            await db.SaveChangesAsync();
        }
    }
}
