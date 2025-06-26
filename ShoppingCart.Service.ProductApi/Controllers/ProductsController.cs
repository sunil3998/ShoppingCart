using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Service.ProductApi.Data;
using ShoppingCart.Service.ProductApi.Models;
using ShoppingCart.Service.ProductApi.Models.Dto;
using System.Globalization;

namespace ShoppingCart.Service.ProductApi.Controllers
{
    [Route("api/Products")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ResponseDto _response;
        private readonly IMapper _mapper;
        public ProductsController(AppDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
            this._response = new ResponseDto();
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<ProductModel> objList = _context.Products.ToList();
                _response.Result = _mapper.Map<IEnumerable<ProductsDto>>(objList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public object Get(int id)
        {
            try
            {
                ProductModel obj = _context.Products.First(x => x.Id == id);
                _response.Result = _mapper.Map<ProductsDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        [Route("Create")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Post(ProductsDto productsDto)
        {
            try
            {
                ProductModel product = _mapper.Map<ProductModel>(productsDto);
                if (productsDto.Image != null)
                {
                    string extention = Path.GetExtension(productsDto.Image.FileName) == "" ? ".jpg" : Path.GetExtension(productsDto.Image.FileName);
                    string fileName = product.Id.ToString() + extention;
                    string filePath = @"wwwroot\ProductImages\" + fileName;
                    var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                    using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                    {
                        productsDto.Image.CopyTo(fileStream);
                    }
                    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                    product.ImageLocalPath = filePath;
                    product.ImageUrl = baseUrl + "/ProductImages/" + fileName;
                }
                else
                {
                    product.ImageUrl = "";
                }
                _context.Products.Add(product);
                _context.SaveChanges();
                _response.Result = _mapper.Map<ProductsDto>(product);
                _response.Message = "Product is added successfully.";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.ToString();
            }
            return _response;
        }

        [HttpPut]
        [Route("Update")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Put(ProductsDto productsDto)
        {
            try
            {
                ProductModel product = _mapper.Map<ProductModel>(productsDto);
                if (productsDto.Image != null)
                {
                    if(!string.IsNullOrEmpty(product.ImageLocalPath))
                    {
                        var oldFilePathDirectory=Path.Combine(Directory.GetCurrentDirectory(), product.ImageLocalPath);
                        FileInfo fileInfo = new FileInfo(oldFilePathDirectory);
                        if(fileInfo.Exists)
                        {
                            fileInfo.Delete();
                        }
                    }

                    string extention = Path.GetExtension(productsDto.Image.FileName) == "" ? ".jpg" : Path.GetExtension(productsDto.Image.FileName);
                    string fileName = product.Id.ToString() + extention;
                    string filePath = @"wwwroot\ProductImages\" + fileName;
                    var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                    using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                    {
                        productsDto.Image.CopyTo(fileStream);
                    }
                    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                    product.ImageLocalPath = filePath;
                    product.ImageUrl = baseUrl + "/ProductImages/" + fileName;
                }
                else
                {
                    product.ImageUrl = "";
                }
                _context.Products.Update(product);
                _context.SaveChanges();
                _response.Result = _mapper.Map<ProductsDto>(product);
                _response.Message = "Product is updated successfully.";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.ToString();
            }
            return _response;
        }

        [HttpDelete]
        [Route("Delete")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Delete(int id)
        {
            try
            {
                var product = _context.Products.First(x => x.Id == id);
                if (!string.IsNullOrEmpty(product.ImageLocalPath))
                {
                    var oldFilePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), product.ImageLocalPath);
                    FileInfo fileInfo = new FileInfo(oldFilePathDirectory);
                    if (fileInfo.Exists)
                    {
                        fileInfo.Delete();
                    }
                }
                _context.Products.Remove(product);
                _context.SaveChanges();
                _response.Result = _mapper.Map<ProductsDto>(product);
                _response.Message = "Product is deleted successfully.";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.ToString();
            }
            return _response;
        }
    }
}
