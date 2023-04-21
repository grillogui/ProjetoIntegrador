using Microsoft.EntityFrameworkCore;
using ProjectEcommerce.src.models;

namespace ProjectEcommerce.src.data
{
    public class ProjectEcommerceContext : DbContext 
    {
            public DbSet<UserModel> Users { get; set; }

            public DbSet<ProductModel> Products { get; set; }

            public DbSet<PurchaseModel> Purchases { get; set; }

            public ProjectEcommerceContext(DbContextOptions<ProjectEcommerceContext> opt) : base(opt)
            {
            }
      
    }
}
