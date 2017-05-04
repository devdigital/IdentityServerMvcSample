using System.Web.Mvc;

namespace IdentityServerMvcSample.Mvc.Controllers
{
    using System.Security.Claims;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }

        [Authorize]
        public ActionResult About()
        {            
            return this.View((this.User as ClaimsPrincipal).Claims);
        }

        public ActionResult Contact()
        {
            this.ViewBag.Message = "Your contact page.";
            return this.View();
        }
    }
}