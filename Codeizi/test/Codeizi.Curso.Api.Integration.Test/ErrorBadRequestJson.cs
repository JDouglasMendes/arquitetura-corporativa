namespace Codeizi.Curso.Api.Integration
{
    public class ErrorBadRequestJson
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public string TraceId { get; set; }
        public dynamic Errors { get; set; }
    }

#pragma warning disable SA1402 // File may only contain a single type
    public class Error
#pragma warning restore SA1402 // File may only contain a single type
    {
        public dynamic Root { get; set; }

    }

}
