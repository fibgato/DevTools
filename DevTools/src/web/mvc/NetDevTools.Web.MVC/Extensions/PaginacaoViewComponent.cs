using NetDevTools.Web.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace NetDevTools.Web.MVC.Extensions
{
    public class PaginacaoViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IPagedList modeloPaginado)
        {
            return View(modeloPaginado);
        }
    }
}
