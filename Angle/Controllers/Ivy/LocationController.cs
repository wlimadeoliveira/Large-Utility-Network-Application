using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Angle.Interfaces;
using Angle.Models.Models.Ivy;
using Angle.Models.ViewModels.Ivy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Angle.Controllers.Ivy
{
    [Authorize]
    public class LocationController : Controller
    {

        private readonly IUnityOfWork _unityOfWork;
        public LocationController(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }
        // GET: Location
        public ActionResult Index()
        {
            var locations = _unityOfWork.Location.GetAll();

            return View("~/Views/Ivy/Location/Index.cshtml", locations);
        }

        // GET: Location/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Location/Create
        public ActionResult Create()
        {
            return View("~/Views/Ivy/Location/Create.cshtml");
        }

        // POST: Location/Create
        [HttpPost]
        public ActionResult Create(Location model)
        {
            try
            {
                _unityOfWork.Location.Insert(model);
                _unityOfWork.Save();
                return View("~/Views/Ivy/Location/Index.cshtml");
            }
            catch
            {
                return View("~/Views/Ivy/Location/Create.cshtml");
            }
        }

        // GET: Location/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Location/Edit/5
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

        // GET: Location/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Location/Delete/5
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