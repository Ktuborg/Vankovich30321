using GR30321.Domain.Entities;
using Vankovich30321.UI.Services.PictureService;
using Vankovich30321.UI.Services.CategoryService;
using Microsoft.AspNetCore.Mvc;

namespace Vankovich30321.UI.Controllers
{
    public class PictureController(ICategoryService categoryService, IPictureService pictureService) : Controller
    {
        [Route("Catalog")]
        [Route("Catalog/{category}")]
        public async Task<IActionResult> Index(string? category, int pageNo = 1)
        {
            // получить список категорий
            var categoriesResponse = await categoryService.GetCategoryListAsync();
            // если список не получен, вернуть код 404
            if (!categoriesResponse.Success)
                return NotFound(categoriesResponse.ErrorMessage);
            // передать список категорий во ViewData 
            ViewData["categories"] = categoriesResponse.Data;
            // передать во ViewData имя текущей категории
            var currentCategory = category == null ? "Все" : categoriesResponse.Data.FirstOrDefault(c => c.NormalizedName == category)?.Name;
            ViewData["currentCategory"] = currentCategory;


            var pictureResponse = await pictureService.GetPictureListAsync(category, pageNo);
            if (!pictureResponse.Success)
                ViewData["Error"] = pictureResponse.ErrorMessage;
            //return View(pictureResponse.Data.Items);
            return View(pictureResponse.Data);
        }
    }
}
