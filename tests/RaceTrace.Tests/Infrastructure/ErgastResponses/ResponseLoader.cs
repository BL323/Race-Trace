using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ErgastApi.Abstractions;
using ErgastApi.Client;
using Moq;
using Moq.Protected;

namespace RaceTrace.Tests.Infrastructure
{
    public class ErgastClientGenerator
    {
        public static IErgastClient ErgastClientWithResponseFromFile(string dirPath, string filePath)
        {
            var response = GetResponse(dirPath, filePath);
            var httpClient = HttpClient(response);
            return new ErgastClient {HttpClient = httpClient};
        }

        private static string GetResponse(string dirPath, string filePath)
        {
            var fPath = $"{dirPath}\\{filePath}";


            var path = Path.IsPathRooted(fPath)
                ? fPath
                : Path.GetRelativePath(Directory.GetCurrentDirectory(), fPath);

            if (!File.Exists(path))
                throw new ArgumentException($"Could not find file at path: {path}");

            // Load the file
            var fileData = File.ReadAllText(path);
            return fileData;
        }


        private static IHttpClient HttpClient(string response)
        {
            var mock = new Mock<HttpMessageHandler>();
            mock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(response)
                })
                .Verifiable();

            return new TestingHttpClient(new HttpClient(mock.Object));
        }
    }
}
