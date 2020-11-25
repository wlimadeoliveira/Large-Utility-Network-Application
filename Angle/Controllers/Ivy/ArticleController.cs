using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Angle.Interfaces;
using Angle.Models.Models.Ivy;
using Angle.Models.ViewModels.Ivy;
using LUNA.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Angle.Controllers.Ivy
{
    [Authorize]
    public class ArticleController : Controller
    {

        private readonly IUnityOfWork _unityOfWork;
        private readonly ProjectDataContext _db;
        public ArticleController(IUnityOfWork unityOfWork, ProjectDataContext db) 
        {
            _unityOfWork = unityOfWork;
            _db = db;
        } 

        // GET: Article
        public ActionResult Index()
        {
            var articles = _unityOfWork.Article.GetAll();
            return View("~/Views/Ivy/Article/Index.cshtml", articles);
        }

        // GET: Article/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Article/Create
        public ActionResult Create()
        {

            ViewBag.ListOfManufacturers = _db.Manufacturer.ToList();
            ViewBag.ListOfSuppliers = _db.Supplier.ToList();
            ViewBag.ListOfStatus = _db.Status.ToList();
            ViewBag.ListOfCategories = _db.Category.ToList();
            ViewBag.ListOfLocations = _db.Location.ToList();

            return View("~/Views/Ivy/Article/Create.cshtml");
        }

        // POST: Article/Create
        [HttpPost]
        public ActionResult Create(ArticleViewModel article)
        {
           
                List<Article> categoryArticles = _db.Article.Where(b => b.CategoryID == article.CategoryID).ToList();



            //Watches for missing ID inside of an Category, and set an new ID for this new Article
            HashSet<long> myRange = new HashSet<long>(Enumerable.Range(Convert.ToInt32(categoryArticles[0].ID), Convert.ToInt32(categoryArticles[categoryArticles.Count -1].ID + 1)).Select(i => (long)i));
            
            
             myRange.ExceptWith(categoryArticles.Select(b=>b.ID));

            var newID = myRange.First();



                Article model = new Article()
                {
                    ID = newID,
                    CategoryID = article.CategoryID,
                    Description = article.Description,
                    LocationID = article.LocationID,
                    ManufacturerID = article.ManufacturerID,
                    Status = article.Status,
                    SupplierID = article.SupplierID,
                    Quantity = article.Quantity,
                    Created =DateTime.Now,
                    ManufacturerPartNumber = article.ManufacturerPartNumber,
                    SupplierPartNumber = article.SupplierPartNumber,
                    Pricing = article.Princing,
                    Updated = DateTime.Now
                   
                };


                _unityOfWork.Article.Insert(model);
            _unityOfWork.Save();
                return RedirectToAction(nameof(Index));
            
                
        }

        // GET: Article/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Article/Edit/5
        [HttpPost]
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

        // GET: Article/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Article/Delete/5
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