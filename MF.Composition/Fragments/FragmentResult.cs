using System.Net;

namespace MF.Composition.Fragments
{
    public class FragmentResult
    {
        public FragmentRequest Request { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public HttpStatusCode? StatusCode { get; set; }
    }
}
