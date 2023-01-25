namespace NoteApp.Common
{
    public class Response<T>
    {
        public bool Status { get; set; }
        public string? Message { get; set; } = "Success";
        public T? Results { get; set; }
        public string[]? Errors { get; set; }
        public MetaData? Metadata { get; set; }
    }
}
