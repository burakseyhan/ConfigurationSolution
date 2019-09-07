namespace Configuration.DAL.Entity
{
    public sealed class ConfigurationEntity : BaseEntity<int>
    {
        public int ApplicationId { get; set; }

        public string DataType { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }

        public bool ConfigurationState { get; set; }

        public bool IsNew { get; set; }

        public bool IsProcessed { get; set; }

        public ConfigurationEntity()
        {

        }
    }
}
