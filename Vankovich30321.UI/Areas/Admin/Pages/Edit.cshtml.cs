using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
//using GR30321.API.Data;
using GR30321.Domain.Entities;
using Vankovich30321.UI.Services.PictureService;
using Vankovich30321.UI.Services.CategoryService;

namespace Vankovich30321.UI.Areas.Admin.Pages
{
    public class EditModel(ICategoryService categoryService, IPictureService pictureService) : PageModel
    {
        //private readonly IPictureService _pictureService;

        //public EditModel(IPictureService pictureService)
        //{
        //    _pictureService = pictureService;
        //}

        [BindProperty]
        public Picture Picture { get; set; } = default!;
        public string? ErrorMessage { get; set; }
        public IFormFile? Image { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var response = await pictureService.GetPictureByIdAsync(id.Value);

            if (response == null)
            {
                return NotFound();
            }
            if (response.Success)
            {
                if (response.Data == null)
                {
                    return NotFound();
                }
                Picture = response.Data;
            }
            else
            {
                ErrorMessage = response.ErrorMessage ?? "Unknown error.";
                return Page();
            }

            // Получим все категории и передим их во ViewData
            var categoryListData = await categoryService.GetCategoryListAsync();
            ViewData["CategoryId"] = new SelectList(categoryListData.Data, "Id",
           "Name");

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Предполагаем, что объект Picture передается с формы
            await pictureService.UpdatePictureAsync(id.Value, Picture, Image);

            return RedirectToPage("./Index");
        }
    }
}
