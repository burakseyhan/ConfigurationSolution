using Configuration.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ConfigurationUI.ViewModel
{
    public class ApplicationViewModel : BaseEntity<int>
    {

        public DateTime RenderedCreatedTime { get; set; }

        public ApplicationViewModel()
        {

        }

        public ApplicationViewModel(ApplicationEntity entity)
        {
            this.Id = entity.Id;
            this.Name = entity.Name;
            this.Description = entity.Description;
            this.RenderedCreatedTime = entity.CreatedDate;
            this.IsActive = entity.IsActive;
        }
    }
}