using Microsoft.AspNetCore.Mvc;

namespace RollMaster.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json.Linq;
    using RollMaster.Models;
    using System.Net.Http.Headers;

    public class ChangelogController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.Add(
                new ProductInfoHeaderValue("AppName", "1.0"));

            var url = "https://api.github.com/repos/Cube011806/RollMaster/commits";

            var response = await client.GetStringAsync(url);
            var commits = JArray.Parse(response);

            var latest = commits[0];

            var model = new ChangelogViewModel
            {
                Message = latest["commit"]["message"].ToString(),
                Author = latest["commit"]["author"]["name"].ToString(),
                Date = DateTime.Parse(latest["commit"]["author"]["date"].ToString()),
                Url = latest["html_url"].ToString()
            };

            return View(model);
        }
    }

}
