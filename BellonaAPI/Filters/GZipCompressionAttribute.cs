using System.Linq;
using System.Net.Http;
using System.IO;
using System.IO.Compression;
using System.Web.Http.Filters;

namespace BellonaAPI.Filters
{
    public class GZipCompressionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actContext)
        {
            var content = actContext.Response.Content;

            var encoding = actContext.Request.Headers.AcceptEncoding;
            if (encoding.Any() && encoding.Any(s => s.Value.ToLower() == "gzip"))
            {
                var bytes = content == null ? null : content.ReadAsByteArrayAsync().Result;
                var zlibbedContent = bytes == null ? new byte[0] :
                GZipCompressionHelper.DeflateByte(bytes);

                actContext.Response.Content = new ByteArrayContent(zlibbedContent);
                actContext.Response.Content.Headers.Remove("Content-Type");
                actContext.Response.Content.Headers.Add("Content-Encoding", "GZip");
                actContext.Response.Content.Headers.Add("Content-Type", "application/json");

                base.OnActionExecuted(actContext);
            }
        }
    }

    public class GZipCompressionHelper
    {
        public static byte[] DeflateByte(byte[] str)
        {
            if (str == null)
            {
                return null;
            }
            using (var output = new MemoryStream())
            {
                using (var compressor = new GZipStream(output, CompressionMode.Compress))
                {
                    compressor.Write(str, 0, str.Length);
                }
                return output.ToArray();
            }
        }
    }
}