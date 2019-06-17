using System.Net.Http;
using ErgastApi.Abstractions;

namespace Infrastructure.DefaultCredentials
{
    /// <summary>
    /// Provides a HTTP Client that uses default credentials.
    /// </summary>
    public class DefaultCredentialsHttpClient : HttpClient, IHttpClient
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="DefaultCredentialsHttpClient"/> class.
        /// </summary>
        public DefaultCredentialsHttpClient()
            : base(new DefaultCredentialsClientHandler())
        {
        }
    }
}
