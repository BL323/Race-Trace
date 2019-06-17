using System.Net;
using System.Net.Http;

namespace Infrastructure.DefaultCredentials
{
    /// <summary>
    /// Provides default client credentials to <see cref="HttpClient" /> class.
    /// </summary>
    public class DefaultCredentialsClientHandler : HttpClientHandler
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="DefaultCredentialsClientHandler"/> class.
        /// </summary>
        public DefaultCredentialsClientHandler() =>
            DefaultProxyCredentials = CredentialCache.DefaultCredentials;
    }
}
