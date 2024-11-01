using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class DbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public DbSet<PhotoPublication> PhotoPublications { get; set; }
    public DbSet<Publication> Publications { get; set; }
    // public DbSet<Review> Reviews { get; set; }
    public DbSet<UserQuestion> UserQuestions { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<PhotoPublication> PhotosPublication { get; set; }
    public DbSet<ProductState> ProductStates { get; set; }
    public DbSet<PublicationState> PublicationStates { get; set; }
    public DbSet<Color> Colors { get; set; }
    public DbSet<WishedArticle> WishedArticles { get; set; }
    public DbSet<UserPublication> UserPublication { get; set; }
    public DbSet<PublicationVisited> UserHistory { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<UserNotification> UserNotifications { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<CardType> CardTypes { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; } 
    public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; } 

    public DbContext(DbContextOptions<DbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(u => u.LastName).IsRequired().HasMaxLength(100);

            // Configuración de relaciones con otras tablas
            entity.HasMany(u => u.Publications)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);

            entity.HasMany(u => u.Cards)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId);

            entity.HasMany(u => u.WishedArticles)
                .WithOne(ad => ad.User)
                .HasForeignKey(ad => ad.Id);
        });

        // Configuración de Publication
        modelBuilder.Entity<Publication>(entity =>
        {
            entity.Property(p => p.Title).IsRequired().HasMaxLength(200);
            entity.Property(p => p.Description).HasMaxLength(500);
            entity.Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(p => p.Stock).IsRequired();
            entity.HasOne(p => p.ProductState)
                  .WithMany(ep => ep.Publications)
                  .HasForeignKey(p => p.IdProductState);
            entity.HasOne(p => p.PublicationState)
                  .WithMany(ep => ep.Publications)
                  .HasForeignKey(p => p.IdPublicationState);
            entity.HasOne(p => p.Category)
                  .WithMany(c => c.Publications)
                  .HasForeignKey(p => p.IdCategoria);
            entity.HasMany(p => p.UserQuestions)
                  .WithOne(pp => pp.Publication)
                  .HasForeignKey(pp => pp.IdPublication);
            entity.HasMany(p => p.PhotosPublication)
                  .WithOne(fp => fp.Publication)
                  .HasForeignKey(fp => fp.IdPublication);
        });

        modelBuilder.Entity<ShoppingCart>()
            .HasOne(cart => cart.User)
            .WithOne()
            .HasForeignKey<ShoppingCart>(cart => cart.IdUser)
            .OnDelete(DeleteBehavior.Restrict);

        // ShoppingCart - ShoppingCartItem (One-to-Many)
        modelBuilder.Entity<ShoppingCart>()
            .HasMany(cart => cart.Items)
            .WithOne()
            .HasForeignKey(item => item.ShoppingCartId)
            .OnDelete(DeleteBehavior.Cascade);

        // ShoppingCartItem - Publication (Many-to-One)
        modelBuilder.Entity<ShoppingCartItem>()
            .HasOne(item => item.Publication)
            .WithMany(publication => publication.ShoppingCartItems)
            .HasForeignKey(item => item.PublicationId)
            .OnDelete(DeleteBehavior.Restrict);

        
        // Otras configuraciones de entidades
        modelBuilder.Entity<Photo>(entity =>
        {
            entity.Property(f => f.ImageData).IsRequired();
        });

        modelBuilder.Entity<PhotoPublication>(entity =>
        {
            entity.HasKey(fp => new { fp.IdPublication, fp.IdPhoto });
            entity.HasOne(fp => fp.Photo)
                  .WithMany(f => f.PhotosPublication)
                  .HasForeignKey(fp => fp.IdPhoto);
        });

        modelBuilder.Entity<ProductState>(entity =>
        {
            entity.Property(ep => ep.Name).IsRequired().HasMaxLength(100);
            entity.Property(ep => ep.Description).HasMaxLength(200);
        });

        modelBuilder.Entity<PublicationState>(entity =>
        {
            entity.Property(ep => ep.Name).IsRequired().HasMaxLength(100);
            entity.Property(ep => ep.Description).HasMaxLength(200);
        });

        modelBuilder.Entity<Color>(entity =>
        {
            entity.Property(ep => ep.Name).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<WishedArticle>(entity =>
        {
            entity.HasOne(ad => ad.User)
                .WithMany(u => u.WishedArticles)
                .HasForeignKey(ad => ad.Id)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(ad => ad.Publication)
                .WithMany(p => p.WishedArticles)
                .HasForeignKey(ad => ad.IdPublication)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<UserPublication>(entity =>
        {
            entity.HasKey(upv => new { upv.Id, upv.IdPublication });
        });

        modelBuilder.Entity<PublicationVisited>(entity =>
        {
            entity.HasKey(upi => new { upi.Id, upi.IdPublication });
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.Property(n => n.Text).IsRequired().HasMaxLength(500);
        });

        modelBuilder.Entity<UserNotification>(entity =>
        {
            entity.HasKey(nu => new { nu.IdNotification, nu.Id });
            entity.HasOne(nu => nu.User)
                  .WithMany(u => u.UserNotifications)
                  .HasForeignKey(nu => nu.Id);
        });

        modelBuilder.Entity<Card>(entity =>
        {
            entity.Property(t => t.CardNumber).IsRequired().HasMaxLength(16);
            entity.Property(t => t.CardholderName).IsRequired().HasMaxLength(100);
            entity.Property(t => t.ExpirationDate).IsRequired().HasMaxLength(5);
            entity.HasOne(t => t.CardType)
                  .WithMany(tt => tt.Cards)
                  .HasForeignKey(t => t.CardTypeId);
        });

        modelBuilder.Entity<CardType>(entity =>
        {
            entity.Property(tt => tt.TypeName).IsRequired().HasMaxLength(100);
            entity.Property(tt => tt.Description).HasMaxLength(200);
        });

         modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(t => t.Id);

            // Relación con el comprador (Buyer)
            entity.HasOne(t => t.Buyer)
                .WithMany(u => u.Purchases) // Suponiendo que la clase User tiene una lista BuyerTransactions
                .HasForeignKey(t => t.IdBuyer)
                .OnDelete(DeleteBehavior.Restrict); // Evita eliminación en cascada

            // Relación con la publicación (Publication)
            entity.HasOne(t => t.Publication)
                .WithMany(p => p.Sales) // Suponiendo que la clase Publication tiene una lista Transactions
                .HasForeignKey(t => t.IdPublication)
                .OnDelete(DeleteBehavior.Restrict); // Evita eliminación en cascada
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
        });

         // Configurar la relación entre PhotoPublication y Photo
        modelBuilder.Entity<PhotoPublication>()
            .HasKey(pp => new { pp.IdPublication, pp.IdPhoto }); // Clave compuesta

        modelBuilder.Entity<PhotoPublication>()
            .HasOne(pp => pp.Photo)  // Cada PhotoPublication tiene una Photo
            .WithMany()               // Una Photo puede estar en muchas PhotoPublications
            .HasForeignKey(pp => pp.IdPhoto); // La clave foránea es IdPhoto

        modelBuilder.Entity<PhotoPublication>()
            .HasOne(pp => pp.Publication) // Cada PhotoPublication tiene una Publication
            .WithMany()                    // Una Publication puede tener muchas PhotoPublications
            .HasForeignKey(pp => pp.IdPublication); // La clave foránea es IdPublication
    }
}
