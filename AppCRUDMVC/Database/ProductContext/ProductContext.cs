using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AppCRUDMVC.Database.ProductContext
{
    public class ProductContext : DbContext
    {
        //Sección INDICO que ENTIDADES deben CONVERTIRSE en TABLAS
        public DbSet<ProductEntity> Products { get; set; }

        public ProductContext(DbContextOptions<ProductContext> option) : base(option)
        {
        }
        public int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
