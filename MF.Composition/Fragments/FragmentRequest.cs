namespace MF.Composition.Fragments
{
    public class FragmentRequest
    {
        public FragmentRequest(string name, Uri url, HttpMethod? method = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            Name = name;
            Url = url ?? throw new ArgumentNullException(nameof(url));
            Method = method ?? HttpMethod.Get;
        }

        public Uri Url { get; private set; }
        public HttpMethod Method { get; private set; }
        public string Name { get; private set; }
        public bool IsMainFragment { get; set; }
        public bool EnableCache { get; set; }
    }
}
