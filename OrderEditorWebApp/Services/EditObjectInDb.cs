using Microsoft.EntityFrameworkCore;
using OrderEditorWebApp.Models;
using System.Threading.Tasks;

namespace OrderEditorWebApp.Services
{
    public class EditObjectInDb
    {
        public async Task EditProvider(Provider provider, ApplicationContext db)
        {
            db.Providers.Update(provider);
            await db.SaveChangesAsync();
        }

        public async Task EditOrder(ViewModelOrder viewModel, ApplicationContext db)
        {
            Order order = db.Orders.Find(viewModel.Order.Id);

            if (order != null)
            {
                order.Number = viewModel.Order.Number;
                order.Date = viewModel.Order.Date;
                order.ProviderId = viewModel.Provider.Id;

                db.Entry(order).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }

        public async Task EditItem(ViewModelOrder viewModel, ApplicationContext db)
        {
            OrderItem item = db.OrderItems.Find(viewModel.OrderItem.Id);

            if (item != null)
            {
                item.Name = viewModel.OrderItem.Name;
                item.Quantity = viewModel.OrderItem.Quantity;
                item.Unit = viewModel.OrderItem.Unit;
                item.OrderId = viewModel.Order.Id;

                db.Entry(item).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }
    }
}
