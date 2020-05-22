using System;
using System.Linq;
using Angle.Interfaces;
using Microsoft.AspNetCore.Mvc;
using LUNA.Models;
using System.Security.Claims;
using LUNA.Models.Models;
using Angle.Helpers;

namespace Angle.Controllers
{
    /// <summary>
    /// Index_QR Controller, used for create and read Index_QR entities
    /// its also makes the redirection of the scanned QR in a kind of look up structure
    /// In case of QR Error QRErrorMode function is called
    /// Try an catches in every methode/function prevents from DB Errors
    /// </summary>
    public class Index_QRController : Controller
    {
        private readonly IUnityOfWork _unityOfWork;

        /// <summary>
        /// Class Contructor, takes and received unityOfWork object 
        /// (alias an intance of FrameWork.DatabaseContext)
        /// and references it to its class variable
        /// </summary>
        /// <param name="unityOfWork">Object is an instance of the Implemented
        /// Interfaces from Type Framework.DatabaseContext (Entity FrameWork Core), its used inside of Controller methods
        /// to execute DB Queries</param>
        public Index_QRController(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        /// <summary>
        /// Load all Index_QR entites to an List and pass it to the View 
        /// </summary>
        /// <returns>In case the query is succesful it redirect to the view and passes all enities as list
        /// in case db query is unsuccesful it redirects to an Error Page</returns>
        public ActionResult Index()
        {
            try
            {
                var Index_QRs = _unityOfWork.Index_QR.GetAll();
                return View(Index_QRs);
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }



        /// <summary>
        /// Redirects to spefic Controller and Action inside of Index_QR Entity
        /// Before redirection it makes an QR Validation. its kind of a lookup function
        /// </summary>
        /// <param name="id">Index_QR ID</param>
        /// <returns>In cs QR is valid it redirect to the specified Controller and Action
        /// per exampe /Product/Wizard/5
        /// case qr Its not Valid it return the QRErrorMode page with error description
        /// </returns>
        public ActionResult Redirect(long Id)
        {
            try
            {
                var QR = _unityOfWork.Index_QR.GetById(Id);

                var errorMessage = QRHelper.CheckQRError(QR);
                if (errorMessage != "")
                {
                    return RedirectToAction("QRErrorMode", "Index_QR", new { errorMessage = errorMessage });
                }

                return RedirectToAction(QR.Action_QR.Name, QR.Controller_QR.Name, new { id = QR.ProductID });
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        /// <summary>
        /// Takes the QR Error Message and Return QR Error View with errorMessage
        /// in its ViewBag
        /// </summary>
        /// <param name="errorMessage">Received from QR Validation Method
        /// "CheckQRError" an string with error description</param>
        /// <returns>QRErrorMode view with the description of QR Error</returns>
        public ActionResult QRErrorMode(string errorMessage)
        {
            ViewBag.ErrorMessage = errorMessage;
            return View();
        }




        /// <summary>
        /// Methode called when user generate and Index_QR entity, 
        /// It takes the ProuctID and COntroller and action name those are passed by the view
        /// looks for its entities in the DB and create a new Index_QR entity with 
        /// composed of those entites and also includes the User entity who was logged in
        /// </summary>
        /// <param name="productID">Product of ID to generate</param>
        /// <param name="controllerName">Name of controller, so the ID can be found</param>
        /// <param name="actionName">Name of the action, so the ID can be found</param>
        /// <returns>In case of insert query success it return the same page with the new generated QR
        ///  in case of fail it return an error page</returns>

        public ActionResult Create(long productID, string controllerName, string actionName)
        {
            try
            {
                Controller_QR controller_QR = _unityOfWork.Controller_QR.GetAll().FirstOrDefault(b => b.Name == controllerName);
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
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }






        /// <summary>
        /// Used when user wants to delete an Index_QR entity
        /// </summary>
        /// <param name="Id">Index_QR ID</param>
        /// <returns>Redirect the user back to de Index_QR Index page
        /// in case that the DB query doesnt work it redirect to an error page</returns>
        public ActionResult Delete(long Id)
        {
            try
            {
                var QR = _unityOfWork.Index_QR.GetById(Id);
                _unityOfWork.Index_QR.Delete(QR);
                _unityOfWork.Save();
                var QRs = _unityOfWork.Index_QR.GetAll();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }



        /// <summary>
        /// This method wenn called, it check the actual IsActive status
        /// and change it to the opposite status
        /// </summary>
        /// <param name="id">Index_QR ID as Parameter</param>
        /// <returns>Redirect the user back to de Index_QR Index page
        /// in case that the DB query doesnt work it redirect to an error page</returns>
        public ActionResult ChangeStatus(long id)
        {
            try
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
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }



    }
}