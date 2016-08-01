using System.Collections.Generic;

namespace MapBuilder.Library.Models
{
    public class GeneratorModel
    {
        public string Alias { get; set; }
        public List<int> NodeIds { get; set; }
        public string TitleProperty { get; set; }
        public string CoordsProperty { get; set; }
    }
}
