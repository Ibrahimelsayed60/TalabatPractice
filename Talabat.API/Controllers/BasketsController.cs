using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.DTOs;
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
        private readonly IMapper _mapper;

        public BasketsController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
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
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto Basket)
        {
            var MappedBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(Basket);

            var CreatedOrUpdatedBasket = await _basketRepository.UpdateBasketAsync(MappedBasket);

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
