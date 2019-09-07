using Configuration.BL.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ConfigurationUI.Helper
{
    public class ConfigHelper : IConfigHelper
    {
        public string ConfigurationServiceDb => ConfigurationManager.ConnectionStrings["ConfigurationServer"].ToString();
    }
}