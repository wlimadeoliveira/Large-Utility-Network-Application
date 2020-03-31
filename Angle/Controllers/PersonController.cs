using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LUNA.Models.Models;
using LUNA.Models;
using Angle.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Angle.Controllers
{
    [Authorize]
    public class PersonController: Controller
    {
     
        private readonly IUnityOfWork _unityOfWork;
        public PersonController(IUnityOfWork unityOfWork)
        {      
            _unityOfWork = unityOfWork;
        }

        public IActionResult Index()
        {
            var persons = _unityOfWork.Person.GetAll();                         
            return View(persons);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var customerList = _unityOfWork.Customer.GetAll();
            ViewBag.ListOfCustomers = customerList;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Person person)
        {
            _unityOfWork.Person.Insert(person);
            _unityOfWork.Save();
           return RedirectToAction("Index");       
        }
        public IActionResult Edit(long id)
        {
            var person = _unityOfWork.Person.GetById(id);
            var customerList = _unityOfWork.Customer.GetAll();
            ViewBag.ListOfCustomers = customerList;
            return View(person);
        }
        [HttpPost]
        public IActionResult Edit(Person person)
        {
            _unityOfWork.Person.Update(person);
            _unityOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(long id)
        {
            var person = _unityOfWork.Person.GetById(id);
       
           
            return View(person);
        }
        [HttpPost]
        public IActionResult Delete(Person person)
        {
            _unityOfWork.Person.Delete(person);
            _unityOfWork.Save();
            return RedirectToAction("Index");
        }





    }
}