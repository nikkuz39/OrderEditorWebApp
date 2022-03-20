using Microsoft.EntityFrameworkCore;
using OrderEditorWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderEditorWebApp.Services
{
    public class LoadDb
    {
        public async Task<List<Provider>> LoadAllProviders(ApplicationContext db)
        {
            var providers = new List<Provider>();

            providers = await db.Providers.ToListAsync();

            return providers;
        }

        public async Task<Provider> LoadProvider(int id, ApplicationContext db)
        {
            Provider provider = await db.Providers.FirstOrDefaultAsync(i => i.Id == id);

            return provider;
        }

        public async Task<List<Order>> LoadAllOrders(ApplicationContext db)
        {
            var orders = new List<Order>();

            orders = await db.Orders.ToListAsync();

            return orders;
        }

        public async Task<Order> LoadOrder(int id, ApplicationContext db)
        {
            Order order = await db.Orders.FirstOrDefaultAsync(i => i.Id == id);

            return order;
        }

        public async Task<List<OrderItem>> LoadAllItems(ApplicationContext db)
        {
            var items = new List<OrderItem>();

            items = await db.OrderItems.ToListAsync();

            return items;
        }

        public async Task<OrderItem> LoadItem(int id, ApplicationContext db)
        {
            OrderItem item = await db.OrderItems.FirstOrDefaultAsync(i => i.Id == id);

            return item;
        }

        public async Task<OrderItem> LoadItemToOrder(int id, ApplicationContext db)
        {
            OrderItem item = await db.OrderItems.FirstOrDefaultAsync(i => i.OrderId == id);

            return item;
        }

        public async Task<ViewModelOrder> LoadDetailOrder(int id, ApplicationContext db)
        {
            var viewModel = new ViewModelOrder();

            Order order = await db.Orders.FirstOrDefaultAsync(i => i.Id == id);

            OrderItem item = await db.OrderItems.FirstOrDefaultAsync(i => i.OrderId == order.Id);

            viewModel.Order = order;
            viewModel.OrderItem = item;
            viewModel.Providers = await db.Providers.ToListAsync();

            return viewModel;
        }

        public async Task<List<Order>> SearchData(int[] itemId, ApplicationContext db)
        {
            var dateStart = DateTime.Today.AddMonths(-1);
            var dateEnd = DateTime.Today;

            var orders = new List<Order>();

            foreach (int idItem in itemId)
            {
                var order = new Order();
                order = await db.Orders.FirstOrDefaultAsync(i => i.Id == idItem && i.Date >= dateStart && i.Date <= dateEnd);

                orders.Add(order);
            }

            return orders;
        }

        public async Task<List<Order>> SearchOrders(int[] itemId, string _dateStart, string _dateEnd, ApplicationContext db)
        {
            var dateStart = new DateTime();
            dateStart = Convert.ToDateTime(_dateStart);

            var dateEnd = new DateTime();
            dateEnd = Convert.ToDateTime(_dateEnd);

            var orders = new List<Order>();

            foreach (int idItem in itemId)
            {
                var order = new Order();
                order = await db.Orders.FirstOrDefaultAsync(i => i.Id == idItem && i.Date >= dateStart && i.Date <= dateEnd);

                orders.Add(order);
            }

            return orders;
        }

        public async Task<List<Order>> SearchProviders(int[] providerId, ApplicationContext db)
        {
            var dateStart = DateTime.Today.AddMonths(-1);
            var dateEnd = DateTime.Today;

            var providers = new List<Order>();

            foreach (int idItem in providerId)
            {
                var order = new Order();
                order = await db.Orders.FirstOrDefaultAsync(i => i.ProviderId == idItem && i.Date >= dateStart && i.Date <= dateEnd);
                providers.Add(order);
            }

            return providers;
        }

        public async Task<List<Order>> SearchProviders(int[] providerId, string _dateStart, string _dateEnd, ApplicationContext db)
        {
            var dateStart = new DateTime();
            dateStart = Convert.ToDateTime(_dateStart);

            var dateEnd = new DateTime();
            dateEnd = Convert.ToDateTime(_dateEnd);

            var providers = new List<Order>();

            foreach (int idItem in providerId)
            {
                var order = new Order();
                order = await db.Orders.FirstOrDefaultAsync(i => i.ProviderId == idItem && i.Date >= dateStart && i.Date <= dateEnd);
                providers.Add(order);
            }

            return providers;
        }

        public ViewModelOrder SearchDate(string _dateStart, string _dateEnd, ApplicationContext db)
        {
            var dateStart = new DateTime();
            dateStart = Convert.ToDateTime(_dateStart);

            var dateEnd = new DateTime();
            dateEnd = Convert.ToDateTime(_dateEnd);

            var viewModel = new ViewModelOrder();

            IQueryable<Order> orders = db.Orders.Where(i => i.Date >= dateStart && i.Date <= dateEnd);

            viewModel.Orders = orders.ToList();

            return viewModel;
        }
    }
}
