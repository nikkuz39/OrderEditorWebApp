using Microsoft.EntityFrameworkCore;
using OrderEditorWebApp.Models;
using System.Threading.Tasks;

namespace OrderEditorWebApp.Services
{
    public class DeleteObjectInDb
    {
        public async Task DeleteProvider(int id, ApplicationContext db)
        {
            Provider provider = new Provider { Id = id };

            db.Entry(provider).State = EntityState.Deleted;

            await db.SaveChangesAsync();
        }

        public async Task DeleteOrder(int id, ApplicationContext db)
        {
            Order order = new Order { Id = id };

            db.Entry(order).State = EntityState.Deleted;

            await db.SaveChangesAsync();
        }

        public async Task DeleteItem(int id, ApplicationContext db)
        {
            OrderItem item = new OrderItem { Id = id };

            db.Entry(item).State = EntityState.Deleted;

            await db.SaveChangesAsync();
        }
    }
}
