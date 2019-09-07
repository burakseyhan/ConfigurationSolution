using System;

namespace Configuration.DAL.Entity
{
    public class BaseEntity<T>
    {
        public T Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get { return DateTime.UtcNow; } }
        
        public bool IsActive { get; set; }
    }
}
