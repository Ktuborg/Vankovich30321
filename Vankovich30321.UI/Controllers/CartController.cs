using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GR30321.Domain.Entities;
using Vankovich30321.UI.Data;
using Vankovich30321.UI.Services.PictureService;
using Vankovich30321.UI.Session;

namespace Vankovich30321.UI.Controllers
{
    public class CartController : Controller
    {
        private readonly IPictureService _pictureService;
        private Cart _cart;

        public CartController(IPictureService pictureService)
        {
            _pictureService = pictureService;

        }
        // GET: CartController
        public ActionResult Index()
        {
            _cart = HttpContext.Session.Get<Cart>("cart") ?? new();
            return View(_cart.CartItems);
        }

        [Route("[controller]/add/{id:int}")]
        public async Task<ActionResult> Add(int id, string returnUrl)
        {
            var data = await _pictureService.GetPictureByIdAsync(id);
            if (data.Success)
            {
                _cart = HttpContext.Session.Get<Cart>("cart") ?? new();
                _cart.AddToCart(data.Data);
                HttpContext.Session.Set<Cart>("cart", _cart);
            }
            return Redirect(returnUrl);
        }
        [Route("[controller]/remove/{id:int}")]
        public ActionResult Remove(int id)
        {
            _cart = HttpContext.Session.Get<Cart>("cart") ?? new();
            _cart.RemoveItems(id);
            HttpContext.Session.Set<Cart>("cart", _cart);
            return RedirectToAction("index");
        }

    }
}

