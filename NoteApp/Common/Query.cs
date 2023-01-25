namespace NoteApp.Common
{
    public class Query
    {
        public string? Sort { get; set; }
        public string? Keyword { get; set; }
        public bool? Desc { get; set; }
        public int? Page { get; set; }
        public int? Limit { get; set; }
    }
}
