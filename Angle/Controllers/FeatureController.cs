using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Angle.Interfaces;
using Angle.Models.ViewModels.FeatureViewModels;
using AutoMapper;
using LUNA.Models;
using LUNA.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Angle.Controllers
{
    [Authorize]
    public class FeatureController : Controller
    {
        
        private readonly IUnityOfWork _unityOfWork;
        private readonly IMapper _mapper;

        public FeatureController(IUnityOfWork unityOfWork, IMapper mapper)
        {
            _unityOfWork = unityOfWork;
            _mapper = mapper;
        }

        public IActionResult Index()
        {

            var features = _unityOfWork.Feature.GetAll();





            return View(features);
        }

        [HttpGet]
        public IActionResult Create()
        {
            

         

            return View();
        }
        [HttpPost]
        public IActionResult Create(Feature feature)
        {
            _unityOfWork.Feature.Insert(feature);
            _unityOfWork.Save();
            return RedirectToAction("Index");
 
        }



        public IActionResult Edit(long id)
        {

            var feature = _unityOfWork.Feature.GetById(id);

            return View(feature);
        }

        [HttpPost]
        public IActionResult Edit(Feature feature)
        {

            _unityOfWork.Feature.Update(feature);
            _unityOfWork.Save();
            return RedirectToAction("Index");

        }

        public IActionResult Delete(long id)
        {

            var feature = _unityOfWork.Feature.GetFeatureForDeleteById(id);
            var featureToDelete = new DeleteFeatureViewModel()
            {
                ID = feature.ID,
                Description = feature.Description,
                Name = feature.Name,
                
            };
            foreach(var type in feature.TypeFeature)
            {
                featureToDelete.typeNames.Add(_unityOfWork.Type.GetById(type.DeviceTypeID).Name);

            }




            return View(featureToDelete);
        }

        [HttpPost]
        public IActionResult Delete(DeleteFeatureViewModel feature)
        {
            if (feature.decisionControll)
            {
                var featureToDelete = _unityOfWork.Feature.GetById(feature.ID);
                _unityOfWork.Feature.Delete(featureToDelete);
                _unityOfWork.Save();
            }
            return RedirectToAction("Index");

        }




    }
}