using Microsoft.AspNetCore.Mvc;
using OrderEditorWebApp.Models;
using OrderEditorWebApp.Services;
using System.Threading.Tasks;

namespace OrderEditorWebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationContext _db;
        private readonly LoadDb _loadDb;
        private readonly AddObjectInDb _addObject;
        private readonly DeleteObjectInDb _deleteObject;
        private readonly EditObjectInDb _editObject;

        public OrderController(ApplicationContext db, LoadDb loadDb, AddObjectInDb addObject,
                                    DeleteObjectInDb deleteObject, EditObjectInDb editObject)
        {
            _db = db;
            _loadDb = loadDb;
            _addObject = addObject;
            _deleteObject = deleteObject;
            _editObject = editObject;
        }

        public async Task<IActionResult> Index()
        {
            var orders = new ViewModelOrder();

            orders.Providers = await _loadDb.LoadAllProviders(_db);
            orders.Orders = await _loadDb.LoadAllOrders(_db);

            return View(orders);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(ViewModelOrder viewModel)
        {
            await _addObject.AddOrder(viewModel, _db);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (id == 0)
            {
                ViewData["Message"] = "Object not found";
                return View();
            }

            await _deleteObject.DeleteOrder(id, _db);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditOrder(int id)
        {
            if (id == 0)
            {
                ViewData["Message"] = "Object not found";
                return View();
            }

            var orders = new ViewModelOrder();

            orders.Order = await _loadDb.LoadOrder(id, _db);
            orders.Providers = await _loadDb.LoadAllProviders(_db);
            orders.OrderItem = await _loadDb.LoadItemToOrder(orders.Order.Id, _db);

            if (orders != null)
                return View(orders);

            ViewData["Message"] = "Object not found";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditOrder(ViewModelOrder viewModel)
        {
            await _editObject.EditOrder(viewModel, _db);

            return RedirectToAction("Index");
        }
    }
}
