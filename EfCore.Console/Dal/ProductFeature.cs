using System.ComponentModel.DataAnnotations.Schema;

namespace EfCore.Console.Dal
{
    public class ProductFeature
    {
        [ForeignKey("Product")]
        public int Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Color { get; set; }
        public virtual Product Product { get; set; }
    }
}
