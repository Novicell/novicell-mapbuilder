using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Script.Serialization;
using MapBuilder.Library.Models;
using MapBuilder.Library.Models.Api;
using MapBuilder.Library.Models.Poco;
using Umbraco.Core;
using Umbraco.Core.Persistence;
using Umbraco.Web;

namespace MapBuilder.Library.Helpers
{
    public class MapModelHelper
    {

        private readonly Database _db = ApplicationContext.Current.DatabaseContext.Database;

        internal ApiMapsModel GetMapModel(ApiGetMapModel apiModel)
        {
            using (_db)
            {
                var sql = string.Format("SELECT * FROM {0} WHERE Alias = '{1}'", StaticHelper.GetMapsTableName(), apiModel.Alias);
                var model = _db.Query<NovicellMapBuilderMapsModel>(sql).FirstOrDefault();
                if (model == null) return null;

                var jss = new JavaScriptSerializer();
                var apiResult = new ApiMapsModel
                {
                    ApiKey = model.ApiKey,
                    Center = model.Center.Split(',').Select(double.Parse).ToArray(),
                    CenterOnMarker = model.CenterOnMarker,
                    ClusterStyle = jss.Deserialize<List<ApiClusterStyleModel>>(model.ClusterStyle),
                    DefaultIconStyleModel = jss.Deserialize<ApiDefaultIconStyleModel>(model.DefaultIconStyle),
                    InfoWindowName = model.InfoWindowName,
                    InfoWindowWidth = model.InfoWindowWidth,
                    InitialZoom = model.InitialZoom,
                    MaxZoom = model.MaxZoom,
                    MinZoom = model.MinZoom,
                    Nodes = GetNodes(model.DataId, model.InfoWindowName, apiModel.NodeIds, apiModel.TitleProperty, apiModel.CoordsProperty),
                    Style = jss.Deserialize<List<ApiStylingModel>>(model.Style),
                    UseClustering = model.UseClustering,
                    UseGeoLocation = model.UseGeoLocation,
                    UseInfoWindows = model.UseInfoWindows,
                    Draggable = model.Draggable,
                    UseZoomControl = model.UseZoomControl,
                    UseMapTypeControl = model.UseMapTypeControl,
                    UseStreetViewControl = model.UseStreetViewControl,
                    ScrollWheelEnabled = model.ScrollWheelEnabled,
                    UseHybridMap = model.UseHybridMap
                };

                return apiResult;
            }
        }

        private List<ApiNodeModel> GetNodes(int id, string infoWindowName, List<int> ids = null, string titleProperty = "", string coordsProperty = "")
        {
            using (_db)
            {
                var sql = string.Format("SELECT * FROM {0} WHERE Id = {1}", StaticHelper.GetDataTableName(), id);
                var dataModel = _db.Query<NovicellMapBuilderDataModel>(sql).FirstOrDefault();

                var items = new List<ApiNodeModel>();

                if (dataModel == null) return items;

                var umbraco = new UmbracoHelper(UmbracoContext.Current);
                var nodes = ids != null && ids.Any() ? umbraco.TypedContent(ids) : umbraco.TypedContentAtRoot().DescendantsOrSelf(dataModel.DocAlias).ToList().Where(x => !x.IsDraft && x.IsVisible());

                var isName = titleProperty.ToLowerInvariant() == "name" ||
                             dataModel.TitleProperty.ToLowerInvariant() == "name";

                var isId = titleProperty.ToLowerInvariant() == "id" ||
                             dataModel.TitleProperty.ToLowerInvariant() == "id";

                var isUrl = titleProperty.ToLowerInvariant() == "url" ||
                             dataModel.TitleProperty.ToLowerInvariant() == "url";

                foreach (var node in nodes)
                {
                    var model = new ApiNodeModel();

                    var title = isName ? node.Name : (isId ? node.Id.ToString() : (isUrl ? node.Url : node.GetPropertyValue<string>(!string.IsNullOrWhiteSpace(titleProperty)
                            ? titleProperty
                            : dataModel.TitleProperty)));

                    model.Id = node.Id;
                    model.Title = title;
                    model.Coordinates =
                        node.GetPropertyValue<string>(!string.IsNullOrWhiteSpace(coordsProperty)
                            ? coordsProperty
                            : dataModel.CoordsProperty).Split(',').Select(d => double.Parse(d, CultureInfo.InvariantCulture)).ToArray();
                    model.InfoWindowContent =
                        new RazorHelper().RenderPartialView("NcMapBuilder/InfoWindows/" + infoWindowName,
                            new InfoWindowModel {Id = node.Id});

                    items.Add(model);
                }

                return items;
            }
        }
    }
}
