using Microsoft.AspNetCore.Html;
using System.Text.Encodings.Web;

namespace MF.Composition.Fragments
{
    public class FragmentStringContent : IHtmlContent
    {
        private readonly string stringContent;

        public FragmentStringContent(FragmentResult fragment)
        {
            stringContent = fragment.Content;
        }

        public void WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            writer.WriteLine(stringContent);
        }
    }
}
