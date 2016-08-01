using System.Web.Mvc;
using MapBuilder.Website.Models;
using Umbraco.Web;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace MapBuilder.Website.Controllers
{
    public class HomeController : RenderMvcController
    {
        public override ActionResult Index(RenderModel renderModel)
        {
            var model = new PageHomeModel();

            model.MapId = model.Content.GetPropertyValue<int>("map");

            return CurrentTemplate(model);
        }
    }
}