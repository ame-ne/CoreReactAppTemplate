using System.Collections.Generic;

namespace CoreReactApp.Domain.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<IncidentCategory> IncidentCategories { get; set; }
    }
}
