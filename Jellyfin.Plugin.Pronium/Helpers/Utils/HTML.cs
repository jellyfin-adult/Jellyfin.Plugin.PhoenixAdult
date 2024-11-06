using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Pronium.Helpers.Utils
{
    internal static class HTML
    {
        public static async Task<HtmlNode> ElementFromURL(string url, CancellationToken cancellationToken, IDictionary<string, string> headers = null, IDictionary<string, string> cookies = null)
        {
            HtmlNode html = new HtmlDocument().DocumentNode;
            var http = await HTTP.Request(url, cancellationToken, headers, cookies).ConfigureAwait(false);
            if (http.IsOK)
            {
                html = ElementFromStream(http.ContentStream);
            }

            return html;
        }

        public static HtmlNode ElementFromStream(Stream data)
        {
            var html = new HtmlDocument();
            html.Load(data);

            return html.DocumentNode;
        }
    }
}
