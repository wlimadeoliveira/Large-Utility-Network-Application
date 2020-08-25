using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Angle.Interfaces;
using Angle.Models.Models;
using Angle.Models.ViewModels;
using Angle.Models.ViewModels.AttributeViewModel;
using AutoMapper;
using LUNA.Models;
using LUNA.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Angle.Controllers
{
    [Authorize]
    public class SoftwareController : Controller
    {
       
        private readonly IUnityOfWork _unityOfWork;

   
        ProjectDataContext _db;

        public SoftwareController(ProjectDataContext db)
        {
            _db = db;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var model = _db.SoftwareTypes.ToList();
       
            return View(model);
        }

        [HttpGet]
        public IActionResult CreateType()
        {

            ViewBag.ListOfOption = _db.SoftwareOptions.ToList();

            return View();
        }
        [HttpPost]
        public IActionResult CreateType(SoftwareTypeViewModel vm)
        {

            

            

                SoftwareType model = new SoftwareType()
                {
                    Description = vm.Description,
                    Name = vm.Name,
                };
                _db.SoftwareTypes.Add(model);
                _db.SaveChanges();
                int counter = 0;
                List<SoftwareTypeOptions> softwareTypeOptions = new List<SoftwareTypeOptions>();
                foreach(var option in vm.hiddenID)
                {
                    SoftwareTypeOptions softwareTypeOption = new SoftwareTypeOptions()
                    {
                        SoftwareOptionID = Convert.ToInt32(option),
                        SoftwareTypeID = model.ID,
                        Value = vm.Values[counter],
                    };

                    _db.Add(softwareTypeOption);
                    counter++;
                }
            _db.SaveChanges();


                return RedirectToAction("Index", "Attribute");           
            
           
            //_db.Attribute.Add(attribute);
            //_db.SaveChanges();
            //return RedirectToAction("Index");
            //return View("Index");
        }

        [HttpGet]
        public IActionResult CreateOption()
        {
            

            return View();
        }
        [HttpPost]
        public IActionResult CreateOption(SoftwareOption option)
        {
            try
            {
                _db.SoftwareOptions.Add(option);

                _db.SaveChanges();
                return View();

                //return RedirectToAction("Index", "Attribute");
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
                return Json(/*hier Model*/null);
            }
            catch
            {
                return Json(null);
            }
        
        }



        [HttpGet]
       
        public IActionResult Edit(long id)
        {
           
            return View();
        }

        [HttpPost]
    
        public IActionResult Edit(AttributeViewModel vm)
        {
            try
            {
                
                return RedirectToAction("Index","Attribute");
            }
            catch
            {
                return View();
            }

        }



        public IActionResult Delete(long id)
        {
           
            
           
            
            return View();
        }


        [HttpPost]
        public IActionResult Delete(DeleteAttributeViewModel vm)
        {

       

            if (vm.smartControll && vm.decisionControll)
            {
                
                return RedirectToAction("Index", "Attribute");
            }
            else
            {
                return RedirectToAction("Index", "Attribute");
            }

            
         

        }







    }
}