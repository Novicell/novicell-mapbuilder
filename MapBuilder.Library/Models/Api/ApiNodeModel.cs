using System.Web.Mvc;

namespace MapBuilder.Library.Models.Api
{
    public class ApiNodeModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double[] Coordinates { get; set; }
        public string InfoWindowContent { get; set; }
    }
}
