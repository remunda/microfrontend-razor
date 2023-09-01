namespace MF.Composition.Fragments
{
    public interface IFragmentClient
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage fragmentHttpRequest, CancellationToken cancellation);
    }
}