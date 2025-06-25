﻿using ShoppingCart.Web.Models;

namespace ShoppingCart.Web.Service.IService
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true);
    }
}
