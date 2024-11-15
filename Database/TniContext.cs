using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using tni_back.Entities;
using tni_back.Entities.Common;
using tni_back.Entities.Constants;

namespace tni_back.Database
{
    public class TniContext : DbContext
    {
        public DbSet<MasterProducts> MasterProducts { get; set; }
        public DbSet<StockProduct> StockProducts { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<UserCart> UserCarts { get; set; }
        public TniContext(DbContextOptions options) : base(options)
        {
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                entry.Entity.UpdatedTime = DateTime.Now;
                if (entry.State == EntityState.Added)
                {
                    if (entry.Entity.Id == Guid.Empty) entry.Entity.Id = Guid.NewGuid();
                    entry.Entity.IsActive = ConstantsEntity.ACTIVE;
                    entry.Entity.CreatedTime = DateTime.Now;
                    entry.Entity.IsDeleted = ConstantsEntity.NOT_DELETED;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        public async Task SeedDatabase()
        {
            MasterProducts.RemoveRange(MasterProducts);
            Users.RemoveRange(Users);
            StockProducts.RemoveRange(StockProducts);
            UserCarts.RemoveRange(UserCarts);
            
            MasterProducts coffee = new MasterProducts
            {
                Id = Guid.Parse("a0212959-2084-492b-9dbb-f41ebe2be79f"),
                Name = "Orange Juice",
                Price = 250,
                Quantity = 97,
                ImgUrl = "https://images.unsplash.com/photo-1646753522408-077ef9839300?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwcm9maWxlLXBhZ2V8NjZ8fHxlbnwwfHx8fA%3D%3D&auto=format&fit=crop&w=500&q=60"

            };
            MasterProducts cookie = new MasterProducts
            {
                Id = Guid.Parse("1397bebc-b457-4fff-b0ef-887ea0493510"),
                Name = "Lemonade",
                Price = 100,
                Quantity = 20,
                ImgUrl = "https://images.unsplash.com/photo-1651950519238-15835722f8bb?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwcm9maWxlLXBhZ2V8Mjh8fHxlbnwwfHx8fA%3D%3D&auto=format&fit=crop&w=500&q=60"

            };
            MasterProducts cake = new MasterProducts
            {
                Id = Guid.Parse("653f0934-d110-4492-a844-19dee61dcdd2"),
                Name = "Coconut Water",
                Price = 230,
                Quantity = 2,
                ImgUrl = "https://images.unsplash.com/photo-1651950537598-373e4358d320?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwcm9maWxlLXBhZ2V8MjV8fHxlbnwwfHx8fA%3D%3D&auto=format&fit=crop&w=500&q=60"
            };

            Users users = new Users
            {
                Id = Guid.Parse("d60db691-7e40-4d72-ad45-ccccd5905404"),
                UserName = "Nopdanai"
            };

            await Users.AddAsync(users);
            await MasterProducts.AddRangeAsync(new List<MasterProducts> { coffee, cookie, cake });
            await SaveChangesAsync();


        }
    }
}