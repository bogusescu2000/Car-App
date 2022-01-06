using InAndOut.Data;
using InAndOut.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Controllers
{
    public class ItemController : Controller
    {

        private readonly ApplicationDbContext _db;

        public ItemController(ApplicationDbContext db)
        {
            _db = db;
        } 

        public IActionResult Index()
        {
            IEnumerable<Item> objList = _db.Items;
            return View(objList);
        } 
        
        //GET-Create
        public IActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //POST-Create
        public IActionResult Create(Item obj)
        {
            _db.Items.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET-Delete
        public IActionResult Delete(int? id)
        {

            if(id == null)
            {
                return NotFound();

            }
            var obj = _db.Items.Find(id);
            if(obj == null)
            {
                return NotFound();

            }
            return View(obj);
        }

        //POST-Delete
        public IActionResult DeleteItem(Item obj)
        {
            _db.Items.Remove(obj);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        //GET-Update
        public IActionResult Update(int? id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var obj = _db.Items.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            else
            {
                return View(obj);
            }
            
        }

        //POST-Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateItem(Item obj)
        {
            _db.Items.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
