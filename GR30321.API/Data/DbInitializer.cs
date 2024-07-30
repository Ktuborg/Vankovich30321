using GR30321.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GR30321.API.Data
{
    public class DbInitializer
    {
        public static async Task SeedData(WebApplication app)
        {
            // Uri проекта
            var uri = "https://localhost:7002/";

            // Получение контекста БД
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Выполнение миграций
            await context.Database.MigrateAsync();

            // Заполнение данными
            if (!context.Categories.Any() && !context.Pictures.Any())
            {
                var categories = new Category[]
                {

                new Category {Name="Леонардо да Винчи",
                                NormalizedName="Leonardo da Vinci"},
                new Category {Name="Виллем де Кунинг",
                                NormalizedName="Willem de Kooning"},
                new Category {Name="Поль Сезанн",
                                NormalizedName="Paul Cezanne"},
                new Category {Name="Поль Гоген",
                                NormalizedName="Paul Gauguin"},
                };
                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
                var pictures = new List<Picture>
                {

                new Picture {Name ="Спаситель мира", Avtor="Леонардо да Винчи",
                    CreationDate= 1500, Description ="На холсте изображен Иисус Христос в иконографическом образе Спасителя мира",
                    Price = 450, Image=uri+"Images/1.jpg", Category=categories.FirstOrDefault
                    (c=>c.NormalizedName.Equals("Leonardo da Vinci"))},

                new Picture {Name ="Обмен", Avtor="Виллем де Кунинг",
                    CreationDate= 1955, Description ="Это одна из первых работ де Кунинга переосмысления абстрактной женской фигуры",
                    Price = 300, Image=uri+"Images/2.jpg", Category=categories.FirstOrDefault
                    (c=>c.NormalizedName.Equals("Willem de Kooning"))},

                new Picture {Name ="Игроки в карты", Avtor="Поль Сезанн",
                    CreationDate= 1895, Description ="Произведение конца XIX века считается частью серии полотен с участниками игры в карты.",
                    Price = 250, Image=uri+"Images/3.jpg", Category=categories.FirstOrDefault
                    (c=>c.NormalizedName.Equals("Paul Cezanne"))},

                new Picture {Name ="Когда свадьба?", Avtor="Поль Гоген",
                    CreationDate= 1892, Description ="Произведение Поля Гогена, созданное им во время нахождения на Таити (примерно в 1892 г.). На холсте лица местных девушек, в которых Поль нашел музу и вдохновение.",
                    Price = 210, Image = uri + "Images/4.jpg", Category = categories.FirstOrDefault
                    (c => c.NormalizedName.Equals("Paul Gauguin")) },

                };

                await context.AddRangeAsync(pictures);
                await context.SaveChangesAsync();
            }
        }

    }
}
