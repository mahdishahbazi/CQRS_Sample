using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuiltIn.CQRS;
using BuiltIn.CQRS.Base;
using BuiltIn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BuiltIn.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ILogger<CartController> _logger;
        private readonly IRequestsBus _requestBus;
        public CartController(ILogger<CartController> logger, 
                            IRequestsBus requestBus)
        {
            _requestBus = requestBus;
            _logger = logger;
        }

        /// <summary>
        /// Creates a Cart Item.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST 
        ///     {
        ///        "item": "coffee",
        ///        "quantity": 4
        ///     }
        ///
        /// </remarks>
        /// <param name="command"></param>
        /// <returns>A newly created AddToCart</returns>
        [HttpPost]
        public ActionResult<ApiResponse> AddToCart(AddToCartCommand command)
        {
            try
            {
                _requestBus.Send(command);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AddToCart", command);
                return BadRequest("Error adding to the cart");
            }
            return new ApiResponse
            {
                Message = "item is added to the cart successfully"
            };
        }

        /// <summary>
        /// Get User's Cart items.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="query"></param>
        /// <returns>A newly created GetUserCart</returns>
        [HttpGet]
        public ActionResult<ApiResponse> GetUserCart(
            [FromQuery]GetUserCartQuery query)
        {
            try
            {
                List<CartItem> res = 
                 _requestBus.Send<List<CartItem>,GetUserCartQuery>(query);
                return new ApiResponse<List<CartItem>>
                {
                    Result = res
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AddToCart", query);
                return BadRequest("Error getting user cart");
            }

        }
    }
}