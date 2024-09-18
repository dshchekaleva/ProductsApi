using Application.Common;
using Application.Products.UseCases;
using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductsApi.DTOs.Models;

namespace ProductsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiVersion("1")]
    [ApiVersion("2")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        // GET api/<ProductsController>
        /// <summary>
        /// Get all products
        /// </summary>
        /// <param name="token">Dependency object for cancellation purposes.</param>
        [HttpGet]
        [MapToApiVersion("1")]
        public async Task<IActionResult> Get(CancellationToken token)
        {
            try
            {
                var result = await _mediator.Send(new GetProducts(), token);
                return Ok(result.Select(x => _mapper.Map<ProductModel>(x)));
            }
            catch (Exception)
            {
                return Problem("Could not get products");
            }
        }

        /// <summary>
        /// Get all products with pagination
        /// </summary>
        /// <param name="limit">Number of items on page. Default is 10</param>
        /// <param name="offset">Offset.</param>
        /// <param name="token">Dependency object for cancellation purposes.</param>
        [HttpGet]
        [MapToApiVersion("2")]
        public async Task<IActionResult> GetV2(int limit, int offset, CancellationToken token)
        {
            try
            {
                var result = await _mediator.Send(new GetProductsWithPagination(new Pagination(limit, offset)), token);
                return Ok(_mapper.Map<ProductsWithPaginationModel>(result));
            }
            catch (Exception)
            {
                return Problem("Could not get products");
            }
        }

        // GET api/<ProductsController>/5
        /// <summary>
        /// Get product by id 
        /// </summary>
        /// <param name="id">Product ID.</param>
        /// <param name="token">Dependency object for cancellation purposes.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken token)
        {
            try
            {
                var result = await _mediator.Send(new GetProductById { Id = id}, token);
                return Ok(_mapper.Map<ProductModel>(result));
            }
            catch (Exception)
            {
                return Problem($"Could not get product by Id {id}");
            }
        }

        // PUT api/<ProductsController>/5
        /// <summary>
        /// Update product description 
        /// </summary>
        /// <param name="id">Product ID.</param>
        /// <param name="description">Product description.</param>
        /// <param string="token">Dependency object for cancellation purposes.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string description, CancellationToken token)
        {
            try
            {
                var result = await _mediator.Send(new UpdateDescription(id, description), token);
                return Ok(_mapper.Map<ProductModel>(result));
            }
            catch (Exception)
            {
                return Problem($"Could not get product by Id {id}");
            }
        }
    }
}
