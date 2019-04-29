using Microsoft.AspNetCore.Antiforgery;
using GalaxySkyCompany.Controllers;

namespace GalaxySkyCompany.Web.Host.Controllers
{
    public class AntiForgeryController : GalaxySkyCompanyControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
