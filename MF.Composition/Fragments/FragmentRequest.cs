namespace MF.Composition.Fragments
{
    public class FragmentRequest
    {
        public FragmentRequest(string name, Uri url)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            Name = name;
            Url = url ?? throw new ArgumentNullException(nameof(url));
        }

        public Uri Url { get; set; }
        public string Name { get; set; }
        public bool IsMainFragment { get; set; }
        public bool EnableCache { get; set; }
        public HttpMethod Method { get; internal set; }
    }
}
