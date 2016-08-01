using System.Collections.Generic;

namespace MapBuilder.Library.Models.Api
{
    public class ApiMapsModel
    {
        public string ApiKey { get; set; }
        public double[] Center { get; set; }
        public bool CenterOnMarker { get; set; }
        public List<ApiClusterStyleModel> ClusterStyle { get; set; }
        public ApiDefaultIconStyleModel DefaultIconStyleModel { get; set; }
        public List<ApiStylingModel> Style { get; set; }
        public string InfoWindowName { get; set; }
        public int InfoWindowWidth { get; set; }
        public int InitialZoom { get; set; }
        public int MinZoom { get; set; }
        public int MaxZoom { get; set; }
        public bool UseClustering { get; set; }
        public bool UseGeoLocation { get; set; }
        public bool UseInfoWindows { get; set; }
        public bool Draggable { get; set; }
        public bool UseZoomControl { get; set; }
        public bool UseMapTypeControl { get; set; }
        public bool UseStreetViewControl { get; set; }
        public bool ScrollWheelEnabled { get; set; }
        public bool UseHybridMap { get; set; }
        public List<ApiNodeModel> Nodes { get; set; }
    }
}
