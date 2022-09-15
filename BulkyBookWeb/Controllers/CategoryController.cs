﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBookWeb.Data;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _db;

        public CategoryController(ICategoryRepository db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categories = _db.GetAll();
            return View(categories);
        }
        //GET CREATE    
        public IActionResult Create()
        {
            return View();
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name != null && obj.DisplayOrder != null)
            {
                if (obj.Name == obj.DisplayOrder.ToString())
                {
                    ModelState.AddModelError("CustomError", "The Display Order cannot exactly match the Name");
                }

                if (ModelState.IsValid)
                {

                    _db.Add(obj);
                    _db.Save();
                    TempData["success"] = "Category created successfully!";
                    return RedirectToAction("Index", "Category");
                }
            }
            return View(obj);
        }


        //GET - EDIT
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDbFirst = _db.GetFirstOrDefault(u => u.Id == id);
            //var categoryFromDb = _db.Find(id);
            //var categoryFromDbFirst = _db.Category.FirstOrDefault(u => u.Id == id);
            //var categoryFromDbFirst = _db.Category.GetFirstOrDefault(u => u.Id == id); Not Work
            //var categoryFromDbThird = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFromDbFirst== null)
            {
                return NotFound();
            }

            return View(categoryFromDbFirst);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "The Display Order cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _db.Update(obj);
                _db.Save();

                TempData["success"] = "Category updated successfully!";

                return RedirectToAction("Index", "Category");
            }

            return View(obj);
        }


        //GET - DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDbFirst = _db.GetFirstOrDefault(u => u.Id == id);

            if (categoryFromDbFirst == null)
            {
                return NotFound();
            }

            return View(categoryFromDbFirst);
        }

        //POST - DELETE
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Remove(obj);
            _db.Save();

            TempData["success"] = "Category deleted successfully!";

            return RedirectToAction("Index");
        }



    }
}