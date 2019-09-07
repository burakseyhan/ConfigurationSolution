using Configuration.BL.Repository;
using Configuration.DAL.Entity;
using ConfigurationUI.UIHelper;
using ConfigurationUI.ViewModel;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ConfigurationUI.Controllers
{
    public class ConfigurationController : Controller
    {
        private readonly IBaseRepository<ConfigurationEntity> configurationRepository;
        private readonly IBaseRepository<ApplicationEntity> applicationRepository;

        public ConfigurationController(IBaseRepository<ConfigurationEntity> configurationRepository, IBaseRepository<ApplicationEntity> applicationRepository)
        {
            this.configurationRepository = configurationRepository;
            this.applicationRepository = applicationRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var viewModelList = new List<ConfigurationViewModel>();

            var resultList = this.configurationRepository.GetItems();

            if (resultList.IsSuccess && resultList.Operation.Count > 0)
            {
                foreach (var item in resultList.Operation)
                {
                    var viewModel = new ConfigurationViewModel(item);

                    viewModelList.Add(viewModel);
                }
            }

            return View(viewModelList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            CustomUIHelper customUIHelper = new CustomUIHelper(this.applicationRepository);

            ViewBag.Projects = customUIHelper.GetProjects();

            return View();
        }

        [HttpPost]
        public ActionResult Create(ConfigurationViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                else
                {
                    ConfigurationEntity entity = new ConfigurationEntity();

                    entity.ApplicationId = viewModel.ApplicationId;
                    entity.DataType = viewModel.DataType;
                    entity.Value = viewModel.Value;
                    entity.Key = viewModel.Key;
                    entity.IsActive = false;
                    entity.IsNew = false;

                    OperationResult<bool> result = this.configurationRepository.Save(entity);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Configuration/Edit/5
        public ActionResult Edit(int id)
        {
            OperationResult<ConfigurationEntity> result = this.configurationRepository.GetOne(id);

            ConfigurationViewModel viewModel = new ConfigurationViewModel(result.Operation);

            return View(viewModel);
        }

        // POST: Configuration/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ConfigurationViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                else
                {
                    var entity = new ConfigurationEntity();
                    
                    entity.Id = viewModel.Id;
                    entity.ApplicationId = viewModel.ApplicationId;
                    entity.DataType = viewModel.DataType;
                    entity.IsActive = viewModel.IsActive;
                    entity.Key = viewModel.Key;
                    entity.Value = viewModel.Value;
                    entity.IsNew = viewModel.IsNew;
                    entity.IsProcessed = viewModel.IsProcessed;

                    this.configurationRepository.Edit(id, entity);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            OperationResult<ConfigurationEntity> result = this.configurationRepository.GetOne(id);

            ConfigurationViewModel viewModel = null;

            if (result.IsSuccess)
            {
                viewModel = new ConfigurationViewModel(result.Operation);
            }

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                this.configurationRepository.Delete(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
