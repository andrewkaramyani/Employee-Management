using AspNet_Core.Models;
using AspNet_Core.Models.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet_Core.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IEmployeeReository _employeeReository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public HomeController(IEmployeeReository employeeReository,
                                IWebHostEnvironment webHostEnvironment)
        {
            _employeeReository = employeeReository;
            this.webHostEnvironment = webHostEnvironment;
        }
        [AllowAnonymous]
        public ViewResult Index()
        {
           //throw new Exception("develop exception");
            var model = _employeeReository.GetAllEmployees();
            return View(model);
        }
        [AllowAnonymous]
        public ViewResult Details(int? id)
        {
            Employee employee = _employeeReository.GetEmployeeById(id.Value);
            if (employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);
            }

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                employee = employee,
                PageTitle = "Employee Detils"
            }
            ;
            return View(homeDetailsViewModel);
        }
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel Model)
        {
            if (ModelState.IsValid)
            {
                string uniquefile = ProcessUploadFile(Model);
                Employee NewEmployee = new Employee
                {
                    Name = Model.Name,
                    Email = Model.Email,
                    Department = Model.Department,
                    PhotoPath = uniquefile,
                };
                _employeeReository.Add(NewEmployee);
                return RedirectToAction("details", new { id = NewEmployee.Id });
            }
            return View();
        }
        [HttpGet]
        public ViewResult Edit(int id)
        {
            Employee employee = _employeeReository.GetEmployeeById(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath
            };
            return View(employeeEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel Model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _employeeReository.GetEmployeeById(Model.Id);
                employee.Name = Model.Name;
                employee.Email = Model.Email;
                employee.Department = Model.Department;

                if (Model.Photo != null)
                {
                    if (Model.ExistingPhotoPath != null)
                    {
                        string filepate = Path.Combine(webHostEnvironment.WebRootPath, "images", Model.ExistingPhotoPath);
                        System.IO.File.Delete(filepate);
                    }
                    employee.PhotoPath = ProcessUploadFile(Model);
                }


                _employeeReository.Update(employee);
                return RedirectToAction("index");
            }
            return View();
        }

        private string ProcessUploadFile(EmployeeCreateViewModel Model)
        {
            string uniquefile = null;
            if (Model.Photo != null)
            {
                string uploadfolder = Path.Combine(webHostEnvironment.WebRootPath + "/images");
                uniquefile = Guid.NewGuid().ToString() + "_" + Model.Photo.FileName;
                string filepath = Path.Combine(uploadfolder, uniquefile);
                using (var filestream = new FileStream(filepath, FileMode.Create))
                {
                    Model.Photo.CopyTo(filestream);
                }

            }

            return uniquefile;
        }
    }
}
