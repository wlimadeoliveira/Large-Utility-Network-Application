using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Angle.Interfaces;
using Angle.Models.ViewModels.AttributeViewModel;
using AutoMapper;
using LUNA.Models;
using LUNA.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Attribute = LUNA.Models.Models.Attribute;

namespace Angle.Controllers
{
    [Authorize]
    public class AttributeController : Controller
    {
       
        private readonly IUnityOfWork _unityOfWork;
        private IMapper _mapper;

        public AttributeController(IUnityOfWork unityOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unityOfWork = unityOfWork;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var model = _unityOfWork.Attribute.GetAll();
            var vm = _mapper.Map<List<AttributeViewModel>>(model);
            //var attributes = _db.Attribute.ToArray();





            return View(vm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            

         

            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateAttributeViewModel vm)
        {
            try
            {
                var model = _mapper.Map<Attribute>(vm);
                _unityOfWork.Attribute.Insert(model);
                _unityOfWork.Save();
                
                return RedirectToAction("Index", "Attribute");           
            }
            catch
            {
                return View();
            }
            //_db.Attribute.Add(attribute);
            //_db.SaveChanges();
            //return RedirectToAction("Index");
            //return View("Index");
        }

        [HttpPost]
        public JsonResult CreateQuick(CreateAttributeViewModel vm)
        {
            try
            {
                var model = _mapper.Map<Attribute>(vm);
                _unityOfWork.Attribute.Insert(model);
                _unityOfWork.Save();
                return Json(model);
            }
            catch
            {
                return Json(null);
            }
        
        }



        [HttpGet]
       
        public IActionResult Edit(long id)
        {
            var model = _unityOfWork.Attribute.GetById(id);
            AttributeViewModel attribute = new AttributeViewModel() { Name = model.Name, ID = model.ID };
            return View(attribute);
        }

        [HttpPost]
    
        public IActionResult Edit(AttributeViewModel vm)
        {
            try
            {
                var model = _mapper.Map<Attribute>(vm);
                _unityOfWork.Attribute.Update(model);
                _unityOfWork.Save();
                return RedirectToAction("Index","Attribute");
            }
            catch
            {
                return View();
            }

        }



        public IActionResult Delete(long id)
        {
            var attributeToDelete = _unityOfWork.Attribute.GetAttributeForDelete(id);
            var deleteAttributes = new DeleteAttributeViewModel()
            {
                ID = id,
                Name = attributeToDelete.Name,
            };
            foreach(var type in attributeToDelete.TypeAttributes)
            {
                deleteAttributes.containingTypes.Add(_unityOfWork.Type.GetById(type.DeviceTypeID).Name);
            }
            
           
            
            return View(deleteAttributes);
        }


        [HttpPost]
        public IActionResult Delete(DeleteAttributeViewModel vm)
        {

            var attributeToDelete = _unityOfWork.Attribute.GetAttributeForDelete(vm.ID);

            if (vm.smartControll && vm.decisionControll)
            {
                _unityOfWork.Attribute.Delete(attributeToDelete);
                _unityOfWork.Save();
                return RedirectToAction("Index", "Attribute");
            }
            else
            {
                return RedirectToAction("Index", "Attribute");
            }

            
         

        }







    }
}