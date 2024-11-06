using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;

namespace Talabat.API.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class BasketsController : APIBaseController
    {
        private readonly IBasketRepository _basketRepository;

        public BasketsController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        // GET or Recreate Basket
        //[HttpGet("{BasketId}")]
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetCustomerBasket(string BasketId)
        {
            var Basket = await _basketRepository.GetBasketAsync(BasketId);
            //if(Basket is null) return new CustomerBasket(BasketId);

            return Basket is null? new CustomerBasket(BasketId) : Ok(Basket);

        }


        // Update Or Create New Basket
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket Basket)
        {
            var CreatedOrUpdatedBasket = await _basketRepository.UpdateBasketAsync(Basket);

            if (CreatedOrUpdatedBasket is null) return BadRequest(new ApiResponse(400));

            return Ok(CreatedOrUpdatedBasket);
        }


        // Delete Basket
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket (string BasketId)
        {
            return await _basketRepository.DeleteBasketAsync(BasketId);
        }

    }
}
