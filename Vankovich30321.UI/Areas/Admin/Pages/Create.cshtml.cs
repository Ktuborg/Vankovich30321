using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
//using GR30321.API.Data;
using GR30321.Domain.Entities;
using Vankovich30321.UI.Services.PictureService;
using Vankovich30321.UI.Services.CategoryService;

namespace Vankovich30321.UI.Areas.Admin.Pages
{
    public class CreateModel(ICategoryService categoryService, IPictureService productService) : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
            var categoryListData = await categoryService.GetCategoryListAsync();
            ViewData["CategoryId"] = new SelectList(categoryListData.Data, "Id",
           "Name");
            return Page();
        }
        [BindProperty]
        public Picture Picture { get; set; } = default!;
        [BindProperty]
        public IFormFile? Image { get; set; }
       
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await productService.CreatePictureAsync(Picture, Image);
            return RedirectToPage("./Index");
        }

        }

    }
