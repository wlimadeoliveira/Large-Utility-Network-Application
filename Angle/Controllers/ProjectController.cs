using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LUNA.Models.Models;
using LUNA.Models;
using Angle.Interfaces;
using Angle.Models.ViewModels.ProjectViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Angle.Controllers
{
    [Authorize]
    public class ProjectController: Controller
    {
        private readonly IUnityOfWork _unityOfWork;
        private readonly ProjectDataContext _db;
        public ProjectController(IUnityOfWork unityOfWork, ProjectDataContext db)
        {
            _unityOfWork = unityOfWork;
            _db = db;
        }

        public IActionResult Index()
        {

            var projects = _unityOfWork.Project.GetAll();
            
            return View(projects);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var customerList =_unityOfWork.Customer.GetAll();
            ViewBag.ListOfCustomers = customerList;
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProjectModel project)
        {
            _unityOfWork.Project.Insert(project);
            _unityOfWork.Save();
            LoggingController.writeLog(project, User.Identity.Name, this.ControllerContext.RouteData.Values["action"].ToString(), this.ControllerContext.RouteData.Values["controller"].ToString());

            return RedirectToAction("Index");   
        }

        public IActionResult Edit(long Id)
        {
            var customerList = _unityOfWork.Customer.GetAll();
            ViewBag.ListOfCustomers = customerList;
            var project = _unityOfWork.Project.GetById(Id); 
            return View(project);
        }
        [HttpPost]
        public IActionResult Edit(ProjectModel project)
        {
            _unityOfWork.Project.Update(project);
            _unityOfWork.Save();
            LoggingController.writeLog(project, User.Identity.Name, this.ControllerContext.RouteData.Values["action"].ToString(), this.ControllerContext.RouteData.Values["controller"].ToString());

            return RedirectToAction("Index");
        }

        public IActionResult Delete(long Id)
        {
            var project = _unityOfWork.Project.GetById(Id);
            DeleteProjectViewModel projectToDelete = new DeleteProjectViewModel()
            {
                ID = project.ID,
                customer = project.Customer,
                Description = project.Description,
                Title = project.Title
            };
            return View(projectToDelete);
        }
        [HttpPost]
        public IActionResult Delete(DeleteProjectViewModel project)
        {
            ProjectModel projectToDelete = new ProjectModel()
            {
                ID = project.ID,
                CustomerID = project.CustomerID,
                Description = project.Description,
                Title = project.Title
            };
            if (project.decisionControll)
            {
                _unityOfWork.Project.Delete(projectToDelete);
                _unityOfWork.Save();
            }
            LoggingController.writeLog(projectToDelete, User.Identity.Name, this.ControllerContext.RouteData.Values["action"].ToString(), this.ControllerContext.RouteData.Values["controller"].ToString());

            return RedirectToAction("Index");
        }






    }
}