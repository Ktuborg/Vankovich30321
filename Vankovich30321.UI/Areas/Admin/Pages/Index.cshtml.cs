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
using Microsoft.AspNetCore.Authorization;

namespace Vankovich30321.UI.Areas.Admin.Pages
{
    [Authorize(Policy = "admin")]

    public class IndexModel : PageModel
    {
        private readonly IPictureService _pictureService;

        public IndexModel(IPictureService pictureService)
        {
            _pictureService = pictureService;
        }

        public List<Picture> Picture { get; set; } = default!;
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
        public async Task OnGetAsync(int? pageNo = 1)
        {
            var response = await _pictureService.GetPictureListAsync(null, pageNo.Value);
            if (response.Success)
            {
                Picture = response.Data.Items;
                CurrentPage = response.Data.CurrentPage;
                TotalPages = response.Data.TotalPages;
            }
        }
    }
}
