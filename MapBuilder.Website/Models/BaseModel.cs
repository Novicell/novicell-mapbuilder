using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace MapBuilder.Website.Models
{
    public class BaseModel : RenderModel
    {
        public BaseModel() : base(UmbracoContext.Current.PublishedContentRequest.PublishedContent) { }
    }
}