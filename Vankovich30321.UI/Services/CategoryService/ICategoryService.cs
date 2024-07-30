using GR30321.Domain.Entities;
using GR30321.Domain.Models;

namespace Vankovich30321.UI.Services.CategoryService
{
    public interface ICategoryService
    {
        // Получение списка всех категорий
        public Task<ResponseData<List<Category>>> GetCategoryListAsync();
    }
}
