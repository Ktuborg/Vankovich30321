using GR30321.Domain.Entities;
using GR30321.Domain.Models;

namespace Vankovich30321.UI.Services.CategoryService
{
    public class MemoryCetegoryService : ICategoryService
    {
        public Task<ResponseData<List<Category>>> GetCategoryListAsync()
        {
            var categories = new List<Category>
            {
                new Category {Id=1, Name="Леонардо да Винчи",
                                NormalizedName="Leonardo da Vinci"},
                new Category {Id=2, Name="Виллем де Кунинг",
                                NormalizedName="Willem de Kooning"},
                new Category {Id=3, Name="Поль Сезанн",
                                NormalizedName="Paul Cezanne"},
                new Category {Id=4, Name="Поль Гоген",
                                NormalizedName="Paul Gauguin"},

            };
            var result = new ResponseData<List<Category>>();
            result.Data = categories;
            return Task.FromResult(result);
        }
    }
}

