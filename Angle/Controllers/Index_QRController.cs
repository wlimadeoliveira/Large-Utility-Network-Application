using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Angle.Interfaces;
using Angle.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LUNA.Models;
using System.Security.Claims;
using LUNA.Models.Models;
using Angle.Helpers;

namespace Angle.Controllers
{
    public class Index_QRController : Controller
    {
        private readonly IUnityOfWork _unityOfWork;

        public Index_QRController(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        // GET: Index_QR
        public ActionResult Index()
        {
            var Index_QRs = _unityOfWork.Index_QR.GetAll();
            return View(Index_QRs);
        }

        // GET: Index_QR/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        /// <summary>
        /// Redirects to spefic Controller and Action 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Redirect(long Id) 
        {
            var QR = _unityOfWork.Index_QR.GetById(Id);

            var errorMessage = QRHelper.CheckQRError(QR);
            if(errorMessage != "")
            {            
                return RedirectToAction("QRErrorMode", "Index_QR", new { errorMessage = errorMessage});
            }
           
            return RedirectToAction(QR.Action_QR.Name, QR.Controller_QR.Name, new { id = QR.ProductID });
        }

        public ActionResult QRErrorMode(string errorMessage)
        {

            ViewBag.ErrorMessage = errorMessage;


            return View();
        }




        // POST: Index_QR/Create

        public ActionResult Create(long productID, string controllerName, string actionName)
        {
           
                

                Controller_QR controller_QR= _unityOfWork.Controller_QR.GetAll().FirstOrDefault(b => b.Name == controllerName);
                Action_QR action_QR = _unityOfWork.Action_QR.GetAll().FirstOrDefault(b => b.Name == actionName);
                Product Product = _unityOfWork.Product.GetById(productID);
            Index_QR model = new Index_QR()
            {
                Product = Product,
                Controller_QR = controller_QR,
                Action_QR = action_QR,
                //ProductID = productID,
                // ControllerID = controller_QR.ID,
                UserID = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                Created = DateTime.Now,
                IsActive = true
               
            };

                _unityOfWork.Index_QR.Insert(model);
                _unityOfWork.Save();

                return RedirectToAction("Detail", "Product", new { id = productID });
          
        }

        // GET: Index_QR/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Index_QR/Edit/5
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

        // GET: Index_QR/Delete/5
        public ActionResult Delete(int Id)
        {
            var QR = _unityOfWork.Index_QR.GetById(Id);
            _unityOfWork.Index_QR.Delete(QR);
            _unityOfWork.Save();
            var QRs = _unityOfWork.Index_QR.GetAll();
            return View(nameof(Index),QRs);
        }

        // POST: Index_QR/Delete/5
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

        public ActionResult ChangeStatus(int id)
        {
            var QR = _unityOfWork.Index_QR.GetById(id);
            if (QR.IsActive)
            {
                QR.IsActive = false;
            }
            else
            {
                QR.IsActive = true;
            }
            _unityOfWork.Index_QR.Update(QR);
            _unityOfWork.Save();
            var QRs = _unityOfWork.Index_QR.GetAll();
            return View(nameof(Index), QRs);
        }



    }
}