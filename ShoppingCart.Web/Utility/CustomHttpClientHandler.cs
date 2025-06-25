namespace ShoppingCart.Web.Utility
{
    public class CustomHttpClientHandler : HttpClientHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            return base.SendAsync(request, cancellationToken);
        }
    }
}
