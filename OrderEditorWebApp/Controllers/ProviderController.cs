using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using OrderEditorWebApp.Models;
using OrderEditorWebApp.Services;
using System;

namespace OrderEditorWebApp.Controllers
{
    public class ProviderController : Controller
    {
        private readonly ApplicationContext _db;
        private readonly LoadDb _loadDb;
        private readonly AddObjectInDb _addObject;
        private readonly DeleteObjectInDb _deleteObject;
        private readonly EditObjectInDb _editObject;

        public ProviderController(ApplicationContext db, LoadDb loadDb, AddObjectInDb addObject, 
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
            var providers = new ViewModelOrder();

            providers.Providers = await _loadDb.LoadAllProviders(_db);

            return View(providers);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProvider(Provider provider)
        {
            await _addObject.AddProvider(provider, _db);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteProvider(int id)
        {
            if (id == 0)
            {
                ViewData["Message"] = "Object not found";
                return View();
            }

            await _deleteObject.DeleteProvider(id, _db);

            return RedirectToAction("Index");           
        }

        public async Task<IActionResult> EditProvider(int id)
        {
            if (id == 0)
            {
                ViewData["Message"] = "Object not found";
                return View();
            }

            var provider = await _loadDb.LoadProvider(id, _db);

            if (provider != null)
                return View(provider);


            ViewData["Message"] = "Object not found";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditProvider(Provider provider)
        {
            await _editObject.EditProvider(provider, _db);

            return RedirectToAction("Index");
        }
    }
}
