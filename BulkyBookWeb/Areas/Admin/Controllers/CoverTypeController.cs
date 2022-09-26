using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBookWeb.Data;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //GET Index
        public IActionResult Index()
        {
            return View(_unitOfWork.CoverType.GetAll());
        }
        //GET CREATE    
        public IActionResult Create()
        {
            return View();
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType coverType)
        {
            if(coverType != null)
            {
                _unitOfWork.CoverType.Add(coverType);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View();
        }


        //GET - EDIT
        public IActionResult Edit(int? id)
        {
            if (id > 0 && id != null)
            {
                return View(_unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id));
            }
            return NotFound();
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType coverType)
        {
            if(coverType != null)
            {
                _unitOfWork.CoverType.Update(coverType);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        //GET - DELETE
        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                CoverType coverType = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
                return View(coverType);
            }
            return NotFound();
        }

        //POST - DELETE
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int id)
        {
            if (id > 0)
            {
                CoverType coverType = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
                _unitOfWork.CoverType.Remove(coverType);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View();
        }



    }
}