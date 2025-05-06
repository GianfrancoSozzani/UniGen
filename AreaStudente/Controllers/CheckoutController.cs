using Microsoft.AspNetCore.Mvc;

namespace AreaStudente.Controllers
{
    public class CheckoutController : Controller
    {
        private string PaypalClientId { get; set; } = "";
        private string PaypalSecret { get; set; } = "";
        private string PaypalUrl { get; set; } = "";
        public CheckoutController(IConfiguration configuration)
        {
            PaypalClientId = configuration["PaypalSettings:ClientId"]!;
            PaypalSecret = configuration["PaypalSettings:Secret"]!;
            PaypalUrl = configuration["PaypalSettings:Url"]!;

        }
        public IActionResult Index()
        {
            return View();
        }

        private string GetPaypalAccessToken()
        {
            string accessToken = "";
            string url = PaypalUrl + "/v1/oauth2/token";
            using (var client = new HttpClient())
            {

            }
            return accessToken;
        }
    }
}
