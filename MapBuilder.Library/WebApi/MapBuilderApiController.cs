using System;
using System.Web.Http;
using MapBuilder.Library.Helpers;
using MapBuilder.Library.Models.Api;
using Umbraco.Web.WebApi;

namespace MapBuilder.Library.WebApi
{
    public class MapBuilderApiController : UmbracoApiController
    {
        private readonly MapModelHelper _prh = new MapModelHelper();

        [HttpPost]
        public ApiResult GetMapModel(ApiGetMapModel model)
        {
            var result = new ApiResult();

            try
            {
                result.Success = true;
                result.Data = _prh.GetMapModel(model);
            }
            catch (Exception e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
            }

            return result;
        }
    }
}
