using System.Net;

namespace MF.Composition.Fragments
{
    public class FragmentResult
    {
        public FragmentRequest Request { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public HttpStatusCode? StatusCode { get; set; }

        public FragmentResult(string name, string content, HttpStatusCode statusCode, FragmentRequest request)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
            }

            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentException($"'{nameof(content)}' cannot be null or empty.", nameof(content));
            }

            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            Name = name;
            Content = content;
            StatusCode = statusCode;
            Request = request;
        }
    }
}
