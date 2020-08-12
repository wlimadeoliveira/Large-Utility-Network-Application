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
using Angle.Helpers;
using Microsoft.AspNetCore.Html;
using Angle.Repositories;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Angle.Models.Models;
using System.Net.Http;

namespace Angle.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IUnityOfWork _unityOfWork;
        private readonly ProjectDataContext _db;
        private readonly IWebHostEnvironment _env;
        public ProductController(IUnityOfWork unityOfWork, ProjectDataContext db,  IWebHostEnvironment env )
        {
            _db = db;
            _unityOfWork = unityOfWork;
            _env = env;
        }

        public IActionResult Index()
        {
            ViewBag.HistoryType = _unityOfWork.History.GetAll();
            var products = _unityOfWork.Product.GetAll();
            var types = _unityOfWork.Type.GetAll();
            ViewBag.typesForSearch = new SelectList(types, "Name", "Name");           
            List<ProductViewModel> productsView = new List<ProductViewModel>();
            foreach (var product in products)
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

                if (model.ParentID != null)
                {
                    int parentID = Convert.ToInt32(model.ParentID);
                    model.ParentSerialNumber = _unityOfWork.Product.GetById(parentID).SerialNumber;
                }
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
            foreach (var child in selectedChilds)
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
            if (product.SelectedAttributes != null)
            {
                foreach (var attribute in product.SelectedAttributes)
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
            }




            _unityOfWork.Product.Update(myProduct);
            _unityOfWork.Save();
            //var childToExclude = _unityOfWork.Product.getChilds(myProduct.ID);
            var productChilds = _unityOfWork.Product.getChilds(myProduct.ID);
            foreach (var child in productChilds)
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
        public async Task<IActionResult> AddHistory(CreateProductHistoryViewModel model)
        {
            if (model.UploadFile != null)
            {
                var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

                if (model.UploadFile.Length > 0)

                {
                    
                    string fName = Path.GetFileNameWithoutExtension(model.UploadFile.FileName);
                    string fExt = Path.GetExtension(model.UploadFile.FileName);
                    string hashCode = RandomString(5);
                    string newUniqueFileName = String.Concat(fName, "_" + hashCode, fExt);

                    using (var fileStream = new FileStream(Path.Combine(uploads, newUniqueFileName), FileMode.Create))

                    {

                        await model.UploadFile.CopyToAsync(fileStream);

                    }
                    Upload myFile = new Upload()
                    {
                        RelativPath = Path.Combine(uploads, newUniqueFileName),
                        Size = model.UploadFile.Length.ToString(),
                        Format = model.UploadFile.ContentType.ToString(),
                    };
                    _unityOfWork.Upload.Insert(myFile);
                    _unityOfWork.Save();
                    model.UploadID = myFile.ID;

                }
            }
            Product product = _unityOfWork.Product.GetById(model.ProductID);
            ProductHistory productHistory = new ProductHistory
            {
                UserID = _db.Users.FirstOrDefault(x => x.Email == model.Email).Id,
                Comment = model.Comment,
                Date = DateTime.Now,
                ProductID = model.ProductID,
                HistoryID = model.HistoryID,
                FileID = model.UploadID 
            };
            product.ProductHistories.Add(productHistory);
            _unityOfWork.Product.Update(product);
            _unityOfWork.Save();
            productHistory.User = null;
            productHistory.UserID = null;
            LoggingController.writeLog(productHistory, User.Identity.Name, this.ControllerContext.RouteData.Values["action"].ToString(), this.ControllerContext.RouteData.Values["controller"].ToString());
           return  Redirect(Request.Headers["Referer"]);
        }

        [HttpPost]
        public async Task<JsonResult> QuickAddHistory(CreateProductHistoryViewModel model)
        {

            
            if (model.UploadFile != null)
            {
                var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");




                if (model.UploadFile.Length > 0)

                {


                    using (var fileStream = new FileStream(Path.Combine(uploads, model.UploadFile.FileName), FileMode.Create))

                    {

                        await model.UploadFile.CopyToAsync(fileStream);

                    }
                    Upload myFile = new Upload()
                    {
                        RelativPath = Path.Combine(uploads, model.UploadFile.FileName),
                        Size = model.UploadFile.Length.ToString(),
                        Format = model.UploadFile.ContentType.ToString(),
                    };
                    _unityOfWork.Upload.Insert(myFile);
                    _unityOfWork.Save();
                    model.UploadID = myFile.ID;

                }
            }
            


            try
            {
                Product product = _unityOfWork.Product.GetById(model.ProductID);
                ProductHistory productHistory = new ProductHistory
                {
                    UserID = _db.Users.FirstOrDefault(x => x.Email == model.Email).Id,
                    Comment = model.Comment,
                    Date = DateTime.Now,
                    ProductID = model.ProductID,
                    HistoryID = model.HistoryID,
                    FileID = model.UploadID
                };



                product.ProductHistories.Add(productHistory);
                _unityOfWork.Product.Update(product);
                _unityOfWork.Save();
                productHistory.User = null;
                productHistory.UserID = null;
                LoggingController.writeLog(productHistory, User.Identity.Name, this.ControllerContext.RouteData.Values["action"].ToString(), this.ControllerContext.RouteData.Values["controller"].ToString());









                return Json(productHistory);
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
            foreach (var child in childs)
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
        public async Task<IActionResult> SerialNumberExist(string serialNumber, long typeID)
        {
         
            var validateName = await _db.Product.FirstOrDefaultAsync
                                (x => x.SerialNumber == serialNumber && x.Type.ID == Convert.ToInt32(typeID) );

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
            histories.Sort((x, y) => DateTime.Compare(y.History.Date, x.History.Date));
            ViewBag.histories = histories;
            ViewBag.HistoryType = _unityOfWork.History.GetAll();
            
            var QRCode = _unityOfWork.Index_QR.GetByProductId(id);

            List<Product> parents = new List<Product>();
            parents = _unityOfWork.Product.getParents(product, parents);

            
          


            if (QRCode != null)
            {
                var host = Request.Host;
                ViewBag.QrCode = QRHelper.GenerateQRCode("http://" + host.Host + Url.Action("Redirect", "Index_QR", new { id = QRCode.Id }));
            }
            else
            {
                ViewBag.QrCode = new HtmlString("<a class='float-right text-center' href=" + Url.Action("Create", "Index_QR", new { productID = id, controllerName = "Product", actionName = "Wizard" }) + "> Generate QR Code" + "<img src='/images/addqr.png' height='150' width='150'></img>" + "</a>");
            }

            ViewBag.Files = product.ProductHistories.Where(b => b.FileID != null).ToList();

            var products1 = _unityOfWork.Product.getChilds(id);
            return View(product);
        }


        /// <summary>
        /// Is called when an QR Code is scanned in an mobile device
        /// It prepares All Information of an specifict product and pass it to the Wizard View
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns>Product Wizard View with specified product data</returns>
        public IActionResult Wizard(long id)
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
            histories.Sort((x, y) => DateTime.Compare(x.History.Date,y.History.Date ));
            
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
            histories.Sort((x, y) => DateTime.Compare(y.History.Date, x.History.Date));
            ViewBag.histories = histories;
            return PartialView("_HistoryTable");
        }

        public PartialViewResult TimeLine(long id)
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
            histories.Sort((x, y) => DateTime.Compare(y.History.Date, x.History.Date));
            ViewBag.histories = histories;
            return PartialView("TimeLine");
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


        [HttpPost]

        public async Task<IActionResult> Upload([FromForm]FileUploadViewModel upload)

        {

         

            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<FileContentResult> GetMyFile(string filename, string mimetype)
        {
            var filepath = Path.Combine($"{this._env.WebRootPath}/uploads/{filename}");
            try
            {
                
            var path = filepath;
            byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(filepath);
           
                return new FileContentResult(fileBytes, mimetype)
                {
                    FileDownloadName = filename
                };
            }
            catch
            {
                return new FileContentResult(System.Text.Encoding.Unicode.GetBytes(filepath), "text/plain")  {
                    FileDownloadName = filename
                };
            }

            /*
            byte[] fileBytes = null;

            if (System.IO.File.Exists(filepath))
            {
                fileBytes = System.IO.File.ReadAllBytes(filepath);
            }
            else
            {
                return null;
            }

            return new FileContentResult(fileBytes, mimetype)
            {
                FileDownloadName = filename
            };*/



        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }




    }
}