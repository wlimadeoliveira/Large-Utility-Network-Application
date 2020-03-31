using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Angle.Interfaces;
using LUNA.Models;
using LUNA.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Angle.Controllers
{
    [Authorize]
    public class StockInformationController : Controller
    { 
        private readonly IUnityOfWork _unityOfWork;
        public StockInformationController(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        public IActionResult Index()
        {
            var stockInformations = _unityOfWork.StockInformation.GetAll();
            return View(stockInformations);
        }

        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(StockInformation stockInformation)
        {
            _unityOfWork.StockInformation.Insert(stockInformation);
            _unityOfWork.Save();
            return RedirectToAction("Index");         
        }
        public IActionResult Edit(long id)
        {
            var stockInformation = _unityOfWork.StockInformation.GetById(id);

            return View(stockInformation);
        }
        [HttpPost]
        public IActionResult Edit(StockInformation stockInformation)
        {
            _unityOfWork.StockInformation.Update(stockInformation);
            _unityOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(long id)
        {
            var stockInformation = _unityOfWork.StockInformation.GetById(id);
            return View(stockInformation);
        }
        [HttpPost]
        public IActionResult Delete(StockInformation stockInformation)
        {
            _unityOfWork.StockInformation.Delete(stockInformation);
            _unityOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}