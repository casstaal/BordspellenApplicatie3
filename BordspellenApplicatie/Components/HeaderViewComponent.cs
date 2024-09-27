using Microsoft.AspNetCore.Mvc;

namespace BordspellenApplicatie.Components
{
    public class HeaderViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
