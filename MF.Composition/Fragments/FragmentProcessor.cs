namespace MF.Composition.Fragments
{
    public class FragmentProcessor : IFragmentProcessor
    {
        private readonly IFragmentClient fragmentClient;

        public FragmentProcessor(IFragmentClient fragmentClient)
        {
            this.fragmentClient = fragmentClient ?? throw new ArgumentNullException(nameof(fragmentClient));
        }

        public async Task<FragmentResult> Get(FragmentRequest fragmentRequest, CancellationToken cancellationToken)
        {
            var httpRequestMessage = new HttpRequestMessage(fragmentRequest.Method, fragmentRequest.Url);
            var result = await fragmentClient.SendAsync(httpRequestMessage, cancellationToken);

            var content = await result.Content.ReadAsStringAsync();

            return new FragmentResult(fragmentRequest.Name, content, result.StatusCode, fragmentRequest);
        }

    }
}
