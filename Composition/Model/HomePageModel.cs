using MF.Composition.Config;
using MF.Composition.Fragments;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace MF.Composition.Model
{
    public class HomePageModel : LayoutBasePageModel
    {
        private readonly IFragmentProcessor fragmentProcessor;
        private readonly IOptions<FragmentsConfig> fragmentOptions;

        public HomePageModel(
            IFragmentProcessor fragmentProcessor,
            IOptions<FragmentsConfig> fragmentOptions)
            : base(fragmentProcessor, fragmentOptions)
        {
            this.fragmentProcessor = fragmentProcessor;
            this.fragmentOptions = fragmentOptions;
        }

        public override async Task<FragmentResult[]> LoadFragments(HttpContext httpContext, CancellationToken cancellationToken)
        {
            var result = await fragmentProcessor.Get(new FragmentRequest("home", fragmentOptions.Value.HomeUrl!), cancellationToken);
            return new[] { result };
        }
    }
}
