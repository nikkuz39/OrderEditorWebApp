using Microsoft.AspNetCore.Mvc;
using OrderEditorWebApp.Models;
using OrderEditorWebApp.Services;
using System.Threading.Tasks;

namespace OrderEditorWebApp.Controllers
{
    public class ItemController : Controller
    {
        private readonly ApplicationContext _db;
        private readonly LoadDb _loadDb;
        private readonly AddObjectInDb _addObject;
        private readonly DeleteObjectInDb _deleteObject;
        private readonly EditObjectInDb _editObject;

        public ItemController(ApplicationContext db, LoadDb loadDb, AddObjectInDb addObject,
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
            var items = new ViewModelOrder();

            items.OrderItems = await _loadDb.LoadAllItems(_db);
            items.Providers = await _loadDb.LoadAllProviders(_db);
            items.Orders = await _loadDb.LoadAllOrders(_db);

            return View(items);
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem(ViewModelOrder viewModel)
        {
            await _addObject.AddItem(viewModel, _db);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteItem(int id)
        {
            if (id == 0)
            {
                ViewData["Message"] = "Object not found";
                return View();
            }

            await _deleteObject.DeleteItem(id, _db);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditItem(int id)
        {
            if (id == 0)
            {
                ViewData["Message"] = "Object not found";
                return View();
            }

            var items = new ViewModelOrder();

            items.OrderItem = await _loadDb.LoadItem(id, _db);
            items.Orders = await _loadDb.LoadAllOrders(_db);

            if (items != null)
                return View(items);

            ViewData["Message"] = "Object not found";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditItem(ViewModelOrder viewModel)
        {
            await _editObject.EditItem(viewModel, _db);

            return RedirectToAction("Index");
        }
    }
}
