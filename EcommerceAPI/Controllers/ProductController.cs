using AutoMapper;
using Ecommerce.Core.Entities;
using Ecommerce.Core.Entities.DTO;
using Ecommerce.Core.IRepositories;
using Ecommerce.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        //private readonly AppDbContext dbContext;
        //private readonly IGenericRepository<Products> genericRepository;

        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        public ApiResponse response;
        public IUnitOfWork<Products> UnitOfWork { get; }

        //public ProductController(AppDbContext dbContext , IGenericRepository<Products> genericRepository)
        public ProductController(IUnitOfWork<Products> unitOfWork , IMapper mapper , )
        {
            this.UnitOfWork = unitOfWork;
            response = new ApiResponse();
            this.mapper = mapper;
            //this.genericRepository = genericRepository;
            //this.productRepository = productRepository;
        }
        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetAll( )
        {
            var model = await UnitOfWork.productRepository.GetAll();
            var check = model.Any();
            if (check)
            {
                response.statusCode = System.Net.HttpStatusCode.OK;
                response.IsSuccess = check;
                var mappedProducts = mapper.Map<IEnumerable<Products> , IEnumerable<ProductDTO>>(model);
                response.Result = mappedProducts;
                return response;
            }
            else
            {
                response.ErroMessages = "not product found";
                response.statusCode = System.Net.HttpStatusCode.OK;
                response.IsSuccess = false;
                return response;
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse>> GetById(int id)
        {
            var model = await UnitOfWork.productRepository.GetById(id);
            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateProduct(Products model)
        {
            await UnitOfWork.productRepository.Create(model);
            UnitOfWork.save();
            return Ok(model);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateProduct(Products model)
        {
            UnitOfWork.productRepository.Update(model);
            await UnitOfWork.save();
            return Ok(model);
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            UnitOfWork.productRepository.Delete(id);
            await UnitOfWork.save();
            return Ok();
        }
        [HttpGet("Product/{cat_id}")]
        public async Task<ActionResult<ApiResponse>> GetProductByCatId(int cat_id)
        {
            var products = await UnitOfWork.productRepository.GetAllProductsByCategoryID(cat_id);
            var mappedProducts = mapper.Map<IEnumerable<Products> , IEnumerable<ProductDTO>>(products);
            return Ok(mappedProducts);
        }
    }
}
