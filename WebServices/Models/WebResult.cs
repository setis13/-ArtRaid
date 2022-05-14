namespace ArtRaid.WebServices.Models {
    public class WebResult {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class WebResult<T> : WebResult {
        public T Data { get; set; }
    }
}
