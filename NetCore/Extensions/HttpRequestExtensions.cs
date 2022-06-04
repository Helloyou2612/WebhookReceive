using System.Text;
using Microsoft.AspNetCore.Http;

namespace NetCore.Extensions
{
    //Reference: https://weblog.west-wind.com/posts/2017/sep/14/accepting-raw-request-body-content-in-aspnet-core-api-controllers
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// Retrieve the raw body as a string from the Request.Body stream
        /// </summary>
        /// <param name="request">Request instance to apply to</param>
        /// <param name="encoding">Optional - Encoding, defaults to UTF8</param>
        /// <param name="inputStream">Optional - Pass in the stream to retrieve from. Other Request.Body</param>
        /// <returns></returns>
        public static async Task<string> GetRawBodyStringAsync(this HttpRequest request, Encoding? encoding = null, Stream? inputStream = null)
        {
            encoding ??= Encoding.UTF8;
            inputStream ??= request.Body;

            using (var reader = new StreamReader(inputStream, encoding))
                return await reader.ReadToEndAsync();
        }


        /// <summary>
        /// Retrieves the raw body as a byte array from the Request.Body stream
        /// </summary>
        /// <param name="request"></param>
        /// <param name="inputStream"></param>
        /// <returns></returns>
        public static async Task<byte[]> GetRawBodyBytesAsync(this HttpRequest request, Stream? inputStream = null)
        {
            inputStream ??= request.Body;

            using (var ms = new MemoryStream(2048))
            {
                await inputStream.CopyToAsync(ms);
                return ms.ToArray();
            }
        }
    }
}