using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Angle.Interfaces;
using Angle.Models.Models.Ivy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Angle.Controllers.Ivy
{
    [Authorize]
    public class CategoryController : Controller
    {

        // GET: Category

        private readonly IUnityOfWork _unityOfWork;

        public CategoryController(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        public ActionResult Index()
        {
            var Categorys = _unityOfWork.Category.GetAll();
            return View("~/Views/Ivy/Category/Index.cshtml",Categorys);
        }

        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View("~/Views/Ivy/Category/Create.cshtml");
        }

        // POST: Category/Create
        [HttpPost]
        public ActionResult Create(Category Category)
        {
            try
            {
                _unityOfWork.Category.Insert(Category);
              
                _unityOfWork.Save();
                return View("~/Views/Ivy/Category/Index.cshtml");
            }
            catch
            {
                return View("~/Views/Ivy/Category/Index.cshtml");
            }
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Category/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}