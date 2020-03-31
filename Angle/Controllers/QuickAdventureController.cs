using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Angle.Interfaces;
using Angle.Models.Models;
using Angle.Models.ViewModels;
using Angle.Models.ViewModels.AccountViewModel;
using Angle.Models.ViewModels.ProductHistoryViewModels;
using LUNA.Models;
using LUNA.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Angle.Controllers
{
    public class QuickAdventureController : Controller
    {
        IUnityOfWork _unityOfWork;
        ProjectDataContext _db;

        public QuickAdventureController(IUnityOfWork unityOfWork, ProjectDataContext db )
        {
            _unityOfWork = unityOfWork;
            _db = db;

        }

        public IActionResult Index()
        {
           
           
         
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
         
            List<QuickAdventure> myAdventures = _db.QuickAdventure.Include(b=> b.Product).Include("Product.Type").Where(b => b.UserID == userId).ToList();

       
 

            return View(myAdventures);
        }

        [HttpGet]
        public IActionResult Create(long id)
        {
            //Product product = _unityOfWork.Product.GetById(quickAdventure.ProductID);
            //ApplicationUser user = _db.Users.FirstOrDefault(x => x.Id == quickAdventure.UserID);
            QuickAdventureViewModel model = new QuickAdventureViewModel()
            {
                Product = _unityOfWork.Product.GetById(id)

            };


            return View(model);
        }

        [HttpPost]
        public IActionResult Create(QuickAdventureViewModel quickAdventure)
        {
            //Product product = _unityOfWork.Product.GetById(quickAdventure.ProductID);
            //ApplicationUser user = _db.Users.FirstOrDefault(x => x.Id == quickAdventure.UserID);
            QuickAdventure model = new QuickAdventure()
            {
                UserID = _db.Users.FirstOrDefault(x => x.Email == quickAdventure.Email).Id,
                ProductID = quickAdventure.ProductID,
                DateTime = DateTime.Now
                
            };
            _db.QuickAdventure.Add(model);
            _db.SaveChanges();
            return View("CreateSuccessful");
        }

        public PartialViewResult ProductDetail(long id)
        {
            var product = _unityOfWork.Product.GetByIdDetailed(id);
            List<Product> childs = _unityOfWork.Product.getChilds(id);
            ViewBag.childs = childs;
            List<ProductHistoryViewModelDetail> histories = new List<ProductHistoryViewModelDetail>();
            foreach (var productHistory in product.ProductHistories)
            {
                ProductHistoryViewModelDetail history = new ProductHistoryViewModelDetail()
                {
                    History = productHistory,
                    UserName = _db.Users.FirstOrDefault(x => x.Id == productHistory.UserID).FirstName + " " + _db.Users.FirstOrDefault(x => x.Id == productHistory.UserID).LastName
                };
                histories.Add(history);
            }
            ViewBag.histories = histories;
            ViewBag.HistoryType = _unityOfWork.History.GetAll();

            return PartialView("_ProductDetail", product);
        }

    }
}