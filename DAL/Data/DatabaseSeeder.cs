using Bogus;
using DAL.Data.Entities;
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
        
    }
}
