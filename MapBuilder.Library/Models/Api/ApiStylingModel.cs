using System.Collections.Generic;

namespace MapBuilder.Library.Models.Api
{
    public class ApiStylingModel
    {
        public string FeatureType { get; set; }
        public string ElementType { get; set; }
        public List<ApiStyleModel> Stylers { get; set; }
    }
}
