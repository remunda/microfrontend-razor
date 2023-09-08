namespace MF.Composition.Fragments
{
    public class FragmentClient : IFragmentClient
    {
        private readonly HttpClient httpClient;

        public FragmentClient(HttpClient httpClient)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage fragmentHttpRequest, CancellationToken cancellation)
        {
            return await httpClient.SendAsync(fragmentHttpRequest, HttpCompletionOption.ResponseHeadersRead, cancellation);
        }
    }
}
