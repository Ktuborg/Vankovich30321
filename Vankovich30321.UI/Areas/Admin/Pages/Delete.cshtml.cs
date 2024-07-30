using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
//using GR30321.API.Data;
using GR30321.Domain.Entities;
using Vankovich30321.UI.Services.PictureService;

namespace Vankovich30321.UI.Areas.Admin.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IPictureService _pictureService;

        public DeleteModel(IPictureService pictureService)
        {
            _pictureService = pictureService;
        }

        [BindProperty]
        public Picture Picture { get; set; } = default!;

        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var response = await _pictureService.GetPictureByIdAsync(id.Value);
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
            return Page();

        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
           await _pictureService.DeletePictureAsync(id.Value);
            
            return RedirectToPage("./Index");
        }
    }
}
