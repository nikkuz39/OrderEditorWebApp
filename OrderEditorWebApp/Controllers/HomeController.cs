using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderEditorWebApp.Models;
using OrderEditorWebApp.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OrderEditorWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _db;
        private readonly LoadDb _loadDb;
        private readonly EditObjectInDb _editObject;

        public HomeController(ILogger<HomeController> logger, ApplicationContext db, LoadDb loadDb, EditObjectInDb editObject)
        {
            _logger = logger;
            _db = db;
            _loadDb = loadDb;
            _editObject = editObject;
        }

        public async Task<IActionResult> Index(int[] orderId, int[] providerId, string dateStart, string dateEnd)
        {
            var viewModel = new ViewModelOrder();

            viewModel.Orders = await _loadDb.LoadAllOrders(_db);

            if (orderId.Length > 0)
                viewModel.Orders = await _loadDb.SearchData(orderId, _db);

            if (orderId.Length > 0 && dateStart != null && dateEnd != null)
                viewModel.Orders = await _loadDb.SearchOrders(orderId, dateStart, dateEnd, _db);

            if (providerId.Length > 0)
                viewModel.Orders = await _loadDb.SearchProviders(providerId, _db);

            if (providerId.Length > 0 && dateStart != null && dateEnd != null)
                viewModel.Orders = await _loadDb.SearchProviders(providerId, dateStart, dateEnd, _db);

            if (dateStart != null && dateEnd != null)
                viewModel = _loadDb.SearchDate(dateStart, dateEnd, _db);


            viewModel.Providers = await _loadDb.LoadAllProviders(_db);

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                ViewData["Message"] = "Object not found";
                return View();
            }

            var orders = new ViewModelOrder();

            orders = await _loadDb.LoadDetailOrder(id, _db);

            if (orders != null)
                return View(orders);

            ViewData["Message"] = "Object not found";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Details(ViewModelOrder viewModel)
        {
            await _editObject.EditOrder(viewModel, _db);

            await _editObject.EditItem(viewModel, _db);

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
