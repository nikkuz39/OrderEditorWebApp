using System.Collections.Generic;

namespace OrderEditorWebApp.Models
{
    public class ViewModelOrder
    {
        public Order Order { get; set; }
        public OrderItem OrderItem { get; set; }
        public Provider Provider { get; set; }

        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }
        public IEnumerable<Provider> Providers { get; set; }
    }
}
