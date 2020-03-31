using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LUNA.Models.Models;
using LUNA.Models;
using Angle.Interfaces;
using Angle.Models.ViewModels.ProductViewModel;
using ProductViewModel = LUNA.Models.Models.ProductViewModel;
using Attribute = LUNA.Models.Models.Attribute;
using Nancy.Json;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Angle.Models.ViewModels.ProductHistoryViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Angle.Models.ViewModels.AttributeViewModel;

namespace Angle.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IUnityOfWork _unityOfWork;
        private readonly ProjectDataContext _db;
        public ProductController(IUnityOfWork unityOfWork, ProjectDataContext db)
        {
            _db = db;
            _unityOfWork = unityOfWork;
        }

        public IActionResult Index()
        {
            ViewBag.HistoryType = _unityOfWork.History.GetAll();
            var products = _unityOfWork.Product.GetAll();
            List<ProductViewModel> productsView = new List<ProductViewModel>();
            foreach(var product in products)
            {
                ProductViewModel model = new ProductViewModel()
                {
                    SerialNumber = product.SerialNumber,
                    ProductDescription = product.Description,
                    ProductID = product.ID,
                    ProjectName = (product.Project?.Title ?? "").ToString(),
                    CustomerName = (product.Customer?.CompanyName ?? "").ToString(),
                    TypeName = (product.Type?.Name ?? "").ToString(),
                    ParentID = (product.Parent?.ID ?? null),
                    ParentSerialNumber = (product.Parent?.SerialNumber ?? "").ToString(),

                };
                productsView.Add(model);
            }
            return View(productsView);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var customerList = _db.Customer.ToArray();
            ViewBag.ListOfCustomers = customerList;
            var projectList = _db.Projects.ToArray();
            ViewBag.ListOfProjects = projectList;
            var stockInformationList = _db.StockInformation.ToArray();
            ViewBag.ListOfStockInformations = stockInformationList;
            var attributeList = _db.Attribute.ToArray();
            ViewBag.ListOfAttribute = attributeList;
            var typeLists = _unityOfWork.Type.GetAll().ToArray();
            ViewBag.ListOfType = typeLists;
            var typeList = _unityOfWork.Type.GetAll();
            var listOfAvailbeChildProducts = _unityOfWork.Product.getAvaibleProducts();
            var attributes = _unityOfWork.Attribute.GetAll();
            var listType = Newtonsoft.Json.JsonConvert.SerializeObject(typeList, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            var listAvaible = Newtonsoft.Json.JsonConvert.SerializeObject(listOfAvailbeChildProducts, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            ViewBag.ListOfChilds = listType;
            ViewBag.ListOfAvaible = listAvaible;
            
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductCreateViewModel product)
        {
            List<Attribute> attributes = new List<Attribute>();
            int arrIndex = 0;
            ProductHistory history = new ProductHistory()
            {
                UserID = _db.Users.FirstOrDefault(x => x.Email == product.Email).Id,
                HistoryID = _unityOfWork.History.GetByName("Created"),
                Date = DateTime.Now,
                Comment = "Created"
            };
            Product myProduct = new Product()
            {
                CustomerID = product.CustomerID,
                Description = product.Description,
                ProjectID = product.ProjectID,
                SerialNumber = product.SerialNumber,
                DeviceTypeID = product.DeviceTypeID,
                StockInformationID = product.StockInformationID,
            };
            if (product.SelectedAttributes != null)
            {
                foreach (string item in product.SelectedAttributes)
                {
                    Attribute attr = _unityOfWork.Attribute.GetById(Convert.ToInt32(item));
                    myProduct.ProductAttributes.Add(new ProductAttribute()
                    {
                        AttributeID = attr.ID,
                        Value = product.ValueSelectedAttributes[arrIndex]
                    }
                    ); ;
                    arrIndex++;
                }
            }
           
                myProduct.ProductHistories.Add(history);
                _unityOfWork.Product.Insert(myProduct);
                _unityOfWork.Save();

                try
                {
                    foreach (string productChild in product.TypeChild)
                    {
                        var childProduct = _unityOfWork.Product.GetById(Convert.ToInt32(productChild));
                        childProduct.ParentID = myProduct.ID;
                        _unityOfWork.Product.Update(childProduct);
                        _unityOfWork.Save();
                    }
                }
                catch { }
                LoggingController.writeLog(product, User.Identity.Name, this.ControllerContext.RouteData.Values["action"].ToString(), this.ControllerContext.RouteData.Values["controller"].ToString());
                return RedirectToAction("Index");
            }
   
        

        public IActionResult Edit(long id)
        {
            var product = _unityOfWork.Product.GetByIdWithAttributes(id);
            ProductCreateViewModel myproduct = new ProductCreateViewModel()
            {
                ID = product.ID,
                SerialNumber = product.SerialNumber,
                Description = product.Description,
                CustomerID = product.CustomerID,
                ProjectID = product.ProjectID,
                StockInformationID = product.StockInformationID,
                DeviceTypeID = product.DeviceTypeID,
                Type = _unityOfWork.Type.GetById(product.DeviceTypeID),
                ParentID = product.ParentID
            };
            List<ProductAttribute> myAttributes = product.ProductAttributes;
            List<Product> childs = _unityOfWork.Product.getChilds(myproduct.ID);
            List<string> childsID = new List<string>();
            foreach (var child in childs)
            {
                childsID.Add(child.ID.ToString());
            }
        
            var customerList = _db.Customer.ToArray();
            ViewBag.ListOfCustomers = customerList;
            var projectList = _db.Projects.ToArray();
            ViewBag.ListOfProjects = projectList;
            var stockInformationList = _db.StockInformation.ToArray();
            ViewBag.ListOfStockInformations = stockInformationList;
            var typeLists = _unityOfWork.Type.GetAll().ToArray();
            ViewBag.ListOfType = typeLists;
            var mychilds = _unityOfWork.Product.getChilds(id);
            IEnumerable<SelectListItem> selectList = from s in mychilds
                                                     select new SelectListItem
                                                     {
                                                         Value = s.ID.ToString(),
                                                         Text = s.SerialNumber + " - " + s.Type.Name,
                                                         Selected = true
                                                     };
            SelectList selectedChilds = new SelectList(selectList, "Value", "Text");
            foreach(var child in selectedChilds)
            {
                child.Selected = true;
            }
            ViewBag.myChilds = selectedChilds;
                   
            return View(myproduct);
        }

        [HttpPost]
        public IActionResult Edit(ProductCreateViewModel product)
        {
            LoggingController.writeLog(product, User.Identity.Name, this.ControllerContext.RouteData.Values["action"].ToString(), this.ControllerContext.RouteData.Values["controller"].ToString());
            List<Attribute> attributes = new List<Attribute>();
            int arrIndex = 0;
            Product myProduct = new Product()
            {
                ID = product.ID,
                CustomerID = product.CustomerID,
                Description = product.Description,
                ProjectID = product.ProjectID,
                SerialNumber = product.SerialNumber,
                DeviceTypeID = product.DeviceTypeID,
                StockInformationID = product.StockInformationID,
                ParentID = product.ParentID,
            };
            myProduct.ProductAttributes.Clear();
            foreach(var attribute in product.SelectedAttributes)
            {
                ProductAttribute model = new ProductAttribute
                {
                    AttributeID = Convert.ToInt32(attribute),
                    Value = product.ValueSelectedAttributes[arrIndex],
                    ProductID = product.ID                
                };
                arrIndex++;
                myProduct.ProductAttributes.Add(model);
            }
            



            _unityOfWork.Product.Update(myProduct);
            _unityOfWork.Save();
            //var childToExclude = _unityOfWork.Product.getChilds(myProduct.ID);
            var productChilds = _unityOfWork.Product.getChilds(myProduct.ID);
            foreach(var child in productChilds)
            {
                child.ParentID = null;
                _unityOfWork.Product.Update(child);
            }
            _unityOfWork.Save();

            try
            {
                foreach (string productChild in product.TypeChild)
                {
                    var childProduct = _unityOfWork.Product.GetById(Convert.ToInt32(productChild));
                    childProduct.ParentID = myProduct.ID;
                    _unityOfWork.Product.Update(childProduct);
                }
                _unityOfWork.Save();
            }
            catch { }
            LoggingController.writeLog(product, User.Identity.Name, this.ControllerContext.RouteData.Values["action"].ToString(), this.ControllerContext.RouteData.Values["controller"].ToString());

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            Product product = _unityOfWork.Product.GetByIdForDelete(id);
          Angle.Models.ViewModels.ProductViewModel.ProductViewModel productToDelete = new Angle.Models.ViewModels.ProductViewModel.ProductViewModel()
            {
                ID = product.ID,
                Description = product.Description,              
                SerialNumber = product.SerialNumber,
            };
            var childs = _unityOfWork.Product.getChilds(product.ID);
            List<string> myChilds = new List<string>();
            foreach (var child in childs)
            {
                myChilds.Add(child.Description);
            }
            productToDelete.TypeChild = myChilds.ToArray();
            if (product.Parent != null)
            {
                productToDelete.ParentID = product.ParentID;
                productToDelete.ParentName = product.Parent.Description;
            }

            LoggingController.writeLog(productToDelete, User.Identity.Name, this.ControllerContext.RouteData.Values["action"].ToString(), this.ControllerContext.RouteData.Values["controller"].ToString());


            return View(productToDelete);
        }

        [HttpPost]
        public IActionResult Delete(Angle.Models.ViewModels.ProductViewModel.ProductViewModel productToDelete)
        {
            Product product = _unityOfWork.Product.GetById(productToDelete.ID);
            List<Product> Childs = _unityOfWork.Product.getChilds(product.ID);
            _unityOfWork.Product.Delete(product);
            _unityOfWork.Save();
            LoggingController.writeLog(product, User.Identity.Name, this.ControllerContext.RouteData.Values["action"].ToString(), this.ControllerContext.RouteData.Values["controller"].ToString());
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult AddHistory(CreateProductHistoryViewModel model)
        {
            Product product = _unityOfWork.Product.GetById(model.ProductID);
            ProductHistory productHistory = new ProductHistory
            {
                UserID = _db.Users.FirstOrDefault(x => x.Email == model.Email).Id,
                Comment = model.Comment,
                Date = DateTime.Now,
                ProductID = model.ProductID,
                HistoryID = model.HistoryID
            };
            product.ProductHistories.Add(productHistory);
            _unityOfWork.Product.Update(product);
            _unityOfWork.Save();
            productHistory.User = null;
            productHistory.UserID = null;
            LoggingController.writeLog(productHistory, User.Identity.Name, this.ControllerContext.RouteData.Values["action"].ToString(), this.ControllerContext.RouteData.Values["controller"].ToString());
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult QuickAddHistory(CreateProductHistoryViewModel model)
        {
            try
            {
                Product product = _unityOfWork.Product.GetById(model.ProductID);
                ProductHistory productHistory = new ProductHistory
                {
                    UserID = _db.Users.FirstOrDefault(x => x.Email == model.Email).Id,
                    Comment = model.Comment,
                    Date = DateTime.Now,
                    ProductID = model.ProductID,
                    HistoryID = model.HistoryID
                };
                product.ProductHistories.Add(productHistory);
                _unityOfWork.Product.Update(product);
                _unityOfWork.Save();
                productHistory.User = null;
                productHistory.UserID = null;
                LoggingController.writeLog(productHistory, User.Identity.Name, this.ControllerContext.RouteData.Values["action"].ToString(), this.ControllerContext.RouteData.Values["controller"].ToString());


                return Json(productHistory) ;
            }
            catch
            {
                return Json(null);
            }
        }








        public IActionResult Clone(long id)
        {
            var product = _unityOfWork.Product.GetByIdWithAttributes(id);
            ProductCreateViewModel myproduct = new ProductCreateViewModel()
            {
                ID = product.ID,
                SerialNumber = product.SerialNumber,
                Description = product.Description,
                CustomerID = product.CustomerID,
                ProjectID = product.ProjectID,
                StockInformationID = product.StockInformationID,
                DeviceTypeID = product.DeviceTypeID,
                Type = _unityOfWork.Type.GetById(product.DeviceTypeID),
                ParentID = product.ParentID
            };
            List<ProductAttribute> myAttributes = product.ProductAttributes;
            List<Product> childs = _unityOfWork.Product.getChilds(myproduct.ID);
            List<string> childsID = new List<string>();
            foreach (var child in childs)
            {
                childsID.Add(child.ID.ToString());
            }
            myproduct.TypeChild = childsID.ToArray();
            var customerList = _db.Customer.ToArray();
            ViewBag.ListOfCustomers = customerList;
            var projectList = _db.Projects.ToArray();
            ViewBag.ListOfProjects = projectList;
            var stockInformationList = _db.StockInformation.ToArray();
            ViewBag.ListOfStockInformations = stockInformationList;
            var attributeList = _db.Attribute.ToArray();
            ViewBag.ListOfAttribute = attributeList;
            var typeLists = _unityOfWork.Type.GetAll().ToArray();
            ViewBag.ListOfType = typeLists;
            var typeList = _unityOfWork.Type.GetAll().ToArray();
            var listOfAvailbeChildProducts = _unityOfWork.Product.getAvaibleProducts();
            ViewBag.SelectedChilds = myproduct.TypeChild;
            var listType = Newtonsoft.Json.JsonConvert.SerializeObject(typeList, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            var listAvaible = Newtonsoft.Json.JsonConvert.SerializeObject(listOfAvailbeChildProducts, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            ViewBag.ListOfChilds = listType;
            ViewBag.ListOfAvaible = listAvaible;
            ViewBag.SelectedChilds = Newtonsoft.Json.JsonConvert.SerializeObject(childs, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            ViewBag.MyAttributes = Newtonsoft.Json.JsonConvert.SerializeObject(myAttributes, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            return View(myproduct);
        }
        // Ajax - Json  USED IN Create - Edit - Clone Product
        //Take -->Type ID as Parameter<--Returns all Type who aren't in use for an specific Product
        [HttpGet]
        public JsonResult GetTypeChild(long Id)
        {           
            var childs = _unityOfWork.Type.GetById(Id).Childs;
            List<Product> productChilds = new List<Product>();
            var product = _unityOfWork.Product.getAvaibleProducts();
            foreach(var child in childs)
            {
                var pchilds = product.Where(b => b.DeviceTypeID == child.ChildID).ToList();
                foreach (var productchild in pchilds)
                {
                    productChilds.Add(productchild);
                }           
            }
            IEnumerable<SelectListItem> selectList = from s in productChilds
                                                     select new SelectListItem
                                                     {
                                                         Value = s.ID.ToString(),
                                                         Text = s.SerialNumber + " - " + s.Type.Name
                                                     };
            var childsList = new SelectList(selectList, "Value", "Text");
            

            return Json(childsList);
        }

        //Used by 'Jquery Validate Obstruhiv Framework' 
        //Checks in Create Product View if Serial Number is already in use
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> SerialNumberExist(string serialNumber)
        {
            var validateName = await _db.Product.FirstOrDefaultAsync
                                (x => x.SerialNumber == serialNumber);
                                
            if (validateName == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Serial Number: { serialNumber} already in use");
            }
        }

        //Takes Product ID an return Product Specific Attributes and it Values
        [HttpGet]
        public JsonResult GetProductAttribute(int Id)
        {
            var product = _unityOfWork.Product.GetByIdWithAttributes(Id);
            List<AttributeViewModel> attributes = new List<AttributeViewModel>();
            foreach (var attribute in product.ProductAttributes)
            {
                AttributeViewModel model = new AttributeViewModel()
                {
                    ID = attribute.AttributeID,
                    Name = _unityOfWork.Attribute.GetById(attribute.AttributeID).Name, //attribute.Attribute.Name,
                    Value = attribute.Value

                };
                attributes.Add(model);
            }
            return Json(attributes);
        }


        public IActionResult Detail(long id)
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

            return View(product);
        }

        public PartialViewResult HistoryTable(long id)
        {
            var product = _unityOfWork.Product.GetByIdDetailed(id);
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
            return PartialView("_HistoryTable");
        }

        [HttpGet]
        public JsonResult HistoryTableJson(long id)
        {
            var product = _unityOfWork.Product.GetByIdDetailed(id);
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
            return Json(histories);
        }

    }
}