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
    public class SupplierController : Controller
    {

        // GET: Supplier

        private readonly IUnityOfWork _unityOfWork;

        public SupplierController(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        public ActionResult Index()
        {
            var Suppliers = _unityOfWork.Supplier.GetAll();
            return View("~/Views/Ivy/Supplier/Index.cshtml",Suppliers);
        }

        // GET: Supplier/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Supplier/Create
        public ActionResult Create()
        {
            return View("~/Views/Ivy/Supplier/Create.cshtml");
        }

        // POST: Supplier/Create
        [HttpPost]
        public ActionResult Create(Supplier Supplier)
        {
            try
            {
                _unityOfWork.Supplier.Insert(Supplier);
              
                _unityOfWork.Save();
                return View("~/Views/Ivy/Supplier/Index.cshtml");
            }
            catch
            {
                return View("~/Views/Ivy/Supplier/Index.cshtml");
            }
        }

        // GET: Supplier/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Supplier/Edit/5
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

        // GET: Supplier/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Supplier/Delete/5
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