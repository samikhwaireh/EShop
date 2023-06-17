using EShop.Domain.Entities.Cart;
using EShop.Domain.Entities.Order;
using EShop.Domain.Entities.ProductCatalog.Wishlists;
using EShop.Domain.Entities.ProductCatalog;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Data;

public class DataSeed
{
    public static async Task SeedAsync(ApplicationDbContext Context, ILoggerFactory loggerFactory, int? retry = 0)
    {
        int retryForAvailability = retry.Value;

        try
        {
            //Context.Database.Migrate();
            //Context.Database.EnsureCreated();

            await SeedCategoriesAsync(Context);
            await SeedReviewsAsync(Context);
            await SeedTagsAsync(Context);

            await SeedProductsAsync(Context);

            await SeedWishlistAndProductsAsync(Context);

            await SeedCartAndItemsAsync(Context);
            await SeedOrderAndItemsAsync(Context);

        }
        catch (Exception exception)
        {
            if (retryForAvailability < 10)
            {
                retryForAvailability++;
                var log = loggerFactory.CreateLogger<ApplicationDbContext>();
                log.LogError(exception.Message);
                await SeedAsync(Context, loggerFactory, retryForAvailability);
            }
            throw;
        }
    }

    private static async Task SeedCategoriesAsync(ApplicationDbContext Context)
    {
        if (Context.Categories.Any())
            return;

        var categories = new List<Category>()
            {
                new Category() { Name = "Laptop"},
                new Category() { Name = "Drone"}, 
                new Category() { Name = "TV & Audio"},
                new Category() { Name = "Phone & Tablet"},
                new Category() { Name = "Camera & Printer"}, 
                new Category() { Name = "Games"},
                new Category() { Name = "Accessories"},
                new Category() { Name = "Watch"}, 
                new Category() { Name = "Home & Kitchen Appliances"}
            };

        Context.Categories.AddRange(categories);
        await Context.SaveChangesAsync();
    }

    private static async Task SeedReviewsAsync(ApplicationDbContext Context)
    {
        if (Context.Reviews.Any())
            return;

        var reviews = new List<Review>()
            {
                new Review
                {
                    Name = "Cristopher Lee",
                    UserId = "cristopher@lee.com",
                    Rate = 4.3,
                    Context = "enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia res eos qui ratione voluptatem sequi Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci veli"
                },
                new Review
                {
                    Name = "Nirob Khan",
                    UserId = "nirob@lee.com",
                    Rate = 4.3,
                    Context = "enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia res eos qui ratione voluptatem sequi Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci veli"
                },
                new Review
                {
                    Name = "MD.ZENAUL ISLAM",
                    UserId = "zenaul@lee.com",
                    Rate = 4.3,
                    Context = "enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia res eos qui ratione voluptatem sequi Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci veli"
                }
            };

        Context.Reviews.AddRange(reviews);
        await Context.SaveChangesAsync();
    }

    private static async Task SeedTagsAsync(ApplicationDbContext Context)
    {
        if (Context.Tags.Any())
            return;

        var tags = new List<Tag>()
            {
                new Tag
                {
                    Name = "Electronic"
                },
                new Tag
                {
                    Name = "Smartphone"
                },
                new Tag
                {
                    Name = "Phone"
                },
                new Tag
                {
                    Name = "Charger"
                },
                new Tag
                {
                    Name = "Powerbank"
                }
            };

        Context.Tags.AddRange(tags);
        await Context.SaveChangesAsync();
    }

    private static async Task SeedProductsAsync(ApplicationDbContext Context)
    {
        if (Context.Products.Any())
            return;

        var products = new List<Product>
            {
                // Phone
                new Product()
                {
                    Name = "uPhone X",
                    Slug = "uphone-x",
                    Summary = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    CoverImage = "product-1.png",
                    UnitPrice = 295,
                    UnitsInStock = 10,
                    Rate = 4.3,
                    Category = Context.Categories.FirstOrDefault(c => c.Name == "Phone & Tablet"),
                    Reviews = Context.Reviews.ToList(),
                    Tags = Context.Tags.ToList()
                },
                new Product()
                {
                    Name = "Ornet Note 9",
                    Slug = "ornet-note",
                    Summary = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    CoverImage = "product-17.png",
                    UnitPrice = 285,
                    UnitsInStock = 10,
                    Rate = 4.3,
                    CategoryId = 4,
                    Reviews = Context.Reviews.ToList(),
                    Tags = Context.Tags.ToList()
                },
                new Product()
                {
                    Name = "Sany Experia Z5",
                    Slug = "sany-experia",
                    Summary = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    CoverImage = "product-24.png",
                    UnitPrice = 360,
                    UnitsInStock = 10,
                    Rate = 4.3,
                    CategoryId = 4,
                    Reviews = Context.Reviews.ToList(),
                    Tags = Context.Tags.ToList()
                },
                new Product()
                {
                    Name = "Flex P 3310",
                    Slug = "flex-p",
                    Summary = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    CoverImage = "product-19.png",
                    UnitPrice = 220,
                    UnitsInStock = 10,
                    Rate = 4.3,
                    CategoryId = 4,
                    Reviews = Context.Reviews.ToList(),
                    Tags = Context.Tags.ToList()
                },
                // Camera                
                new Product()
                {
                    Name = "Mony Handycam Z 105",
                    Slug = "mony-handycam-z",
                    Summary = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    CoverImage = "product-5.png",
                    UnitPrice = 145,
                    UnitsInStock = 10,
                    Rate = 4.3,
                    CategoryId = 5,
                    Reviews = Context.Reviews.ToList(),
                    Tags = Context.Tags.ToList()
                },
                new Product()
                {
                    Name = "Axor Digital Camera",
                    Slug = "axor-digital",
                    Summary = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    CoverImage = "product-6.png",
                    UnitPrice = 199,
                    UnitsInStock = 10,
                    Rate = 4.3,
                    CategoryId = 5,
                    Reviews = Context.Reviews.ToList(),
                    Tags = Context.Tags.ToList()
                },
                new Product()
                {
                    Name = "Silvex DSLR Camera T 32",
                    Slug = "silvex-camera",
                    Summary = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    CoverImage = "product-7.png",
                    UnitPrice = 580,
                    UnitsInStock = 10,
                    Rate = 4.3,
                    CategoryId = 5,
                    Reviews = Context.Reviews.ToList(),
                    Tags = Context.Tags.ToList()
                },
                new Product()
                {
                    Name = "Necta Instant Camera",
                    Slug = "necta-instant",
                    Summary = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    CoverImage = "product-8.png",
                    UnitPrice = 320,
                    UnitsInStock = 10,
                    Rate = 4.3,
                    CategoryId = 5,
                    Reviews = Context.Reviews.ToList(),
                    Tags = Context.Tags.ToList()
                },
                new Product()
                {
                    Name = "Pranon Photo Printer Y 25",
                    Slug = "pranon-printer",
                    Summary = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    CoverImage = "product-11.png",
                    UnitPrice = 210,
                    UnitsInStock = 10,
                    Rate = 4.3,
                    CategoryId = 5,
                    
                    Reviews = Context.Reviews.ToList(),
                    Tags = Context.Tags.ToList()
                },
                new Product()
                {
                    Name = "Game Station X 22",
                    Slug = "game-station-x",
                    Summary = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    CoverImage = "product-3.png",
                    UnitPrice = 295,
                    UnitsInStock = 10,
                    Rate = 4.3,
                    CategoryId = 6,
                    
                    Reviews = Context.Reviews.ToList(),
                    Tags = Context.Tags.ToList()
                },
                new Product()
                {
                    Name = "Game Stations PW 25",
                    Slug = "game-station-pw",
                    Summary = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    CoverImage = "product-13.png",
                    UnitPrice = 285,
                    UnitsInStock = 10,
                    Rate = 4.3,
                    CategoryId = 6,
                    
                    Reviews = Context.Reviews.ToList(),
                    Tags = Context.Tags.ToList()
                },      
                new Product()
                {
                    Name = "Zeon Zen 4 Pro",
                    Slug = "zeon-zen",
                    Summary = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    CoverImage = "product-1.png",
                    UnitPrice = 295,
                    UnitsInStock = 10,
                    Rate = 4.3,
                    CategoryId = 1,
                    
                    Reviews = Context.Reviews.ToList(),
                    Tags = Context.Tags.ToList()
                },
                new Product()
                {
                    Name = "Aquet Drone D 420",
                    Slug = "aquet-drone",
                    Summary = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    CoverImage = "product-2.png",
                    UnitPrice = 275,
                    UnitsInStock = 10,
                    Rate = 4.3,
                    CategoryId = 2,
                    
                    Reviews = Context.Reviews.ToList(),
                    Tags = Context.Tags.ToList()
                },
                new Product()
                {
                    Name = "Roxxe Headphone Z 75",
                    Slug = "roxxe-headphone-z",
                    Summary = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    CoverImage = "product-4.png",
                    UnitPrice = 110,
                    UnitsInStock = 10,
                    Rate = 4.3,
                    CategoryId = 7,
                    
                    Reviews = Context.Reviews.ToList(),
                    Tags = Context.Tags.ToList()
                },
                new Product()
                {
                    Name = "Mascut Smart Watch",
                    Slug = "mascut-smart",
                    Summary = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    CoverImage = "product-9.png",
                    UnitPrice = 365,
                    UnitsInStock = 10,
                    Rate = 4.3,
                    CategoryId = 8,
                    
                    Reviews = Context.Reviews.ToList(),
                    Tags = Context.Tags.ToList()
                },
                new Product()
                {
                    Name = "Z Bluetooth Headphone",
                    Slug = "z-bluetooth",
                    Summary = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    CoverImage = "product-10.png",
                    UnitPrice = 189,
                    UnitsInStock = 10,
                    Rate = 4.3,
                    CategoryId = 8,
                    
                    Reviews = Context.Reviews.ToList(),
                    Tags = Context.Tags.ToList()
                },
                new Product()
                {
                    Name = "Roses Speaker Box",
                    Slug = "roses-speaker",
                    Summary = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    CoverImage = "product-12.png",
                    UnitPrice = 210,
                    UnitsInStock = 10,
                    Rate = 4.3,
                    CategoryId = 3,
                    
                    Reviews = Context.Reviews.ToList(),
                    Tags = Context.Tags.ToList()
                },
                new Product()
                {
                    Name = "Nexo Andriod TV Box",
                    Slug = "nexo-andriod",
                    Summary = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    CoverImage = "product-16.png",
                    UnitPrice = 360,
                    UnitsInStock = 10,
                    Rate = 4.3,
                    CategoryId = 3,
                    
                    Reviews = Context.Reviews.ToList(),
                    Tags = Context.Tags.ToList()
                },
                new Product()
                {
                    Name = "Xonet Speaker P 9",
                    Slug = "xonet-speaker",
                    Summary = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    CoverImage = "product-18.png",
                    UnitPrice = 185,
                    UnitsInStock = 10,
                    Rate = 4.3,
                    CategoryId = 3,
                    
                    Reviews = Context.Reviews.ToList(),
                    Tags = Context.Tags.ToList()
                },
                new Product()
                {
                    Name = "Zorex Coffe Maker",
                    Slug = "zorex-coffe",
                    Summary = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    CoverImage = "product-14.png",
                    UnitPrice = 210,
                    UnitsInStock = 10,
                    Rate = 4.3,
                    CategoryId = 9,
                    
                    Reviews = Context.Reviews.ToList(),
                    Tags = Context.Tags.ToList()
                },
                new Product()
                {
                    Name = "Jeilips Steam Iron K 2",
                    Slug = "jeilips-steam",
                    Summary = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    CoverImage = "product-15.png",
                    UnitPrice = 365,
                    UnitsInStock = 10,
                    Rate = 4.3,
                    CategoryId = 9,
                    
                    Reviews = Context.Reviews.ToList(),
                    Tags = Context.Tags.ToList()
                },
                new Product()
                {
                    Name = "Jackson Toster V 27",
                    Slug = "jackson-toster",
                    Summary = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    CoverImage = "product-20.png",
                    UnitPrice = 185,
                    UnitsInStock = 10,
                    Rate = 4.3,
                    CategoryId = 9,
                    
                    Reviews = Context.Reviews.ToList(),
                    Tags = Context.Tags.ToList()
                },
                new Product()
                {
                    Name = "Mega Juice Maker",
                    Slug = "mega-juice",
                    Summary = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    CoverImage = "product-21.png",
                    UnitPrice = 185,
                    UnitsInStock = 10,
                    Rate = 4.3,
                    CategoryId = 9,
                    
                    Reviews = Context.Reviews.ToList(),
                    Tags = Context.Tags.ToList()
                },
                new Product()
                {
                    Name = "Shine Microwave Oven",
                    Slug = "shine-microwave",
                    Summary = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    CoverImage = "product-22.png",
                    UnitPrice = 185,
                    UnitsInStock = 10,
                    Rate = 4.3,
                    CategoryId = 9,
                    
                    Reviews = Context.Reviews.ToList(),
                    Tags = Context.Tags.ToList()
                },
                new Product()
                {
                    Name = "Auto Rice Cooker",
                    Slug = "auto-rice",
                    Summary = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    CoverImage = "product-23.png",
                    UnitPrice = 130,
                    UnitsInStock = 10,
                    Rate = 4.3,
                    CategoryId = 9,
                    
                    Reviews = Context.Reviews.ToList(),
                    Tags = Context.Tags.ToList()
                }
            };

        Context.Products.AddRange(products);
        await Context.SaveChangesAsync();
    }

    private static async Task SeedWishlistAndProductsAsync(ApplicationDbContext Context)
    {
        if (Context.Wishlists.Any())
            return;

        var wishlists = new List<Wishlist>()
            {
                new Wishlist
                {
                    UserId = 1
                }
            };

        var newProductWishlists = new List<ProductWishlist>()
            {
                new ProductWishlist
                {
                    Product = Context.Products.Where(x => x.Id % 2 == 1).FirstOrDefault(),
                    Wishlist = wishlists.FirstOrDefault()
                },
                new ProductWishlist
                {
                    Product = Context.Products.Where(x => x.Id % 2 == 1).Skip(1).FirstOrDefault(),
                    Wishlist = wishlists.FirstOrDefault()
                }
            };

        Context.Wishlists.AddRange(wishlists);
        Context.ProductWishlists.AddRange(newProductWishlists);

        await Context.SaveChangesAsync();
    }

    private static async Task SeedCartAndItemsAsync(ApplicationDbContext Context)
    {
        if (Context.Carts.Any())
            return;

        var carts = new List<Cart>()
            {
                new Cart
                {
                    UserId = 1,
                    Items = new List<CartItem>
                    {
                        new CartItem
                        {
                            Product = Context.Products.FirstOrDefault(p => p.Name == "uPhone X"),
                            Quantity = 2,
                            Color = "Black",
                            UnitPrice = 295,
                        },
                        new CartItem
                        {
                            Product = Context.Products.FirstOrDefault(p => p.Name == "Game Station X 22"),
                            Quantity = 1,
                            Color = "Red",
                            UnitPrice = 295,
                        },
                        new CartItem
                        {
                            Product = Context.Products.FirstOrDefault(p => p.Name == "Jackson Toster V 27"),
                            Quantity = 1,
                            Color = "Black",
                            UnitPrice = 185,
                        }
                    }
                }
        };

        Context.Carts.AddRange(carts);
        await Context.SaveChangesAsync();
    }

    private static async Task SeedOrderAndItemsAsync(ApplicationDbContext Context)
    {
        if (Context.Orders.Any())
            return;

        var address = new OrderAddress
        {
            AddressLine = "Gungoren",
            City = "Istanbul",
            Country = "Turkey",
            EmailAddress = "aspnetrun@outlook.com",
            FirstName = "Mehmet",
            LastName = "Ozkaya",
            CompanyName = "AspnetRun",
            PhoneNo = "05012222222",
            State = "027",
            ZipCode = "34056"
        };

        var addressShipping = new OrderAddress
        {
            AddressLine = "Gungoren",
            City = "Istanbul",
            Country = "Turkey",
            EmailAddress = "aspnetrun@outlook.com",
            FirstName = "Mehmet",
            LastName = "Ozkaya",
            CompanyName = "AspnetRun",
            PhoneNo = "05012222222",
            State = "027",
            ZipCode = "34056"
        };

        var orders = new List<Order>()
            {
                new Order
                {
                    UserId = 1,
                    //BillingAddress = address,
                    //ShippingAddress = addressShipping,
                    PaymentMethod = PaymentMethod.PayOnDoor,
                    Status = OrderStatus.Progress,
                    TotalPrice = 347,
                    Items = new List<OrderItem>
                    {
                       new OrderItem
                       {
                           Product = Context.Products.FirstOrDefault(p => p.Name == "uPhone X"),
                            Quantity = 2,
                            Color = "Black",
                            UnitPrice = 295,
                       },
                        new OrderItem
                        {
                            Product = Context.Products.FirstOrDefault(p => p.Name == "Game Station X 22"),
                            Quantity = 1,
                            Color = "Red",
                            UnitPrice = 295,
                        },
                        new OrderItem
                        {
                            Product = Context.Products.FirstOrDefault(p => p.Name == "Jackson Toster V 27"),
                            Quantity = 1,
                            Color = "Black",
                            UnitPrice = 185,
                        }
                    }
                }
        };

        Context.Orders.AddRange(orders);
        await Context.SaveChangesAsync();
    }

}
