using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace MapBuilder.Library.Models.Poco
{
    [TableName("NovicellMapBuilderMaps")]
    [PrimaryKey("Id", autoIncrement = true)]
    public class NovicellMapBuilderMapsModel
    {
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        [NullSetting(NullSetting = NullSettings.Null)]
        public string ApiKey { get; set; }
        [NullSetting(NullSetting = NullSettings.Null)]
        public int DataId { get; set; }
        public int InitialZoom { get; set; }
        public int MinZoom { get; set; }
        public int MaxZoom { get; set; }
        [Length(3000)]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string Style { get; set; }
        public bool UseClustering { get; set; }
        [Length(2000)]
        public string ClusterStyle { get; set; }
        [Length(2000)]
        public string DefaultIconStyle { get; set; }
        public bool CenterOnMarker { get; set; }
        public string Center { get; set; }
        public bool UseGeoLocation { get; set; }
        public bool UseInfoWindows { get; set; }
        public int InfoWindowWidth { get; set; }
        public string InfoWindowName { get; set; }
        public bool Draggable { get; set; }
        public bool UseZoomControl { get; set; }
        public bool UseMapTypeControl { get; set; }
        public bool UseStreetViewControl { get; set; }
        public bool ScrollWheelEnabled { get; set; }
        public bool UseHybridMap { get; set; }
    }
}
