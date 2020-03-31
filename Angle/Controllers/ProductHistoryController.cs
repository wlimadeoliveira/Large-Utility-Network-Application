using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Angle.Interfaces;
using Angle.Models.ViewModels.ProductHistoryViewModels;
using LUNA.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Angle.Controllers.Ivy
{
    public class ProductHistoryController : Controller
    {
        public IUnityOfWork _unityOfWork;
        public ProjectDataContext _db;
        public ProductHistoryController(IUnityOfWork unityOfWork, ProjectDataContext db)
        {
            _unityOfWork = unityOfWork;
            _db = db;
        }
        public ActionResult Index()
        {
            List<ProductHistoryViewModelSimple> allProductHistories = new List<ProductHistoryViewModelSimple>();
            ViewBag.HistoryType = _unityOfWork.History.GetAll();
            var products = _unityOfWork.Product.getAllProductsHistory();
            foreach (var product in products)
            {
                foreach (var producthistory in product.ProductHistories)
                {
                    ProductHistoryViewModelSimple history = new ProductHistoryViewModelSimple()
                    {
                        History = producthistory,
                        CreatorEmail = _db.Users.FirstOrDefault(x => x.Id == producthistory.UserID).FirstName + " " + _db.Users.FirstOrDefault(x => x.Id == producthistory.UserID).LastName
                    };
                    allProductHistories.Add(history);
                }
            }
            return View(allProductHistories);
        }
        public ActionResult Details(int id)
        {
            ViewBag.HistoryType = _unityOfWork.History.GetAll();
            var product = _unityOfWork.Product.getProductWithHistory(id);
            List<ProductHistoryViewModelDetail> productHistories = new List<ProductHistoryViewModelDetail>();
            foreach (var productHistory in product.ProductHistories)
            {
                ProductHistoryViewModelDetail history = new ProductHistoryViewModelDetail()
                {
                    History = productHistory,
                    UserName = _db.Users.FirstOrDefault(x => x.Id == productHistory.UserID).FirstName + " " + _db.Users.FirstOrDefault(x => x.Id == productHistory.UserID).LastName
                };
                productHistories.Add(history);
            }
            ViewBag.ProductDescription = product.Description;
            ViewBag.SerialNumber = product.SerialNumber;
            ViewBag.ProductID = product.ID;
            return View(productHistories);
        }
        public ActionResult TimeLine(long id)
        {
            var product = _unityOfWork.Product.getProductWithHistory(id);
            List<ProductHistoryViewModelDetail> productHistories = new List<ProductHistoryViewModelDetail>();
            foreach (var productHistory in product.ProductHistories)
            {
                ProductHistoryViewModelDetail history = new ProductHistoryViewModelDetail()
                {
                    History = productHistory,
                    Date = productHistory.Date.ToString("dd/MM/yyyy"),
                    UserName = _db.Users.FirstOrDefault(x => x.Id == productHistory.UserID).FirstName + " " + _db.Users.FirstOrDefault(x => x.Id == productHistory.UserID).LastName
                };
                productHistories.Add(history);
            }
            ViewBag.ProductDescription = product.Description;
            ViewBag.SerialNumber = product.SerialNumber;
            ViewBag.ProductID = product.ID;
            return View(productHistories);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Edit(CreateProductHistoryViewModel model)
        {
            var productHistoryToUpdate = new ProductHistory();
            ProductHistory newProductHistory = new ProductHistory()
            {
                ProductID = model.ProductID,
                HistoryID = model.HistoryID,
                Comment = model.Comment,
                Date = model.DateTimeNow,
                UserID = model.Email
            };
            var product = _unityOfWork.Product.getProductWithHistory(model.ProductID);
            long id = product.ID;
            // find history entry in list of histories
            foreach (var productHistory in product.ProductHistories)
            {
                // a unique history entry is given by ProductID, HistoryID, Date
                if (productHistory.Date.ToString() == model.DateTimeNow.ToString() && model.OldHistoryID == productHistory.HistoryID && model.Email == productHistory.UserID)
                {
                    productHistoryToUpdate = productHistory;
                    break;
                }
            }
            product.ProductHistories.Remove(productHistoryToUpdate);
            product.ProductHistories.Add(newProductHistory);
            _unityOfWork.Product.Update(product);
            _unityOfWork.Save();
            newProductHistory.User = null;
            newProductHistory.UserID = null;
            LoggingController.writeLog(newProductHistory, User.Identity.Name, this.ControllerContext.RouteData.Values["action"].ToString(), this.ControllerContext.RouteData.Values["controller"].ToString());
            return RedirectToAction("Details", new { id = id });
        }
        [Authorize(Roles = "Admin, Administrator")]
        public ActionResult Delete(int productId, string userId, DateTime date, int historyId)
        {
            var productHistoryToDelete = new ProductHistory();
            var product = _unityOfWork.Product.getProductWithHistory(productId);
            int id = productId;
            foreach (var productHistory in product.ProductHistories)
            {
                if (date.ToString() == productHistory.Date.ToString() && historyId == productHistory.HistoryID && userId == productHistory.UserID)
                {
                    productHistoryToDelete = productHistory;
                    break;
                }
            }
            product.ProductHistories.Remove(productHistoryToDelete);
            _unityOfWork.Product.Update(product);
            _unityOfWork.Save();
            productHistoryToDelete.User = null;
            productHistoryToDelete.UserID = null;
            LoggingController.writeLog(productHistoryToDelete, User.Identity.Name, this.ControllerContext.RouteData.Values["action"].ToString(), this.ControllerContext.RouteData.Values["controller"].ToString());
            return RedirectToAction("Details", new { id = id });
        }
    }
}