using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Xml.Linq;
using MapBuilder.Library.Helpers;
using MapBuilder.Library.Models.Api;
using MapBuilder.Library.Models.Poco;
using Umbraco.Core;
using Umbraco.Core.Persistence;
using Umbraco.Web.WebApi;

namespace MapBuilder.Library.WebApi
{
    public class MapBuilderBackOfficeApiController : UmbracoAuthorizedApiController
    {
        private readonly Database _db = ApplicationContext.Current.DatabaseContext.Database;

        [HttpGet]
        public List<NovicellMapBuilderMapsModel> GetAllMaps()
        {
            using (_db)
            {
                var sql = string.Format("SELECT * FROM {0}", StaticHelper.GetMapsTableName());
                return _db.Query<NovicellMapBuilderMapsModel>(sql).ToList();
            }
        }

        [HttpPost]
        public ApiResult CreateNewMap(string name)
        {
            var result = new ApiResult();

            try
            {
                result.Success = true;
                using (_db)
                {
                    name = name.Trim();
                    var alias = StaticHelper.UppercaseWordsAndRemoveWhiteSpace(name.Replace("(", string.Empty).Replace(")", string.Empty));
                    var sql = string.Format("SELECT * FROM {0}", StaticHelper.GetMapsTableName());
                    var allMaps = _db.Query<NovicellMapBuilderMapsModel>(sql).ToList();
                    var mapName = allMaps.FirstOrDefault(x => x.Name == name);
                    var mapAlias = allMaps.FirstOrDefault(x => x.Alias == alias);
                    var tmpName = name;

                    var match = true;
                    var i = 1;
                    while (match)
                    {
                        if (mapName != null)
                        {
                            tmpName = name + " (" + i + ")";
                            alias = StaticHelper.UppercaseWordsAndRemoveWhiteSpace(tmpName.Replace("(", string.Empty).Replace(")", string.Empty));
                            mapName = allMaps.FirstOrDefault(x => x.Name == tmpName);
                            mapAlias = allMaps.FirstOrDefault(x => x.Alias == alias);

                            i++;
                        }
                        else if (mapAlias != null)
                        {
                            var tmpNameTwo = name + " (" + i + ")";
                            alias = StaticHelper.UppercaseWordsAndRemoveWhiteSpace(tmpNameTwo.Replace("(", string.Empty).Replace(")", string.Empty));
                            mapAlias = allMaps.FirstOrDefault(x => x.Alias == alias);
                            i++;
                        }
                        else
                        {
                            name = tmpName;
                            match = false;
                        }
                    }

                    var model = new NovicellMapBuilderMapsModel
                    {
                        Name = name,
                        Alias = alias,
                        InitialZoom = 7,
                        MinZoom = 7,
                        MaxZoom = 18,
                        Style = "[]",
                        DefaultIconStyle = "{\"url\":\"/App_Plugins/NcMapBuilder/Images/map-pin.png\",\"Width\":30,\"Height\":40}",
                        ClusterStyle = "[{\"anchor\": [0, 0], \"textColor\":\"white\",\"url\":\"/App_Plugins/NcMapBuilder/Images/m1.png\",\"width\":53,\"height\":52}," +
                                       "{\"anchor\": [0, 0], \"textColor\":\"white\",\"url\":\"/App_Plugins/NcMapBuilder/Images/m2.png\",\"width\":56,\"height\":55}," +
                                       "{\"anchor\": [0, 0], \"textColor\":\"white\",\"url\":\"/App_Plugins/NcMapBuilder/Images/m3.png\",\"width\":66,\"height\":65}," +
                                       "{\"anchor\": [0, 0], \"textColor\":\"white\",\"url\":\"/App_Plugins/NcMapBuilder/Images/m4.png\",\"width\":78,\"height\":77}," +
                                       "{\"anchor\": [0, 0], \"textColor\":\"white\",\"url\":\"/App_Plugins/NcMapBuilder/Images/m5.png\",\"width\":90,\"height\":89}]",
                        Center = "55.89736311199348,10.856373806359898",
                        UseGeoLocation = true,
                        UseInfoWindows = true,
                        InfoWindowWidth = 300,
                        InfoWindowName = "Default",
                        Draggable = true,
                        UseZoomControl = true,
                        UseMapTypeControl = true,
                        UseStreetViewControl = true,
                        ScrollWheelEnabled = true,
                        UseHybridMap = false
                    };

                    result.Data = _db.Insert(model);
                }
            }
            catch (Exception e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
            }

            return result;
        }

        [HttpGet]
        public ApiResult GetMap(int id)
        {
            var result = new ApiResult();

            try
            {
                result.Success = true;
                using (_db)
                {
                    var sql = string.Format("SELECT * FROM {0} WHERE id = {1}", StaticHelper.GetMapsTableName(), id);
                    result.Data = _db.Query<NovicellMapBuilderMapsModel>(sql).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
            }

            return result;
        }

        [HttpPost]
        public ApiResult SaveMap(NovicellMapBuilderMapsModel model)
        {
            var result = new ApiResult();

            try
            {
                result.Success = true;
                using (_db)
                {
                    _db.Save(model);
                }
            }
            catch (Exception e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
            }

            return result;
        }

        [HttpPost]
        public void RemoveMap(int id)
        {
            using (_db)
            {
                var sql = string.Format("DELETE FROM {0} WHERE id = {1}", StaticHelper.GetMapsTableName(), id);
                _db.Execute(sql);
            }
        }

        [HttpPost]
        public ApiResult CreateNewDataSource(string name)
        {
            var result = new ApiResult();

            try
            {
                result.Success = true;
                using (_db)
                {
                    var model = new NovicellMapBuilderDataModel
                    {
                        Name = name
                    };
                    result.Data = _db.Insert(model);
                }
            }
            catch (Exception e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
            }

            return result;
        }

        [HttpGet]
        public List<NovicellMapBuilderDataModel> GetAllDataSources()
        {
            var sql = string.Format("SELECT * FROM {0}", StaticHelper.GetDataTableName());
            return _db.Query<NovicellMapBuilderDataModel>(sql).ToList();
        }

        [HttpGet]
        public NovicellMapBuilderDataModel GetDataSource(int id)
        {
            using (_db)
            {
                var sql = string.Format("SELECT * FROM {0} WHERE id = {1}", StaticHelper.GetDataTableName(), id);
                return _db.Query<NovicellMapBuilderDataModel>(sql).FirstOrDefault();
            }
        }

        [HttpPost]
        public ApiResult SaveDataSource([FromBody] NovicellMapBuilderDataModel data)
        {
            var result = new ApiResult();

            try
            {
                result.Success = true;
                using (_db)
                {
                    _db.Save(data);
                }
            }
            catch (Exception e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
            }

            return result;
        }

        [HttpPost]
        public void RemoveDataSource([FromBody] NovicellMapBuilderDataModel data)
        {
            using (_db)
            {
                var sql = string.Format("SELECT * FROM {0} WHERE DataId = {1}", StaticHelper.GetMapsTableName(), data.Id);
                var mapsWithDateSource =
                    _db.Query<NovicellMapBuilderMapsModel>(sql).ToList();

                foreach (var map in mapsWithDateSource)
                {
                    map.DataId = -1;
                    _db.Save(map);
                }

                _db.Delete(data);
            }
        }

        [HttpGet]
        public List<string> GetDocumentTypes()
        {
            var service = ApplicationContext.Current.Services.ContentTypeService;
            return service.GetAllContentTypes().Select(x => x.Alias).ToList();
        }

        [HttpPost]
        public List<string> GetDocumentTypeProperties(string docTypeAlias)
        {
            if (docTypeAlias.IsNullOrWhiteSpace() || docTypeAlias == "null")
                return new List<string>();

            var service = ApplicationContext.Current.Services.ContentTypeService;
            var contentType = service.GetContentType(docTypeAlias);
            var propertyTypesAliasesList = new List<string> { "id", "name", "url" };
            propertyTypesAliasesList = propertyTypesAliasesList.Concat(contentType.PropertyTypes.Select(x => x.Alias)).ToList();
            return propertyTypesAliasesList;
        }

        [HttpPost]
        public List<string> LookUpCoords(string address)
        {
            if (address.IsNullOrWhiteSpace()) return new List<string>();

            var requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false", Uri.EscapeDataString(address));

            var request = WebRequest.Create(requestUri);
            var response = request.GetResponse();
            var xdoc = XDocument.Load(response.GetResponseStream());
            var list = new List<string>();

            var geocodeResponseElement = xdoc.Element("GeocodeResponse");
            if (geocodeResponseElement == null) return list;

            var resultElement = geocodeResponseElement.Element("result");
            if (resultElement == null) return list;

            var geometryElement = resultElement.Element("geometry");
            if (geometryElement == null) return list;

            var locationElement = geometryElement.Element("location");
            if (locationElement == null) return list;

            var latElement = locationElement.Element("lat");
            var lngElement = locationElement.Element("lng");
            if (latElement == null || lngElement == null) return list;

            list.Add(latElement.Value);
            list.Add(lngElement.Value);

            return list;
        }
    }
}
