using Configuration.BL.Repository;
using Configuration.DAL.Entity;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ConfigurationUI.UIHelper
{
    public class CustomUIHelper
    {
        private IBaseRepository<ApplicationEntity> repository;

        public CustomUIHelper()
        {

        }

        public CustomUIHelper(IBaseRepository<ApplicationEntity> repository)
        {
            this.repository = repository;
        }

        public List<SelectListItem> GetProjects()
        {
            var viewModelList = new List<SelectListItem>();

            var resultList = repository.GetItems();

            if (resultList.IsSuccess && resultList.Operation.Count > 0)
            {
                foreach (var item in resultList.Operation)
                {
                    viewModelList.Add(new SelectListItem()
                    {
                        Text = item.Name,
                        Value = item.Id.ToString(),
                        Selected = false
                    });
                }
            }

            return viewModelList;
        }
    }
}