using MapBuilder.Library.Migration;
using Umbraco.Core;

namespace MapBuilder.Library.Events
{
    public class MapBuilderEvents : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            new MigrationHelper().HandleMapBuilderMigration();
        }
    }
}
