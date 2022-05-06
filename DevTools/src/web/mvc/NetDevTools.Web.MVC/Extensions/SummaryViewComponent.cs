using Microsoft.AspNetCore.Mvc;

namespace NetDevTools.Web.MVC.Extensions
{
    public class SummaryViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
