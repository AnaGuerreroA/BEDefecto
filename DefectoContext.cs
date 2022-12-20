using BEDefecto.Models;
using Microsoft.EntityFrameworkCore;

namespace BEDefecto
{
    public class DefectoContext: DbContext
    {
        public DbSet<Auth> Auths { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Image> Images { get; set; }

        
        public DefectoContext(DbContextOptions<DefectoContext> options): base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            List<Category> categories = new List<Category>();
            categories.Add(new Category() { CategoryId = 1, Name = "Categoria 1" });
            categories.Add(new Category() { CategoryId = 2, Name = "Categoria 2" });
            categories.Add(new Category() { CategoryId = 3, Name = "Categoria 3" });

            modelBuilder.Entity<Category>(categoria => {
                categoria.ToTable("Categories");
                categoria.HasKey(c => c.CategoryId);    
                categoria.Property(c => c.CategoryId).ValueGeneratedOnAdd();
                categoria.Property(c => c.Name).IsRequired().HasMaxLength(150);        
                categoria.HasData(categories);
            
            });    

            List<Image> images = new List<Image>();
            images.Add(new Image() { ImageId = 1, ImageName = "https://placeimg.com/640/480/any?random=${Math.random()}", ProductId = 1 });
            images.Add(new Image() { ImageId = 2, ImageName = "https://placeimg.com/640/480/any?random=${Math.random()}", ProductId = 2 }); 
            images.Add(new Image() { ImageId = 3, ImageName = "https://placeimg.com/640/480/any?random=${Math.random()}", ProductId = 3 });

            modelBuilder.Entity<Image>(imagen => {
                imagen.ToTable("Images");
                imagen.HasKey(i => i.ImageId);
                imagen.Property(i => i.ImageName).IsRequired().HasMaxLength(150);
                imagen.HasData(images);
            });

            List<Product> productInit = new List<Product>();
            productInit.Add(new Product() { ProductId = 1, Title = "Producto 1", Price = 1000, Description = "Descripcion 1",  CategoryId = 1, Quantity = 0});
            productInit.Add(new Product() { ProductId = 2, Title = "Producto 2", Price = 2000, Description = "Descripcion 2",  CategoryId = 2, Quantity = 0});
            productInit.Add(new Product() { ProductId = 3, Title = "Producto 3", Price = 3000, Description = "Descripcion 3",  CategoryId = 3, Quantity = 0});


            modelBuilder.Entity<Product>( producto =>
            {
                producto.ToTable("Products");
                producto.HasKey(p => p.ProductId);
                producto.Property(p => p.ProductId).ValueGeneratedOnAdd();
                producto.HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryId);
                producto.Property(p => p.Title).IsRequired().HasMaxLength(150);
                producto.Property(p => p.Price).IsRequired();
                producto.Property(p => p.Quantity).IsRequired().HasDefaultValue(0);
                producto.Property(p => p.Description).IsRequired().HasMaxLength(150);
                producto.HasData(productInit);
            });

            

        }

    }
}