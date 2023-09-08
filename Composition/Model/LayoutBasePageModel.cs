using MF.Composition.Fragments;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using MF.Composition.Config;
using Microsoft.AspNetCore.Html;

namespace MF.Composition.Model
{
    public abstract class LayoutBasePageModel : PageModel
    {
        private readonly IFragmentProcessor fragmentProcessor;
        private readonly IOptions<FragmentsConfig> fragmentOptions;

        public IDictionary<string, FragmentResult> Fragments { get; } = new Dictionary<string, FragmentResult>();

        public LayoutBasePageModel(
            IFragmentProcessor fragmentProcessor,
            IOptions<FragmentsConfig> fragmentOptions)
        {
            this.fragmentProcessor = fragmentProcessor ?? throw new ArgumentNullException(nameof(fragmentProcessor));
            this.fragmentOptions = fragmentOptions ?? throw new ArgumentNullException(nameof(fragmentOptions));
        }

        public IHtmlContent RenderFragment(string name)
        {
            var fragment = Fragments[name];
            return new FragmentStringContent(fragment);
        }

        public abstract Task<FragmentResult[]> LoadFragments(HttpContext httpContext, CancellationToken cancellationToken);

        private Task<FragmentResult[]> LoadFragmentsInternal(HttpContext httpContext, CancellationToken cancellationToken)
        {
            var headerTask = fragmentProcessor.Get(new FragmentRequest("header", fragmentOptions.Value.HeaderUrl!), cancellationToken);
            var footerTask = fragmentProcessor.Get(new FragmentRequest("footer", fragmentOptions.Value.FooterUrl!), cancellationToken);

            return Task.WhenAll(footerTask, headerTask);
        }

        public override async Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            var baseFragmentsTask = LoadFragmentsInternal(context.HttpContext, context.HttpContext.RequestAborted);
            var mainFragmentsTask = LoadFragments(context.HttpContext, context.HttpContext.RequestAborted);

            var fragments = await Task.WhenAll(baseFragmentsTask, mainFragmentsTask);

            foreach (var fragment in fragments.SelectMany(f => f))
            {
                Fragments.Add(fragment.Name, fragment);
            }

            await base.OnPageHandlerSelectionAsync(context);
        }

        public IActionResult OnGet() => ProcessRequest(HttpContext.RequestAborted);
        public IActionResult OnPost() => ProcessRequest(HttpContext.RequestAborted);

        private FragmentResult? GetMainFragment() => Fragments?.Values.FirstOrDefault(f => f.Request.IsMainFragment);

        private IActionResult ProcessRequest(CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
            {
                return StatusCode(499);
            }

            var fragment = GetMainFragment();
            var status = 200;

            if (fragment != null && fragment.StatusCode.HasValue)
            {
                status = (int)fragment.StatusCode.Value;                
            }

            if (status >= 404)
            {
                Response.StatusCode = status;
                //Render 404 page
                return StatusCode(status);
            }

            if (status >= 400)
            {
                return StatusCode(status);
            }

            if (status == 302)
            {
                return Redirect("");
            }

            return Page();
        }

    }
}
