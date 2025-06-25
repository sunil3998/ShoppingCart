using Microsoft.EntityFrameworkCore;

namespace ShoppingCart.Service.OrderApi.Utility
{
    public class BackendApiAuthenticationHttpClientHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BackendApiAuthenticationHttpClientHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Add("Authorization", token);
            }
            // Log the request for debugging purposes
            Console.WriteLine($"Request to {request.RequestUri} with Authorization token: {token}");
            return await base.SendAsync(request, cancellationToken);
        }

    }
}
