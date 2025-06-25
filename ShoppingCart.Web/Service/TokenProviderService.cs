using ShoppingCart.Web.Service.IService;
using ShoppingCart.Web.Utility;

namespace ShoppingCart.Web.Service
{
    public class TokenProviderService : ITokenProviderService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TokenProviderService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public void ClearToken()
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Delete(StaticData.TokenCookie);//, new CookieOptions()
            //{
            //    HttpOnly = true,
            //    Expires = DateTimeOffset.UtcNow.AddMilliseconds(10)
            //});
        }

        public string? GetToken()
        {
            string? token = null;
            bool? hasToken = _httpContextAccessor.HttpContext?.Request.Cookies.TryGetValue(StaticData.TokenCookie, out token);
            return hasToken is true ? token : null;
        }


        public void SetToken(string token)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Append(StaticData.TokenCookie, token);
            //{
            //    HttpOnly = true,
            //    Expires = DateTimeOffset.UtcNow.AddMilliseconds(10)
            //});
        }
    }
}
