using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Dal
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public decimal DiscountPrice { get; set; }

        public int Stock { get; set; }

        public int Barcode { get; set; }

        public int CategoryId { get; set; }

        public bool IsDeleted { get; set; }

        public virtual Category Category { get; set; }

        public virtual ProductFeature ProductFeature { get; set; }

        /*
        Microsoft.EntityFrameworkCore.Proxies
        Virtual KeyWord is for lazy loading. To enable it you should use the nuget mentioned above
        */

    }
}
