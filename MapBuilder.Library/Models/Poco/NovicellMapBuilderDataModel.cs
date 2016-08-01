using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace MapBuilder.Library.Models.Poco
{
    [TableName("NovicellMapBuilderData")]
    [PrimaryKey("Id", autoIncrement = true)]
    public class NovicellMapBuilderDataModel
    {
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }
        public string Name { get; set; }
        [NullSetting(NullSetting = NullSettings.Null)]
        public string DocAlias { get; set; }
        [NullSetting(NullSetting = NullSettings.Null)]
        public string TitleProperty { get; set; }
        [NullSetting(NullSetting = NullSettings.Null)]
        public string CoordsProperty { get; set; }
    }
}
