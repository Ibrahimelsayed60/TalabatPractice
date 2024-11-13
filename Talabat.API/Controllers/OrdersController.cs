using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.API.DTOs;
using Talabat.API.Errors;
using Talabat.Core.Entities.OrderAggregate;
using Talabat.Core.Services;

namespace Talabat.API.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class OrdersController : APIBaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        // Create Order
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var MappedAddress = _mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);
            var Order = await _orderService.CreateOrderAsync(BuyerEmail, orderDto.BasketId, orderDto.DeliveryMethodId, MappedAddress);
            if (Order is null) return BadRequest(new ApiResponse(400, "There is a problem with your Order"));
            return Ok(Order);

        }


        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IReadOnlyList<OrderToReturnDto>), StatusCodes.Status200OK)]
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrders()
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var Orders = await _orderService.GetOrdersForSpecificUserAsync(BuyerEmail);
            if (Orders is null) return NotFound(new ApiResponse(404, "There is no Orders for this user"));

            var MappedOrders = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(Orders);

            return Ok(MappedOrders);
        }


        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdForUser(int id)
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var order = await _orderService.GetOrderByIdForSpecificUserAsync(BuyerEmail, id);
            if (order is null) return NotFound(new ApiResponse(404, $"There is no order with {id} for this user"));
            var MappedOrder = _mapper.Map<Order, OrderToReturnDto>(order);
            return Ok(MappedOrder);

        }

    }
}
