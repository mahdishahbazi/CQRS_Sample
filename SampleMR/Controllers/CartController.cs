using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleMR.CQRS.AddToCartCommand;
using SampleMR.CQRS.DeleteItemInCartCommand;
using SampleMR.CQRS.EmptyCartCommand;
using SampleMR.CQRS.GetUserCartQuery;
using SampleMR.CQRS.UpdateItemInCartCommand;
using SampleMR.Models;

namespace SampleMR.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        public CartController(IMediator mediator, 
            ILogger<CartController> logger)
        {
            Mediator = mediator;
            Logger = logger;
        }

        public IMediator Mediator { get; }
        public ILogger<CartController> Logger { get; }

        [HttpPost("[action]")]
        public async Task<ActionResult<ApiResponse>> AddToCart(AddToCartCommand command)
        {
            try
            {
                Guid itemId = await Mediator.Send(command);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "AddToCart");
                return BadRequest("Error adding to the cart");
            }
            return new ApiResponse
            {
                Message = "item is added to the cart successfully"
            };
        }


        [HttpPut("[action]")]
        public async Task<ActionResult<ApiResponse>> UpdateItem(UpdateItemInCartCommand request)
        {
            //remember in most scenarios, you should get user id from context not from the api
            var userId = 10;// in real world you get this from the context
            request.UserId = userId;
            try
            {
                await Mediator.Send(request);
                return new ApiResponse
                {
                    Message = "item is updated successfully"
                };
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "UpdateItem");
                return BadRequest("Error updating item");
            }
        }

        [HttpDelete("[action]")]
        public async Task<ActionResult> EmptyMyCart()
        {
            //remember in most scenarios, you should get user id from context not from the api
            var userId = 10;// in real world you get this from the context
            try
            {
                await Mediator.Send(new EmptyCartCommand { UserId = userId });
                return Ok();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Empty My Cart");
                return BadRequest("Error empty cart");
            }
        }

        [HttpDelete("[action]")]
        public async Task<ActionResult> DeleteItem(Guid itemId)
        {
            //remember in most scenarios, you should get user id from context not from the api
            var userId = 10;// in real world you get this from the context
            try
            {
                await Mediator.Send(new DeleteItemInCartCommand { UserId = userId, ItemId = itemId });
                return Ok();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "DeleteItem");
                return BadRequest("Error Delete Item");
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IList<CartItem>>> GetMyCart()
        {
            //remember in most scenarios, you should get user id from context not from the api
            var userId = 10;// in real world you get this from the context
            try
            {
                IList<CartItem> result = await Mediator.Send(new GetUserCartQuery { UserId = userId });
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "GetUserCart");
                return BadRequest("Error getting user cart");
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IList<CartItem>>> GetUserCart(
            [FromQuery]GetUserCartQuery query)
        {
            try
            {
                IList<CartItem> res = await
                 Mediator.Send(query);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "GetUserCart", query);
                return BadRequest("Error getting user cart");
            }

        }


    }
}