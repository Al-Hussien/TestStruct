using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IExtendedUnitOfWork _extendedUnitOfWork;
        public CategoryController(IExtendedUnitOfWork unitOfWork)
        {
            _extendedUnitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            _extendedUnitOfWork.Category.GetAll();
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            Category category = new Category();
            if (id == null)
            {
                //this is for create
                return View(category);
            }
            //this is for edit
            category = _extendedUnitOfWork.Category.Get(id.GetValueOrDefault()); ;
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert (Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.Id == 0)
                {
                    _extendedUnitOfWork.Category.Add(category);
                }
                else
                {
                    _extendedUnitOfWork.Category.Update(category);
                }
                _extendedUnitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _extendedUnitOfWork.Category.GetAll();
            return Json(new { data = allObj });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _extendedUnitOfWork.Category.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _extendedUnitOfWork.Category.Remove(objFromDb);
            _extendedUnitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}