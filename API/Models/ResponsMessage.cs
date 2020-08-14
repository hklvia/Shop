namespace API.Models
{
    public class ResponsMessage<T>
    {
        public T Data { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public int Total { get; set; }
    }
}