using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using MapBuilder.Library.Models;

namespace MapBuilder.Library.ExtensionMethods
{
    public static class PartialRenderExtension
    {
        /// <summary>
        /// Generate map partial - Using the datasource only.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="mapAlias">The alias for the map.</param>
        /// <returns></returns>
        public static MvcHtmlString RenderMap(this HtmlHelper htmlHelper, string mapAlias)
        {
            var model = new GeneratorModel
            {
                Alias = mapAlias
            };

            return htmlHelper.Partial("NcMapBuilder/Generator", model);
        }

        /// <summary>
        /// Generate map partial with only nodeIds used for markers - Important to note, that they should be included in the datasource for the map.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="mapAlias">The alias for the map.</param>
        /// <param name="nodeIds">A list of node ids (List&lt;int&gt;)</param>
        /// <returns></returns>
        public static MvcHtmlString RenderMap(this HtmlHelper htmlHelper, string mapAlias, List<int> nodeIds)
        {
            var model = new GeneratorModel
            {
                Alias = mapAlias,
                NodeIds = nodeIds
            };

            return htmlHelper.Partial("NcMapBuilder/Generator", model);
        }

        /// <summary>
        /// Generate map partial with only nodeIds used for markers - Important to note, that each node should be using the same titleProperty and coordsProperty for this to work.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="mapAlias">The alias for the map.</param>
        /// <param name="nodeIds">A list of node ids (List&lt;int&gt;)</param>
        /// <param name="titleProperty">The property alias for the title on the nodes.</param>
        /// <param name="coordsProperty">The property alias for the coordinates on the nodes.</param>
        /// <returns></returns>
        public static MvcHtmlString RenderMap(this HtmlHelper htmlHelper, string mapAlias, List<int> nodeIds,
            string titleProperty, string coordsProperty)
        {
            var model = new GeneratorModel
            {
                Alias = mapAlias,
                NodeIds = nodeIds,
                TitleProperty = titleProperty,
                CoordsProperty = coordsProperty
            };

            return htmlHelper.Partial("NcMapBuilder/Generator", model);
        }

        /// <summary>
        /// Generates scripts. Used in generator.cshtml file - Should not generally be used.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static MvcHtmlString RenderMapScripts(this HtmlHelper htmlHelper)
        {
            return htmlHelper.Partial("NcMapBuilder/Scripts", null, null);
        }
    }
}
