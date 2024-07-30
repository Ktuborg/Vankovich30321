using GR30321.Domain.Entities;
using GR30321.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Vankovich30321.UI.Services.CategoryService;

namespace Vankovich30321.UI.Services.PictureService
{
    public class MemoryPictureService : IPictureService
    {
        List<Picture> _pictures;
        List<Category> _categories;
        private readonly IConfiguration _config;

        public MemoryPictureService([FromServices] IConfiguration config, ICategoryService categoryService)
        {
            _categories = categoryService.GetCategoryListAsync()
           .Result
           .Data;
            _config = config;
            SetupData();
        }

        // Инициализация списков
        private void SetupData()
        {
            _pictures = new List<Picture>
            {
                new Picture {Id = 1, Name ="Спаситель мира", Avtor="Леонардо да Винчи",
                CreationDate= 1500, Description ="На холсте изображен Иисус Христос в иконографическом образе Спасителя мира",
                Price = 450, Image="Images/1.jpg", CategoryId=_categories.Find(c=>c.NormalizedName.Equals("Leonardo da Vinci")).Id, Category = _categories.Find(c => c.NormalizedName.Equals("Leonardo da Vinci")) },

                new Picture {Id = 2, Name ="Обмен", Avtor="Виллем де Кунинг",
                CreationDate= 1955, Description ="Это одна из первых работ де Кунинга переосмысления абстрактной женской фигуры",
                Price = 300, Image="Images/2.jpg", CategoryId=_categories.Find(c=>c.NormalizedName.Equals("Willem de Kooning")).Id, Category = _categories.Find(c=>c.NormalizedName.Equals("Willem de Kooning"))},

                new Picture {Id = 3, Name ="Игроки в карты", Avtor="Поль Сезанн",
                CreationDate= 1895, Description ="Произведение конца XIX века считается частью серии полотен с участниками игры в карты.",
                Price = 250, Image="Images/3.jpg", CategoryId=_categories.Find(c=>c.NormalizedName.Equals("Paul Cezanne")).Id, Category = _categories.Find(c=>c.NormalizedName.Equals("Paul Cezanne")) },

                new Picture {Id = 4, Name ="Когда свадьба?", Avtor="Поль Гоген",
                CreationDate= 1892, Description ="Произведение Поля Гогена, созданное им во время нахождения на Таити (примерно в 1892 г.). На холсте лица местных девушек, в которых Поль нашел музу и вдохновение.",
                Price = 210, Image="Images/4.jpg", CategoryId=_categories.Find(c=>c.NormalizedName.Equals("Paul Gauguin")).Id, Category = _categories.Find(c => c.NormalizedName.Equals("Paul Gauguin")) },

            };
        }
        public Task<ResponseData<Picture>> CreatePictureAsync(Picture picture, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<Picture>> DeletePictureAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<Picture>> GetPictureByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<ListModel<Picture>>> GetPictureListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            //var model = new ListModel<Picture>() { Items = _pictures };
            //var result = new ResponseData<ListModel<Picture>>()

            //{ Data = model };
            //return Task.FromResult(result);

            // Создать объект результата
            var result = new ResponseData<ListModel<Picture>>();
            // Id категории для фильрации
            int? categoryId = null;
            // если требуется фильтрация, то найти Id категории с заданным categoryNormalizedName
            if (categoryNormalizedName != null)
                categoryId = _categories.Find(c => c.NormalizedName.Equals(categoryNormalizedName))?.Id;
            // Выбрать объекты, отфильтрованные по Id категории, если этот Id имеется
            var data = _pictures
            .Where(d => categoryId == null ||
           d.CategoryId.Equals(categoryId))?
            .ToList();

            // получить размер страницы из конфигурации
            int pageSize = _config.GetSection("ItemsPerPage").Get<int>();
            // получить общее количество страниц
            int totalPages = (int)Math.Ceiling(data.Count / (double)pageSize);
            // получить данные страницы
            var listData = new ListModel<Picture>()
            {
                Items = data.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList(),
                CurrentPage = pageNo,
                TotalPages = totalPages
            };


            // поместить ранные в объект результата
            //result.Data = new ListModel<Picture>() { Items = data };
            result.Data = listData;

            // Если список пустой
            if (data.Count == 0)
            {
                result.Success = false;
                result.ErrorMessage = "Нет объектов в выбраннной категории";
            }
            // Вернуть результат
            return Task.FromResult(result);

        }

        public Task<ResponseData<Picture>> UpdatePictureAsync(int id, Picture picture, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }
    }
}
