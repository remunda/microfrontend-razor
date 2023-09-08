namespace MF.Composition.Fragments
{
    public interface IFragmentProcessor
    {
        Task<FragmentResult> Get(FragmentRequest fragmentRequest, CancellationToken cancellationToken);
    }
}