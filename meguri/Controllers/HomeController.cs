using Meguri.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.NetworkInformation;
using Microsoft.Extensions.Localization;
using Meguri.Resources;

namespace Meguri.Controllers {
    [AllowAnonymous]
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;

        // IStringLocalizer<SharedResource> を依存関係注入（DI）する。
        private readonly IStringLocalizer<SharedResource> _localizer;

        // loggerを注入する。
        // 共通リソースのLocalizerを注入する。
        public HomeController(
            ILogger<HomeController> logger,
            IStringLocalizer<SharedResource> localizer
        ) {
            _logger = logger;
            _localizer = localizer;
        }

        public IActionResult Index() {
            string serviceName = _localizer["ServiceName"];
            string tagline = _localizer["Tagline"];

            ViewData["Title"] = $"{serviceName}｜{tagline}";

            return View();
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
