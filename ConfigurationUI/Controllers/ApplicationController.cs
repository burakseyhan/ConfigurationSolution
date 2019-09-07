using Configuration.BL.Repository;
using Configuration.DAL.Entity;
using ConfigurationUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConfigurationUI.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly IBaseRepository<ApplicationEntity> repository;

        public ApplicationController(IBaseRepository<ApplicationEntity> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var viewModelList = new List<ApplicationViewModel>();

            var resultList = this.repository.GetItems();

            if (resultList.IsSuccess)
            {
                foreach (var item in resultList.Operation)
                {
                    var viewModel = new ApplicationViewModel(item);

                    viewModelList.Add(viewModel);
                }
            }

            return View(viewModelList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ApplicationViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                else
                {
                    ApplicationEntity entity = new ApplicationEntity();

                    entity.Name = viewModel.Name;
                    entity.Description = viewModel.Description;
                    entity.IsActive = false;

                    OperationResult<bool> result = this.repository.Save(entity);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            OperationResult<ApplicationEntity> result = this.repository.GetOne(id);

            ApplicationViewModel viewModel = new ApplicationViewModel(result.Operation);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(int id, ApplicationViewModel viewModel)
        {
            try
            {   
                if (!ModelState.IsValid)
                {
                    return View();
                }
                else
                {
                    var entity = new ApplicationEntity();

                    entity.Id = viewModel.Id;
                    entity.Name = viewModel.Name;
                    entity.Description = viewModel.Description;
                    entity.IsActive = viewModel.IsActive;

                    this.repository.Edit(id, entity);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            OperationResult<ApplicationEntity> result = this.repository.GetOne(id);
            ApplicationViewModel viewModel = null;

            if (result.IsSuccess)
            {
                viewModel = new ApplicationViewModel(result.Operation);
            }

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                this.repository.Delete(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
