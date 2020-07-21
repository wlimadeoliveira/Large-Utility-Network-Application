using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Angle.Interfaces;
using LUNA.Models;

namespace Angle.Controllers
{
    public class HistoryController : Controller
    {
        private readonly IUnityOfWork _unityOfWork;

        public HistoryController(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }
        public ActionResult Index()
        {
            var histories = _unityOfWork.History.GetAll();
            return View(histories);
        }
        public ActionResult Details(int id)
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(History history)
        {
            try
            {
                _unityOfWork.History.Insert(history);
                _unityOfWork.Save();
                return RedirectToAction("Index", "History");
                LoggingController.writeLog(history, User.Identity.Name, this.ControllerContext.RouteData.Values["action"].ToString(), this.ControllerContext.RouteData.Values["controller"].ToString());

            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public JsonResult CreateOnPost(History history)
        {

            try
            {
                _unityOfWork.History.Insert(history);
                _unityOfWork.Save();
                var historytypes = _unityOfWork.History.GetAll();
                return Json(historytypes);
                LoggingController.writeLog(history, User.Identity.Name, this.ControllerContext.RouteData.Values["action"].ToString(), this.ControllerContext.RouteData.Values["controller"].ToString());

            }
            catch
            {
                var historytypes = _unityOfWork.History.GetAll();
                return Json(historytypes);
            }
        }

        public ActionResult Edit(int id)
        {
            var history = _unityOfWork.History.GetById(id);

            return View(history);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(History model)
        {
            try
            {
                var historyType = _unityOfWork.History.GetById(model.ID);
                historyType.Description = model.Description;
                historyType.BootStrapBadge = model.BootStrapBadge;
                _unityOfWork.History.Update(historyType);
                _unityOfWork.Save();
                LoggingController.writeLog(historyType, User.Identity.Name, this.ControllerContext.RouteData.Values["action"].ToString(), this.ControllerContext.RouteData.Values["controller"].ToString());

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                var historyType = _unityOfWork.History.GetById(id);
                _unityOfWork.History.Delete(historyType);
                _unityOfWork.Save();
                LoggingController.writeLog(historyType, User.Identity.Name, this.ControllerContext.RouteData.Values["action"].ToString(), this.ControllerContext.RouteData.Values["controller"].ToString());

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(nameof(Index));
            }
        }
    }
}