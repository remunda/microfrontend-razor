using MF.Composition.Config;
using MF.Composition.Fragments;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace MF.Composition.Model
{
    public class ContentPageModel : LayoutBasePageModel
    {
        private readonly IFragmentProcessor fragmentProcessor;
        private readonly IOptions<FragmentsConfig> fragmentOptions;

        public ContentPageModel(
            IFragmentProcessor fragmentProcessor,
            IOptions<FragmentsConfig> fragmentOptions)
            : base(fragmentProcessor, fragmentOptions)
        {
            this.fragmentProcessor = fragmentProcessor;
            this.fragmentOptions = fragmentOptions;
        }

        public override async Task<FragmentResult[]> LoadFragments(HttpContext httpContext, CancellationToken cancellationToken)
        {
            var result = await fragmentProcessor.Get(new FragmentRequest("content", fragmentOptions.Value.ContentUrl!), cancellationToken);
            return new[] { result };
        }
    }
}
