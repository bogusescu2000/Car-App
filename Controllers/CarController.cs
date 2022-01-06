using InAndOut.Data;
using InAndOut.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Controllers
{
    public class CarController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _appEnvironment;

        public CarController(ApplicationDbContext db, IWebHostEnvironment appEnvironment)
        {
            _db = db;
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {
            return View(this.GetCars(1));
        }

        
        /// ////////////Pagination///////////////////////
        [HttpPost]
        public IActionResult Index(int currentPageIndex)
        {
            return View(this.GetCars(currentPageIndex));
        }

        private CarModel GetCars(int currentPage)
        {
            int maxRows = 6;
            CarModel carModel = new();

            carModel.Cars = (from cars in this._db.Cars
                                       select cars)
                        .OrderByDescending(car => car.Id)
                        .Skip((currentPage - 1) * maxRows)
                        .Take(maxRows).ToList();

            double pageCount = (double)((decimal)this._db.Cars.Count() / Convert.ToDecimal(maxRows));
            carModel.PageCount = (int)Math.Ceiling(pageCount);

            carModel.CurrentPageIndex = currentPage;

            return carModel;
        }
        //GET-Create
        public IActionResult Create()
        {
            return View();
        }

        //POST-Create
        [HttpPost]
        public async Task<IActionResult> Create(IFormFile uploadedFile, Car item)
        {
            if (uploadedFile != null)
            {
                
                string path = "/Files/" + uploadedFile.FileName;
                
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                Car obj = new() { Name = item.Name, Age = item.Age, CarImage = path };
                
            _db.Cars.Add(obj);
            _db.SaveChanges();
           
            }
            return RedirectToAction("Index");
        }

        //GET-Update
        public IActionResult Update(int ? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var obj = _db.Cars.Find(id);
            if(obj == null)
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
        public async Task<IActionResult> UpdateCar(IFormFile uploadedFile, Car item)
        {
            if (uploadedFile != null)
            {
                string path = "/Files/" + uploadedFile.FileName;

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                Car obj = new() {Id = item.Id, Name = item.Name, Age = item.Age, CarImage = path };

                _db.Cars.Update(obj); 
                _db.SaveChanges();

            }
            return RedirectToAction("Index");
        }

        //GET-Delete
        public IActionResult Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();

            }
            var obj = _db.Cars.Find(id);
            if (obj == null)
            {
                return NotFound();

            }
            return View(obj);
        }

        //POST-Delete
        public IActionResult DeleteCar(Car obj)
        {
            _db.Cars.Remove(obj);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }



        ////Single Post
        //GET-SinglePost
        public IActionResult Details(int Id)
        {
           
            Car obj = _db.Cars.Find(Id);

            
            return View(obj);
        }
    }
}
