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
    public class ArticleController : Controller
    {

        private readonly IUnityOfWork _unityOfWork;
        public ArticleController(IUnityOfWork unityOfWork) 
        {
            _unityOfWork = unityOfWork;
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
            return View();
        }

        // POST: Article/Create
        [HttpPost]
        public ActionResult Create(Article article)
        {
            try
            {
                _unityOfWork.Article.Insert(article);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("~/Views/Ivy/Article/Create.cshtml");
            }
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