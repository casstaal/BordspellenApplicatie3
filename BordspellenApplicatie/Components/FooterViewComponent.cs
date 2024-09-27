using Microsoft.AspNetCore.Mvc;

namespace BordspellenApplicatie.Components
{
    public class FooterViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
