using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LUNA.Models.Models;
using LUNA.Models;
using Angle.Interfaces;
using AutoMapper;
using Angle.Models.ViewModels.TypeViewModels;
using Attribute = LUNA.Models.Models.Attribute;
using Angle.Models.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Angle.Models.ViewModels.AttributeViewModel;
using Microsoft.EntityFrameworkCore;

namespace Angle.Controllers
{
    [Authorize]
    public class TypeController : Controller
    {

        private readonly IUnityOfWork _unityOfWork;
        private readonly IMapper _mapper;
        private readonly ProjectDataContext _db;
        public TypeController(IUnityOfWork unityOfWork, IMapper mapper, ProjectDataContext db)
        {
            _unityOfWork = unityOfWork;
            _mapper = mapper;
            _db = db;
        }

        public IActionResult  Index()
        {
            
             var model = _unityOfWork.Type.GetAll();
            var vm = _mapper.Map<List<TypeViewModel>>(model);
            PType type =_unityOfWork.Type.getChilds(15);

            return View(vm);
        }



        


        [HttpGet]
        public IActionResult Create()
        {

            var typeList = _unityOfWork.Type.GetAll();
            var attributeList = _unityOfWork.Attribute.GetAll();
            var featureList = _unityOfWork.Feature.GetAll();
            ViewBag.ListOfAttribute = attributeList;
            ViewBag.ListOfType = typeList;
            ViewBag.ListOfFeature = featureList;


            return View();
        }
        [HttpPost]
        public IActionResult Create(TypeCreateViewModel type)
        {

            int arrIndex = 0;

            PType myType = new PType()
            {
                Name = type.Name,
                Description = type.Description,

            };
            if (type.SelectedFeatures != null)
            {
                foreach (long feature in type.SelectedFeatures)
                {
                    myType.TypeFeatures.Add(new TypeFeature()
                    {
                        FeatureID = feature
                    });
                }
            }
            if (type.SelectedAttributes != null)
            {
                foreach (string item in type.SelectedAttributes)
                {

                    Attribute attr = _unityOfWork.Attribute.GetByName(item);
                    myType.TypeAttributes.Add(new TypeAttribute()
                    {
                        AttributeID = attr.ID,
                        Value = type.ValueSelectedAttributes[arrIndex]
                    }
                    ); ;
                    arrIndex++;

                }
            }


            List<long> existingChilds = new List<long>();

            if (type.TypeChild != null)
            {
                foreach (long child in type.TypeChild)
                {
                    if (!existingChilds.Contains(child))
                    {
                        myType.Childs.Add(new TypeChild()
                        {
                            ChildID = child,
                            Quantity = type.TypeChild.Where(p => p == child).Count()
                        }); ;
                        existingChilds.Add(child);
                    }
                }
            }


            _unityOfWork.Type.Insert(myType);
            _unityOfWork.Save();
            LoggingController.writeLog(myType, User.Identity.Name, this.ControllerContext.RouteData.Values["action"].ToString(), this.ControllerContext.RouteData.Values["controller"].ToString());


            return RedirectToAction("Index");
   

        }


   
        public IActionResult Edit(string id)
        {
            PType typeToEdit = _unityOfWork.Type.GetById(Convert.ToInt32(id));
            var typeList = _unityOfWork.Type.GetAll();
            var attributeList = _unityOfWork.Attribute.GetAll();
            var featureList = _unityOfWork.Feature.GetAll();
            ViewBag.ListOfAttribute = attributeList;
            ViewBag.ListOfType = typeList;
            ViewBag.ListOfFeature = featureList;
            TypeCreateViewModel editType = new TypeCreateViewModel()
            {
                ID = typeToEdit.ID,
                Description = typeToEdit.Description,
                Name = typeToEdit.Name
            };
            List<long> featuresID = new List<long>();
            List<string> attributesID = new List<string>();
            List<long> childsID = new List<long>();
            foreach (var feature in typeToEdit.TypeFeatures)
            {
                featuresID.Add(feature.FeatureID);
            }
            editType.SelectedFeatures = featuresID.ToArray();
           
           

            foreach (var attribute in typeToEdit.TypeAttributes)
            {
                attributesID.Add(attribute.AttributeID.ToString());
            }
            editType.SelectedAttributes = attributesID.ToArray();

            List<PType> childs = new List<PType>();
            foreach (var child in typeToEdit.Childs)
            {
                for (var i = 0; i < child.Quantity; i++)
                {
                    childsID.Add(child.ChildID);
                }
            }
            editType.TypeChild = childsID.ToArray();

            ViewBag.selectedChilds = Newtonsoft.Json.JsonConvert.SerializeObject(editType.TypeChild, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            ViewBag.selectedAttributes = Newtonsoft.Json.JsonConvert.SerializeObject(typeToEdit.TypeAttributes, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            ViewBag.selectedFeatures = Newtonsoft.Json.JsonConvert.SerializeObject(editType.SelectedFeatures, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            ViewBag.selectedAttributesArray = Newtonsoft.Json.JsonConvert.SerializeObject(editType.SelectedAttributes, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });



            return View(editType);

        }

        [HttpPost]
        public IActionResult Edit(TypeCreateViewModel type)
        {

            int arrIndex = 0;

            PType myType = _unityOfWork.Type.GetById(type.ID);

            myType.ID = type.ID;
            myType.Name = type.Name;
            myType.Description = type.Description;

            
            if (type.SelectedFeatures != null)
            {
                myType.TypeFeatures.Clear();
                foreach (long feature in type.SelectedFeatures)
                {                 
                    myType.TypeFeatures.Add(new TypeFeature()
                    {
                        FeatureID = feature
                    });
                }
            }

            


            if (type.SelectedAttributes != null)
            {
                myType.TypeAttributes = new List<TypeAttribute>();

                foreach (string item in type.SelectedAttributes)
                {                      
                        myType.TypeAttributes.Add(new TypeAttribute()
                        {
                            DeviceTypeID = type.ID,
                            AttributeID = _unityOfWork.Attribute.GetByName(item).ID,
                            Value = type.ValueSelectedAttributes[arrIndex]
                        }
                        );;
                        arrIndex++;     
                }           
            }

            

            List<long> existingChilds = new List<long>();

            if (type.TypeChild != null)
            {
                myType.Childs.Clear();
                foreach(var childID in type.TypeChild)
                {
                    int quantity = type.TypeChild.Where(b => b == childID).Count();
                    TypeChild model = new TypeChild()
                    {
                        TypeID = myType.ID,
                        ChildID = childID,
                        Quantity = quantity
                    };
                    type.TypeChild = type.TypeChild.Where(b => b != childID).ToArray();
                    myType.Childs.Add(model);
                }                     
            }
            _unityOfWork.Type.Update(myType);
            _unityOfWork.Save();            

            // This part Update also Product Attribute of Existing Products
            var products = _unityOfWork.Product.getProductsByType(myType.ID);          
            foreach (var product in products)
            {
                List<long> existingAttributes = new List<long>();            
                foreach (var p in product.ProductAttributes)
                {
                    existingAttributes.Add(p.AttributeID);
                }   
            
                foreach(var name in type.SelectedAttributes)
                {
                    if (!existingAttributes.Contains(_unityOfWork.Attribute.GetByName(name).ID))
                    {
                        ProductAttribute pa = new ProductAttribute
                        {
                            ProductID = product.ID,
                            AttributeID = _unityOfWork.Attribute.GetByName(name).ID,
                        };
                        product.ProductAttributes.Add(pa);
                    }

                }
                _unityOfWork.Product.Update(product);
                existingAttributes.Clear();
            }
            _unityOfWork.Save();
            LoggingController.writeLog(myType, User.Identity.Name, this.ControllerContext.RouteData.Values["action"].ToString(), this.ControllerContext.RouteData.Values["controller"].ToString());
            return RedirectToAction("Index");
        }




        public IActionResult Clone(string id)
        {
            PType typeToClone = _unityOfWork.Type.GetById(Convert.ToInt32(id));
            var typeList = _unityOfWork.Type.GetAll();
            var attributeList = _unityOfWork.Attribute.GetAll();
            var featureList = _unityOfWork.Feature.GetAll();
            ViewBag.ListOfAttribute = attributeList;
            ViewBag.ListOfType = typeList;
            ViewBag.ListOfFeature = featureList;
            TypeCreateViewModel newType = new TypeCreateViewModel()
            {
                Description = typeToClone.Description,
                Name = typeToClone.Name
            };
            List<long> featuresID = new List<long>();
            List<string> attributesID = new List<string>();
            List<long> childsID = new List<long>();
            foreach (var feature in typeToClone.TypeFeatures)
            {
                featuresID.Add(feature.FeatureID);
            }
            newType.SelectedFeatures = featuresID.ToArray();
            foreach (var attribute in typeToClone.TypeAttributes)
            {
                attributesID.Add(attribute.AttributeID.ToString());
            }
            newType.SelectedAttributes = attributesID.ToArray();
            foreach (var child in typeToClone.Childs)
            {
                childsID.Add(child.ChildID);
            }

            ViewBag.selectedChilds = Newtonsoft.Json.JsonConvert.SerializeObject(newType.TypeChild, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            ViewBag.selectedAttributes = Newtonsoft.Json.JsonConvert.SerializeObject(typeToClone.TypeAttributes, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            ViewBag.selectedFeatures = Newtonsoft.Json.JsonConvert.SerializeObject(newType.SelectedFeatures, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            ViewBag.selectedAttributesArray = Newtonsoft.Json.JsonConvert.SerializeObject(newType.SelectedAttributes, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });



            return View(newType);

        }

        [HttpPost]
        public IActionResult Clone(TypeCreateViewModel type)
        {

            int arrIndex = 0;

            PType myType = new PType()
            {
                Name = type.Name,
                Description = type.Description,

            };
            if (type.SelectedFeatures != null)
            {
                foreach (long feature in type.SelectedFeatures)
                {
                    myType.TypeFeatures.Add(new TypeFeature()
                    {
                        FeatureID = feature
                    });
                }
            }
            if (type.SelectedAttributes != null)
            {
                foreach (string item in type.SelectedAttributes)
                {


                    myType.TypeAttributes.Add(new TypeAttribute()
                    {
                       
                        AttributeID = _unityOfWork.Attribute.GetByName(item).ID,
                        Value = type.ValueSelectedAttributes[arrIndex]
                    }
                    ); ; ; ;
                    arrIndex++;

                }
            }


            List<long> existingChilds = new List<long>();

            if (type.TypeChild != null)
            {
                foreach (long child in type.TypeChild)
                {
                    if (!existingChilds.Contains(child))
                    {
                        myType.Childs.Add(new TypeChild()
                        {
                            ChildID = child,
                            Quantity = type.TypeChild.Where(p => p == child).Count()
                        }); ;
                        existingChilds.Add(child);
                    }
                }
            }


            _unityOfWork.Type.Insert(myType);
            _unityOfWork.Save();

            LoggingController.writeLog(myType, User.Identity.Name, this.ControllerContext.RouteData.Values["action"].ToString(), this.ControllerContext.RouteData.Values["controller"].ToString());



            return RedirectToAction("Index");

        }


      
        public IActionResult Delete(string id)
        {                 
            var type = _unityOfWork.Type.GetById(Convert.ToInt32(id));
            TypeDeleteViewModel typeToDelete = new TypeDeleteViewModel()
            {
                ID = type.ID,
                Name = type.Name,
                Description = type.Description
            };
            typeToDelete.Products = _unityOfWork.Product.getProductsByType(Convert.ToInt32(id));
            return View(typeToDelete);

            

        }
        [HttpPost]
        public IActionResult Delete(TypeDeleteViewModel typeToDelete)
        {
            var myTypeToDelete = _unityOfWork.Type.GetById(typeToDelete.ID);
            typeToDelete.Products = _unityOfWork.Product.getProductsByType(Convert.ToInt32(typeToDelete.ID));

            var deleteAllProducts = typeToDelete.deleteProducts;
            if (deleteAllProducts)
            {
                foreach(var product in typeToDelete.Products)
                {
                    _unityOfWork.Product.Delete(product);
                }
            }
            _unityOfWork.Type.Delete(myTypeToDelete);

            _unityOfWork.Save();
            LoggingController.writeLog(myTypeToDelete, User.Identity.Name, this.ControllerContext.RouteData.Values["action"].ToString(), this.ControllerContext.RouteData.Values["controller"].ToString());

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> NameExist(string name)
        {
            var validateName = await _db.PType.FirstOrDefaultAsync
                                (x => x.Name == name);

            if (validateName == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Type name: { name} is already in use");
            }
        }




        //return TypeAttribute and Values
        [HttpGet]
        public JsonResult GetTypeAttribute(int Id){

            var type = _unityOfWork.Type.GetById(Id);

            List<AttributeViewModel> attributes = new List<AttributeViewModel>(); 

            foreach(var attribute in type.TypeAttributes) 
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
    }
}


    
