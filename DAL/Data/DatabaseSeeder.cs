using Bogus;
using DAL.Constants;
using DAL.Data.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Data
{
    public static class DatabaseSeeder
    {
        public static void Seed()
        {
            using (EFAppContext dataContext = new EFAppContext())
            {
                SeedCategories(dataContext);
                SeedProducts(dataContext);
                SeedProductImages(dataContext);
                SeedUsers(dataContext);
                SeedBasket(dataContext);
                SeedRoles(dataContext);
                SeedUserRoles(dataContext);
            }
        }
        private static void SeedCategories(EFAppContext dataContext)
        {
            if (!dataContext.Categories.Any())
            {
                var pizza = new CategoryEntity
                {
                    Name = "Pizza",
                    DateCreated = DateTime.Now,
                    Image = @"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS1YqYCxTPTgWe6s_C0f470QnY47I82Y-PznA&usqp=CAU"
                };
                var sopus = new CategoryEntity
                {
                    Name = "Soups",
                    DateCreated = DateTime.Now,
                    Image = "https://images.immediate.co.uk/production/volatile/sites/2/2016/08/25097.jpg?quality=90&crop=2px,151px,596px,542px&resize=960,872"
                };
                var desserts = new CategoryEntity
                {
                    Name = "Desserts",
                    DateCreated = DateTime.Now,
                    Image = @"https://food.fnr.sndimg.com/content/dam/images/food/fullset/2020/04/06/FNK_Chocolate-Mousse_H_v2_4x3.jpg.rend.hgtvcom.441.331.suffix/1586200107816.jpeg"
                };
                var drinks = new CategoryEntity
                {
                    Name = "Drinks",
                    DateCreated = DateTime.Now,
                    Image = @"https://images.immediate.co.uk/production/volatile/sites/30/2013/05/Singapore-sling-7ddad3e.jpg"
                };

                dataContext.Categories.Add(pizza);
                dataContext.Categories.Add(sopus);
                dataContext.Categories.Add(desserts);
                dataContext.Categories.Add(drinks);
                dataContext.SaveChanges();
            
            }
        }

        private static void SeedProducts(EFAppContext dataContext)
        {
           
            if (!dataContext.Products.Any())
            {

                CategoryRepository repository = new CategoryRepository(dataContext);
                var list = repository.GetAll();
                int pizzaId = list.Where((i) => i.Name == "Pizza").FirstOrDefault().Id;
                int soupsId = list.Where((i) => i.Name == "Soups").FirstOrDefault().Id;
                int desserstId = list.Where((i) => i.Name == "Desserts").FirstOrDefault().Id;
                int drinksId = list.Where((i) => i.Name == "Drinks").FirstOrDefault().Id;

                var margherita = new ProductEntity
                {
                    DateCreated = DateTime.Now,
                    Name = "Margherita",
                    Price = 264,
                    DiscountPrice = 200,
                    IsOnDiscount = false,
                    Weight = 552,
                    Description = "Tomato Sauce, Mozzarella, Basil",
                    CategoryId = pizzaId

                };
                var formaggi = new ProductEntity
                {
                    DateCreated = DateTime.Now,
                    Name = "Formaggi",
                    Price = 225,
                    DiscountPrice = 180,
                    IsOnDiscount = false,
                    Weight = 480,
                    Description = "Tomato Sauce, Mozzarella, Parmesan, Mascarpone, Gorgonzola",
                    CategoryId = pizzaId

                };
                var pepperoni = new ProductEntity
                {
                    DateCreated = DateTime.Now,
                    Name = "Pepperoni",
                    Price = 264,
                    DiscountPrice = 220,
                    IsOnDiscount = false,
                    Weight = 530,
                    Description = "Tomato Sauce, Mozzarella, Double Pepperoni",
                    CategoryId = pizzaId

                };
                var carbonara = new ProductEntity
                {
                    DateCreated = DateTime.Now,
                    Name = "Carbonara",
                    Price = 280,
                    DiscountPrice = 240,
                    IsOnDiscount = false,
                    Weight = 525,
                    Description = "Onion, Bacon, Ham, Mushrooms, Mozarella, Al'fredo sauce",
                    CategoryId = pizzaId

                };
                ////////////////////////////////////////////////////////////////////////
                var tomYum = new ProductEntity
                {
                    DateCreated = DateTime.Now,
                    Name = "Tom Yum",
                    Price = 380,
                    DiscountPrice = 330,
                    IsOnDiscount = false,
                    Weight = 640,
                    Description = "Shrimp and chicken broth, Steamed rice, Scallop, Shrimp",
                    CategoryId = soupsId

                };
                var chickenNoodle  = new ProductEntity
                {
                    DateCreated = DateTime.Now,
                    Name = "Chicken noodle soup",
                    Price = 105,
                    DiscountPrice = 85,
                    IsOnDiscount = false,
                    Weight = 300,
                    Description = "Shrimp and chicken broth, Steamed rice, Scallop, Shrimp",
                    CategoryId = soupsId

                };
                var misoSoup = new ProductEntity
                {
                    DateCreated = DateTime.Now,
                    Name = "Miso soup with salmon",
                    Price = 175,
                    DiscountPrice = 150,
                    IsOnDiscount = false,
                    Weight = 300,
                    Description = "Miso broth, algae wakame, mushrooms Shiitake, cheese Tofu, salmon, green onion, sesame",
                    CategoryId = soupsId

                };
                var cheeseCreamySoup = new ProductEntity
                {
                    DateCreated = DateTime.Now,
                    Name = "Cheese creamy soup",
                    Price = 140,
                    DiscountPrice = 115,
                    IsOnDiscount = false,
                    Weight = 300,
                    Description = "Cream cheese soup with crispy croutons",
                    CategoryId = soupsId
                };
                ///////////////////////////////////////////////
                var tiramisu = new ProductEntity
                {
                    DateCreated = DateTime.Now,
                    Name = "Tiramisu",
                    Price = 160,
                    DiscountPrice = 130,
                    IsOnDiscount = false,
                    Weight = 160,
                    Description = "Coffee and Marsala wine give this iconic dessert its kick",
                    CategoryId = desserstId
                };
                var berryCheesecake = new ProductEntity
                {
                    DateCreated = DateTime.Now,
                    Name = "Red Berry & Vanilla Cheesecake",
                    Price = 140,
                    DiscountPrice = 110,
                    IsOnDiscount = false,
                    Weight = 145,
                    Description = "Tart, sweet red berries meets rich vanilla",
                    CategoryId = desserstId
                };
                var chocolateBrownie = new ProductEntity
                {
                    DateCreated = DateTime.Now,
                    Name = "Chocolate Brownie",
                    Price = 155,
                    DiscountPrice = 115,
                    IsOnDiscount = false,
                    Weight = 165,
                    Description = "Double Belgian chocolate, served warm with vanilla gelato",
                    CategoryId = desserstId
                };
                var applePie = new ProductEntity
                {
                    DateCreated = DateTime.Now,
                    Name = "Apple Pie",
                    Price = 135,
                    DiscountPrice = 105,
                    IsOnDiscount = false,
                    Weight = 160,
                    Description = "Sugar, Wheat flour, Egg, Butter",
                    CategoryId = desserstId
                };
                /////////////////////////////////////////////////////////////////////////
                
                var cocaCola = new ProductEntity
                {
                    DateCreated = DateTime.Now,
                    Name = "Coca-Cola",
                    Price = 38,
                    DiscountPrice = 35,
                    IsOnDiscount = false,
                    Weight = 500,
                    CategoryId = drinksId
                };
                var sprite = new ProductEntity
                {
                    DateCreated = DateTime.Now,
                    Name = "Sprite",
                    Price = 38,
                    DiscountPrice = 35,
                    IsOnDiscount = false,
                    Weight = 500,
                    CategoryId = drinksId
                };
                var schweppes = new ProductEntity
                {
                    DateCreated = DateTime.Now,
                    Name = "Schweppes Mojito",
                    Price = 40,
                    DiscountPrice = 36,
                    IsOnDiscount = false,
                    Weight = 250,
                    CategoryId = drinksId
                };
                var richOrange = new ProductEntity
                {
                    DateCreated = DateTime.Now,
                    Name = "Rich Orange",
                    Price = 80,
                    DiscountPrice = 75,
                    IsOnDiscount = false,
                    Weight = 1000,
                    CategoryId = drinksId
                };

                dataContext.Products.Add(margherita);
                dataContext.Products.Add(formaggi);
                dataContext.Products.Add(pepperoni);
                dataContext.Products.Add(carbonara);
                dataContext.Products.Add(tomYum);
                dataContext.Products.Add(chickenNoodle);
                dataContext.Products.Add(misoSoup);
                dataContext.Products.Add(cheeseCreamySoup);
                dataContext.Products.Add(tiramisu);
                dataContext.Products.Add(berryCheesecake);
                dataContext.Products.Add(chocolateBrownie);
                dataContext.Products.Add(applePie);
                dataContext.Products.Add(cocaCola);
                dataContext.Products.Add(sprite);
                dataContext.Products.Add(schweppes);
                dataContext.Products.Add(richOrange);
                dataContext.SaveChanges();
            }

        }

        private static void SeedProductImages(EFAppContext dataContext)
        {
           
            if (!dataContext.ProductImages.Any())
            {
                ProductRepository repository = new ProductRepository(dataContext);
                var list = repository.GetAll();
                int margheritaId = list.Where((i) => i.Name == "Margherita").FirstOrDefault().Id;
                int formaggiId = list.Where((i) => i.Name == "Formaggi").FirstOrDefault().Id;
                int pepperoniId = list.Where((i) => i.Name == "Pepperoni").FirstOrDefault().Id;
                int carbonaraId = list.Where((i) => i.Name == "Carbonara").FirstOrDefault().Id;

                int tomYumId = list.Where((i) => i.Name == "Tom Yum").FirstOrDefault().Id;
                int chickenNoodleId = list.Where((i) => i.Name == "Chicken noodle soup").FirstOrDefault().Id;
                int misoSoupId = list.Where((i) => i.Name == "Miso soup with salmon").FirstOrDefault().Id;
                int creamySoupId = list.Where((i) => i.Name == "Cheese creamy soup").FirstOrDefault().Id;

                int tiramisuId = list.Where((i) => i.Name == "Tiramisu").FirstOrDefault().Id;
                int cheesecakeId = list.Where((i) => i.Name == "Red Berry & Vanilla Cheesecake").FirstOrDefault().Id;
                int brownieId = list.Where((i) => i.Name == "Chocolate Brownie").FirstOrDefault().Id;
                int applePieId = list.Where((i) => i.Name == "Apple Pie").FirstOrDefault().Id;

                int colaId = list.Where((i) => i.Name == "Coca-Cola").FirstOrDefault().Id;
                int spriteId = list.Where((i) => i.Name == "Sprite").FirstOrDefault().Id;
                int schweppesId = list.Where((i) => i.Name == "Schweppes Mojito").FirstOrDefault().Id;
                int richId = list.Where((i) => i.Name == "Rich Orange").FirstOrDefault().Id;

                var margherita1 = new ProductImageEntity
                {
                    Name = @"https://res.cloudinary.com/norgesgruppen/images/c_scale,dpr_auto,f_auto,q_auto:eco,w_1600/mnnrfrpmfqbjtnxdycf2/klassisk-pizza-margherita",
                    Priority = 1,
                    DateCreated = DateTime.Now,
                    ProductId = margheritaId
                };
                var margherita2 = new ProductImageEntity
                {
                    Name = @"https://www.jessicagavin.com/wp-content/uploads/2019/08/skillet-margherita-pizza-12-1200.jpg",
                    Priority = 2,
                    DateCreated = DateTime.Now,
                    ProductId = margheritaId
                };
                var margherita3 = new ProductImageEntity
                {
                    Name = @"https://www.kuechengoetter.de/uploads/media/960x960/02/4282-pizza-margherita.jpg?v=1-0",
                    Priority = 3,
                    DateCreated = DateTime.Now,
                    ProductId = margheritaId
                };

                var formaggi1 = new ProductImageEntity
                {
                    Name = @"https://thumbs.dreamstime.com/b/pizza-quatro-formaggi-tomato-sauce-kinds-cheese-black-background-224077173.jpg",
                    Priority = 1,
                    DateCreated = DateTime.Now,
                    ProductId = formaggiId
                };
                var formaggi2 = new ProductImageEntity
                {
                    Name = @"https://prostepesto.pl/wp-content/uploads/2021/09/pizza-quattro-formaggi.jpg",
                    Priority = 2,
                    DateCreated = DateTime.Now,
                    ProductId = formaggiId
                };
                var formaggi3 = new ProductImageEntity
                {
                    Name = @"https://www.italianstylecooking.net/wp-content/uploads/2020/04/Pizza-quattro-formaggi-neu.jpg",
                    Priority = 3,
                    DateCreated = DateTime.Now,
                    ProductId = formaggiId
                };

                var pepperoni1 = new ProductImageEntity
                {
                    Name = @"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRcRkshGWRWWmB1rN_SKfUrQZCllyK8eIWEyw&usqp=CAU",
                    Priority = 1,
                    DateCreated = DateTime.Now,
                    ProductId = pepperoniId
                };
                var pepperoni2 = new ProductImageEntity
                {
                    Name = @"https://instafood.com.ua/images/images/pitsa-pepperoni.jpg",
                    Priority = 2,
                    DateCreated = DateTime.Now,
                    ProductId = pepperoniId
                };
                var pepperoni3 = new ProductImageEntity
                {
                    Name = @"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT-zKTyAdfOhT5Dr5laVmv5T3sqQJVHVFjkXA&usqp=CAU",
                    Priority = 3,
                    DateCreated = DateTime.Now,
                    ProductId = pepperoniId
                };

                var carbonara1 = new ProductImageEntity
                {
                    Name = @"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQGytW9euGKqhzjTz5CGJ7tmwnsn5x3PfDTKg&usqp=CAU",
                    Priority = 1,
                    DateCreated = DateTime.Now,
                    ProductId = carbonaraId
                };
                var carbonara2 = new ProductImageEntity
                {
                    Name = @"https://recetasdecocina.elmundo.es/wp-content/uploads/2018/02/pizza-carbonara.jpg",
                    Priority = 2,
                    DateCreated = DateTime.Now,
                    ProductId = carbonaraId
                };
                var carbonara3 = new ProductImageEntity
                {
                    Name = @"https://www.pizza24.ge/images/thumbs/0001858_carbonara-pizza-new_510.jpeg",
                    Priority = 3,
                    DateCreated = DateTime.Now,
                    ProductId = carbonaraId
                };
                //////////////////////////////////////////////////////////////////////////////////////////////////

                var tomYum1 = new ProductImageEntity
                {
                    Name = @"https://casta.ua/storage/editor/fotos/tom-yam.jpeg",
                    Priority = 1,
                    DateCreated = DateTime.Now,
                    ProductId = tomYumId
                };
                var tomYum2 = new ProductImageEntity
                {
                    Name = @"https://www.recipetineats.com/wp-content/uploads/2019/09/Tom-Yum-soup_2.jpg",
                    Priority = 2,
                    DateCreated = DateTime.Now,
                    ProductId = tomYumId
                };
                var tomYum3 = new ProductImageEntity
                {
                    Name = @"https://www.maggi.ru/data/images/recept/img640x500/recept_8567_zpcj.jpg",
                    Priority = 3,
                    DateCreated = DateTime.Now,
                    ProductId = tomYumId
                };

                var chickenNoodle1 = new ProductImageEntity
                {
                    Name = @"https://as2.ftcdn.net/v2/jpg/03/15/11/77/1000_F_315117712_bkUYmghb3wUjQdEODchQ1pcV9dapIeMk.jpg",
                    Priority = 1,
                    DateCreated = DateTime.Now,
                    ProductId = chickenNoodleId
                };
                var chickenNoodle2 = new ProductImageEntity
                {
                    Name = @"https://recipetineats.com/wp-content/uploads/2017/05/Chicken-Noodle-Soup-from-scratch_3.jpg",
                    Priority = 2,
                    DateCreated = DateTime.Now,
                    ProductId = chickenNoodleId
                };
                var chickenNoodle3 = new ProductImageEntity
                {
                    Name = @"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTBiUviKSPmRImwqKvR63seYL-XMoJeTRHTrw&usqp=CAU",
                    Priority = 3,
                    DateCreated = DateTime.Now,
                    ProductId = chickenNoodleId
                };

                var misoSoup1 = new ProductImageEntity
                {
                    Name = @"https://www.feastingathome.com/wp-content/uploads/2022/01/Miso-Soup-18-1024x1536.jpg",
                    Priority = 1,
                    DateCreated = DateTime.Now,
                    ProductId = misoSoupId
                };
                var misoSoup2 = new ProductImageEntity
                {
                    Name = @"https://www.feastingathome.com/wp-content/uploads/2022/01/Miso-Soup-17.jpg",
                    Priority = 2,
                    DateCreated = DateTime.Now,
                    ProductId = misoSoupId
                };
                var misoSoup3 = new ProductImageEntity
                {
                    Name = @"https://img.freepik.com/premium-photo/traditional-asian-soup-black-background-miso-soup-with-salmon_235573-1456.jpg?w=2000",
                    Priority = 3,
                    DateCreated = DateTime.Now,
                    ProductId = misoSoupId
                };

                var cheeseCreamySoup1 = new ProductImageEntity
                {
                    Name = @"https://www.thegraciouswife.com/wp-content/uploads/2014/10/Butternut-Squash-Bisque-LARGE-8-1024x683.jpg",
                    Priority = 1,
                    DateCreated = DateTime.Now,
                    ProductId = creamySoupId
                };
                var cheeseCreamySoup2 = new ProductImageEntity
                {
                    Name = @"https://bellyfull.net/wp-content/uploads/2022/10/Broccoli-Cheddar-Soup-blog-2.jpg",
                    Priority = 2,
                    DateCreated = DateTime.Now,
                    ProductId = creamySoupId
                };
                var cheeseCreamySoup3 = new ProductImageEntity
                {
                    Name = @"https://modernminimalism.com/wp-content/uploads/2020/10/Broccoli-Cauliflower-Cheese-Soup-Web-4-scaled.jpg",
                    Priority = 3,
                    DateCreated = DateTime.Now,
                    ProductId = creamySoupId
                };
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                var tiramisu1 = new ProductImageEntity
                {
                    Name = @"https://img.freepik.com/premium-photo/italian-tiramisu-cake-with-cocoa-mint-plate-black-background-top-view_89816-32883.jpg?w=2000",
                    Priority = 1,
                    DateCreated = DateTime.Now,
                    ProductId = tiramisuId
                };
                var tiramisu2 = new ProductImageEntity
                {
                    Name = @"https://asassyspoon.com/wp-content/uploads/easy-individual-tiramisu-cups-3.jpg",
                    Priority = 2,
                    DateCreated = DateTime.Now,
                    ProductId = tiramisuId
                };
                var tiramisu3 = new ProductImageEntity
                {
                    Name = @"https://upload.wikimedia.org/wikipedia/commons/thumb/e/e7/Classic_Italian_Tiramisu-3_%2829989504485%29.jpg/800px-Classic_Italian_Tiramisu-3_%2829989504485%29.jpg",
                    Priority = 3,
                    DateCreated = DateTime.Now,
                    ProductId = tiramisuId
                };

                var berryCheesecake1 = new ProductImageEntity
                {
                    Name = @"https://www.pressandjournal.co.uk/wp-content/uploads/sites/2/2020/08/shutterstock_1476182615-scaled-e1598441551136.jpg",
                    Priority = 1,
                    DateCreated = DateTime.Now,
                    ProductId = cheesecakeId
                };
                var berryCheesecake2 = new ProductImageEntity
                {
                    Name = @"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTE0V_hHqoOR648JupT-qoJcUSF1ekIpYuDQw&usqp=CAU",
                    Priority = 2,
                    DateCreated = DateTime.Now,
                    ProductId = cheesecakeId
                };
                var berryCheesecake3 = new ProductImageEntity
                {
                    Name = @"https://images.aws.nestle.recipes/original/f9b71d478726e3f1448911979032836c_bak---03---cherry-berry-cheesecake-657_edit.jpg",
                    Priority = 3,
                    DateCreated = DateTime.Now,
                    ProductId = cheesecakeId
                };

                var chocolateBrownie1 = new ProductImageEntity
                {
                    Name = @"https://t3.ftcdn.net/jpg/03/00/12/58/360_F_300125802_jeNNzQKpdfGOCtjXih4NuyLLVUeGTel3.jpg",
                    Priority = 1,
                    DateCreated = DateTime.Now,
                    ProductId = brownieId
                };
                var chocolateBrownie2 = new ProductImageEntity
                {
                    Name = @"https://food.fnr.sndimg.com/content/dam/images/food/fullset/2021/12/15/0/FNM_010122-Dark-Chocolate-Brownie-Sundae_s4x3.jpg.rend.hgtvcom.616.462.suffix/1639586951781.jpeg",
                    Priority = 2,
                    DateCreated = DateTime.Now,
                    ProductId = brownieId
                };
                var chocolateBrownie3 = new ProductImageEntity
                {
                    Name = @"https://www.oetker.in/Recipe/Recipes/oetker.in/in-en/dessert/image-thumb__147740__RecipeDetailsLightBox/brownie-with-ice-cream.jpg",
                    Priority = 3,
                    DateCreated = DateTime.Now,
                    ProductId = brownieId
                };

                var applePie1 = new ProductImageEntity
                {
                    Name = @"https://myareanetwork-photos.s3.amazonaws.com/editorphotos/f/42947_1620673085.jpg",
                    Priority = 1,
                    DateCreated = DateTime.Now,
                    ProductId = applePieId
                };
                var applePie2 = new ProductImageEntity
                {
                    Name = @"https://i0.wp.com/www.livewellbakeoften.com/wp-content/uploads/2020/08/Apple-Pie-1.jpg?resize=682%2C1024&ssl=1",
                    Priority = 2,
                    DateCreated = DateTime.Now,
                    ProductId = applePieId
                };
                var applePie3 = new ProductImageEntity
                {
                    Name = @"https://www.missallieskitchen.com/wp-content/uploads/2019/05/classic-lattice-apple-pie-.jpg",
                    Priority = 3,
                    DateCreated = DateTime.Now,
                    ProductId = applePieId
                };
                //////////////////////////////////////////////////////////////////////////////
                var cola = new ProductImageEntity
                {
                    Name = @"https://img.auchan.ua/rx/q_90,ofmt_webp/https://auchan.ua/media/catalog/product/5/4/54491472_43.jpg",
                    Priority = 1,
                    DateCreated = DateTime.Now,
                    ProductId = colaId
                };
                var sprite = new ProductImageEntity
                {
                    Name = @"https://vesuvio.ua/wp-content/uploads/2020/02/fanta-vesuvio-pizza-2.jpg",
                    Priority = 1,
                    DateCreated = DateTime.Now,
                    ProductId = spriteId
                };
                var schweppes = new ProductImageEntity
                {
                    Name = @"https://okwine.ua/files/product/be1f3d42-5940-493b-8711-c25c12de845b.napij-schweppes-classic-mojito-zhb-025l.jpg",
                    Priority = 1,
                    DateCreated = DateTime.Now,
                    ProductId = schweppesId
                };
                var richOrange = new ProductImageEntity
                {
                    Name = @"https://img3.zakaz.ua/13102020.1602599926.ad72436478c_2020-10-13_Tatyana_L/13102020.1602599926.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.350nowm.jpg.350x.jpg",
                    Priority = 1,
                    DateCreated = DateTime.Now,
                    ProductId = richId
                };



                dataContext.ProductImages.Add(margherita1);
                dataContext.ProductImages.Add(margherita2);
                dataContext.ProductImages.Add(margherita3);
                dataContext.ProductImages.Add(formaggi1);
                dataContext.ProductImages.Add(formaggi2);
                dataContext.ProductImages.Add(formaggi3);
                dataContext.ProductImages.Add(pepperoni1);
                dataContext.ProductImages.Add(pepperoni2);
                dataContext.ProductImages.Add(pepperoni3);
                dataContext.ProductImages.Add(carbonara1);
                dataContext.ProductImages.Add(carbonara2);
                dataContext.ProductImages.Add(carbonara3);

                dataContext.ProductImages.Add(tomYum1);
                dataContext.ProductImages.Add(tomYum2);
                dataContext.ProductImages.Add(tomYum3);
                dataContext.ProductImages.Add(chickenNoodle1);
                dataContext.ProductImages.Add(chickenNoodle2);
                dataContext.ProductImages.Add(chickenNoodle3);
                dataContext.ProductImages.Add(misoSoup1);
                dataContext.ProductImages.Add(misoSoup2);
                dataContext.ProductImages.Add(misoSoup3);
                dataContext.ProductImages.Add(cheeseCreamySoup1);
                dataContext.ProductImages.Add(cheeseCreamySoup2);
                dataContext.ProductImages.Add(cheeseCreamySoup3);

                dataContext.ProductImages.Add(tiramisu1);
                dataContext.ProductImages.Add(tiramisu2);
                dataContext.ProductImages.Add(tiramisu3);
                dataContext.ProductImages.Add(berryCheesecake1);
                dataContext.ProductImages.Add(berryCheesecake2);
                dataContext.ProductImages.Add(berryCheesecake3);
                dataContext.ProductImages.Add(chocolateBrownie1);
                dataContext.ProductImages.Add(chocolateBrownie2);
                dataContext.ProductImages.Add(chocolateBrownie3);
                dataContext.ProductImages.Add(applePie1);
                dataContext.ProductImages.Add(applePie2);
                dataContext.ProductImages.Add(applePie3);

                dataContext.ProductImages.Add(cola);
                dataContext.ProductImages.Add(sprite);
                dataContext.ProductImages.Add(schweppes);
                dataContext.ProductImages.Add(richOrange);



                dataContext.SaveChanges();
            }

        }

        private static void SeedUsers(EFAppContext dataContext)
        {
            if (!dataContext.Users.Any())
            {
                var user1 = new UserEntity
                {
                    FirstName = "Ivan",
                    LastName = "Koval",
                    Email = "ivan@ua.com",
                    Phone = "0971548789",
                    Password = "123456",
                    IsDelete = false,
                    DateCreated = DateTime.Now
                };
                var user2 = new UserEntity
                {
                    FirstName = "Vika",
                    LastName = "Khmil",
                    Email = "vika@ua.com",
                    Phone = "0967548780",
                    Password = "123456",
                    IsDelete = false,
                    DateCreated = DateTime.Now
                };
                var user3 = new UserEntity
                {
                    FirstName = "John",
                    LastName = "Dorrison",
                    Email = "john@gmail.com",
                    Phone = "0970012363",
                    Password = "123456",
                    IsDelete = false,
                    DateCreated = DateTime.Now
                };
                var user4 = new UserEntity
                {
                    FirstName = "Kate",
                    LastName = "Fomit",
                    Email = "kate@gmail.com",
                    Phone = "0662323231",
                    Password = "123456",
                    IsDelete = false,
                    DateCreated = DateTime.Now
                };


                dataContext.Users.Add(user1);
                dataContext.Users.Add(user2);
                dataContext.Users.Add(user3);
                dataContext.Users.Add(user4);
               
                dataContext.SaveChanges();

            }
        }
        private static void SeedBasket(EFAppContext dataContext)
        {
            if (!dataContext.Baskets.Any())
            {
                UserRepository us_repository = new UserRepository(dataContext);
                var usertList = us_repository.GetAll();
                int userId1 = usertList.Where((i) => i.Email == "vika@ua.com").FirstOrDefault().Id;
                int userId2 = usertList.Where((i) => i.Email == "john@gmail.com").FirstOrDefault().Id;

                ProductRepository pr_repository = new ProductRepository(dataContext);
                var productList = pr_repository.GetAll();
                int pepperoniId = productList.Where((i) => i.Name == "Pepperoni").FirstOrDefault().Id;
                int cheesecakeId = productList.Where((i) => i.Name == "Red Berry & Vanilla Cheesecake").FirstOrDefault().Id;
                int tomYumId = productList.Where((i) => i.Name == "Tom Yum").FirstOrDefault().Id;


                var basket1 = new BasketEntity
                {
                    UserId = userId1,
                    ProductId = cheesecakeId,
                    Count = 2
                };
                var basket2 = new BasketEntity
                {
                    UserId = userId1,
                    ProductId = tomYumId,
                    Count = 3
                };
                var basket3 = new BasketEntity
                {
                    UserId = userId2,
                    ProductId = pepperoniId,
                    Count = 4
                };

                dataContext.Baskets.Add(basket1);
                dataContext.Baskets.Add(basket2);
                dataContext.Baskets.Add(basket3);
                dataContext.SaveChanges();
            }
        }

        private static void SeedRoles(EFAppContext dataContext)
        {
            if (!dataContext.Roles.Any())
            {
                var r1 = new RoleEntity
                {
                    Name = Roles.User
                };
                var r2 = new RoleEntity
                {
                    Name = Roles.Admin
                };
                var r3 = new RoleEntity
                {
                    Name = Roles.SuperAdmin
                };
                dataContext.Roles.Add(r1);
               dataContext.Roles.Add(r2);
               dataContext.Roles.Add(r3);
               dataContext.SaveChanges();

            }
        }

        private static void SeedUserRoles(EFAppContext dataContext)
        {
            if (!dataContext.UserRoles.Any())
            {
                UserRepository us_repository = new UserRepository(dataContext);
                var usertList = us_repository.GetAll();
                int userId = usertList.Where((i) => i.Email == "admin@gmail.com").FirstOrDefault().Id;

                var rolesList = dataContext.Roles;
                int roleId = rolesList.Where((i) => i.Name == Roles.Admin).FirstOrDefault().Id;
                var userrole = new UserRoleEntity
                {
                    UserId = userId,
                    RoleId = roleId
                };



                int suprAdmId = usertList.Where((i) => i.Email == "superAdmin@gmail.com").FirstOrDefault().Id;
                int superRoleId = rolesList.Where((i) => i.Name == Roles.SuperAdmin).FirstOrDefault().Id;

                var userrole2 = new UserRoleEntity
                {
                    UserId = suprAdmId,
                    RoleId = superRoleId
                };

                dataContext.UserRoles.Add(userrole);
                dataContext.UserRoles.Add(userrole2);
                dataContext.SaveChanges();

            }
        }

    }
}
