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
    public class ManufacturerController : Controller
    {

        // GET: Manufacturer

        private readonly IUnityOfWork _unityOfWork;

        public ManufacturerController(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        public ActionResult Index()
        {
            var manufacturers = _unityOfWork.Manufacturer.GetAll();
            return View("~/Views/Ivy/Manufacturer/Index.cshtml",manufacturers);
        }

        // GET: Manufacturer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Manufacturer/Create
        public ActionResult Create()
        {
            return View("~/Views/Ivy/Manufacturer/Create.cshtml");
        }

        // POST: Manufacturer/Create
        [HttpPost]
        public ActionResult Create(Manufacturer manufacturer)
        {
            try
            {
                _unityOfWork.Manufacturer.Insert(manufacturer);
                _unityOfWork.Save();
                return View("~/Views/Ivy/Manufacturer/Index.cshtml");
            }
            catch
            {
                return View("~/Views/Ivy/Manufacturer/Index.cshtml");
            }
        }

        // GET: Manufacturer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Manufacturer/Edit/5
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

        // GET: Manufacturer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Manufacturer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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