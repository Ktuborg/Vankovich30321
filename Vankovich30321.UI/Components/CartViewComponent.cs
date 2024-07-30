using Microsoft.AspNetCore.Mvc;

namespace Vankovich30321.UI.Components
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() 
        { 
            return View(); 
        }
    }
}
