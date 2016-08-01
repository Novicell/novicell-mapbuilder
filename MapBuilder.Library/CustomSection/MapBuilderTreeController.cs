using Umbraco.Core.Services;
using System.Linq;
using System.Net.Http.Formatting;
using MapBuilder.Library.Helpers;
using MapBuilder.Library.Models.Poco;
using umbraco.BusinessLogic.Actions;
using Umbraco.Core;
using Umbraco.Core.Persistence;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Web.Trees;

namespace MapBuilder.Library.CustomSection
{
    [PluginController("NcMapBuilder")]
    [Tree("MapBuilder", "MapBuilderTree", "MapBuilder")]
    public class MapBuilderTreeController : TreeController
    {
        private static readonly Database Db = ApplicationContext.Current.DatabaseContext.Database;

        protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings)
        {
            var nodes = new TreeNodeCollection();
            string[] rootNodes = { "Maps", "Data" };
            var ts = Services.TextService;

            if (id == Constants.System.Root.ToInvariantString())
            {
                foreach (var rootNode in rootNodes)
                {
                    var icon = "icon-map";
                    var title = ts.Localize("ncmb/maps");
                    if (rootNode == "Data")
                    {
                        icon = "icon-settings";
                        title = ts.Localize("ncmb/data");
                    }

                    var hasChildren = false;
                    if (rootNode == "Maps")
                    {
                        var mapsItems = Db.ExecuteScalar<int>("SELECT COUNT(Id) FROM " + StaticHelper.GetMapsTableName());
                        hasChildren = mapsItems > 0;
                    }
                    else if (rootNode == "Data")
                    {
                        var dataItems = Db.ExecuteScalar<int>("SELECT COUNT(Id) FROM " + StaticHelper.GetDataTableName());
                        hasChildren = dataItems > 0;
                    }


                    var node = CreateTreeNode(rootNode, "-1", queryStrings, title, icon, hasChildren);

                    nodes.Add(node);
                }
            }
            else if (id == rootNodes[0])
            {
                var mapsItems = Db.Query<NovicellMapBuilderMapsModel>("SELECT * FROM " + StaticHelper.GetMapsTableName()).ToList();
                nodes.AddRange(mapsItems.Select(item => CreateTreeNode("map-" + item.Id.ToString(), rootNodes[0], queryStrings, item.Name, "icon-map-marker")));
            }
            else if (id == rootNodes[1])
            {
                var dataItems = Db.Query<NovicellMapBuilderDataModel>("SELECT * FROM " + StaticHelper.GetDataTableName()).ToList();
                nodes.AddRange(dataItems.Select(item => CreateTreeNode("data-" + item.Id.ToString(), rootNodes[1], queryStrings, item.Name, "icon-server-alt")));
            }

            return nodes;
        }


        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {
            var menu = new MenuItemCollection();

            if (id == Constants.System.Root.ToInvariantString() || id == "Data" || id == "Maps")
            {
                menu.Items.Add<ActionNew>("Create", "Creator", "Maps");
                menu.Items.Add<ActionRefresh>("Reload");
            }
            else
            {
                menu.Items.Add<ActionDelete>("Delete");
            }

            return menu;
        }
    }
}
