namespace MapBuilder.Library.Models.Api
{
    public class ApiResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public object Data { get; set; }
    }
}
