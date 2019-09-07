using Configuration.BL.Repository;
using Configuration.DAL.Entity;
using ConfigurationUI.UIHelper;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ConfigurationUI.ViewModel
{
    public class ConfigurationViewModel
    {
        public int Id { get; set; }

        public int ApplicationId { get; set; }

        public string DataType { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }

        public DateTime CreatedDate { get; set; }

        public string ApplicationName { get; set; }

        public bool IsActive { get; set; }

        public bool IsProcessed { get; set; }

        public bool ConfigurationState { get; set; }

        public bool IsNew { get; set; }

        public ConfigurationViewModel()
        {

        }

        public ConfigurationViewModel(ConfigurationEntity entity)
        {
            this.Id = entity.Id;
            this.ApplicationId = entity.ApplicationId;
            this.ApplicationName = entity.Name;
            this.DataType = entity.DataType;
            this.Key = entity.Key;
            this.Value = entity.Value;
            this.CreatedDate = entity.CreatedDate;
            this.IsActive = entity.IsActive;
            this.ConfigurationState = entity.ConfigurationState;
            this.IsNew = entity.IsNew;
            this.IsProcessed = entity.IsProcessed;
        }
    }
}