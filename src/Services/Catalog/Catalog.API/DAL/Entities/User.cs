using System.Collections.Generic;

namespace Catalog.API.DAL.Entities
{
    public class User : EntityBase
    {
        public string UserName { get; set; }
        public ICollection<ProductRating> Ratings { get; set; }
    }
}
