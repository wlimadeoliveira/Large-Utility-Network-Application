using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Angle.Interfaces;
using Angle.Models.ViewModels.CustomerViewModels;
using LUNA.Models;
using LUNA.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Angle.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly IUnityOfWork _unityOfWork;

        public CustomerController(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
            
        }

        public IActionResult Index()
        {
            var customers = _unityOfWork.Customer.GetAll();
            return View(customers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            _unityOfWork.Customer.Insert(customer);
            _unityOfWork.Save();
            return RedirectToAction("Index");      
        }
       
        public IActionResult Edit(long Id)
        {
            var customerToEdit = _unityOfWork.Customer.GetById(Id);
            return View(customerToEdit);           
        }
        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            _unityOfWork.Customer.Update(customer);
            _unityOfWork.Save();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(long Id)
        {
            var customerToDelete = _unityOfWork.Customer.GetById(Id);
            var relationatedProducts = _unityOfWork.Product.getProductByCustomers(Id);
            var myCustomer = new DeleteCustomerViewModel()
            {
                ID = customerToDelete.ID,
                AddressOffice = customerToDelete.AddressOffice,
                AddressBill = customerToDelete.AddressBill,
                PhoneNumber = customerToDelete.PhoneNumber,
                products = relationatedProducts
            };
            return View(myCustomer);
        }
        [HttpPost]
        public IActionResult Delete(DeleteCustomerViewModel customerToDelete)
        {
            if (customerToDelete.decisionControll)
            {
                Customer customer = _unityOfWork.Customer.GetById(customerToDelete.ID);
                List<Product> relationatedProducts = _unityOfWork.Product.getProductByCustomers(customer.ID);
                foreach(var product in relationatedProducts)
                {
                    product.CustomerID = null;
                    _unityOfWork.Product.Update(product);        
                }
                _unityOfWork.Customer.Delete(customer);
                _unityOfWork.Save();
            }
            return RedirectToAction("Index");

        }







    }
}