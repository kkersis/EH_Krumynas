﻿using AutoMapper;
using AutoWrapper.Wrappers;
using EKrumynas.DTOs.ShoppingCart;
using EKrumynas.Models;
using EKrumynas.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EKrumynas.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IMapper _mapper;

        public ShoppingCartController(IShoppingCartService shoppingCartService, IMapper mapper)
        {
            _shoppingCartService = shoppingCartService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ShoppingCartGetDto> GetById(int id)
        {
            try
            {
                var shoppingCart = await _shoppingCartService.GetCartById(id);
                var shoppingCartGetDto = _mapper.Map<ShoppingCartGetDto>(shoppingCart);

                return shoppingCartGetDto ?? new ShoppingCartGetDto();
            }
            catch (ArgumentException)
            {
                throw new ApiException(
                    statusCode: 400,
                    message: "Incorrect request data");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ShoppingCartGetDto> DeleteById(int id)
        {
            try 
            {
                var shoppingCart = await _shoppingCartService.DeleteById(id);
                var shoppingCartGetDto = _mapper.Map<ShoppingCartGetDto>(shoppingCart);

                return shoppingCartGetDto ?? new ShoppingCartGetDto();
            }
            catch (ArgumentException)
            {
                {
                    throw new ApiException(
                        statusCode: 400,
                        message: "Incorrect request data");
                }
            }
        }
        [HttpDelete]
        [Route("/{cartId}/{itemId}/{productType}")]
        public async Task<ShoppingCartGetDto> DeleteItemById(int cartId, int itemId, ProductType productType)
        {
            try
            {
                var shoppingCart = await _shoppingCartService.DeleteItemById(cartId, itemId, productType);
                var shoppingCartGetDto = _mapper.Map<ShoppingCartGetDto>(shoppingCart);

                return shoppingCartGetDto ?? new ShoppingCartGetDto();
            }
            catch (ArgumentException)
            {
                {
                    throw new ApiException(
                        statusCode: 400,
                        message: "Incorrect request data");
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(ShoppingCartAddDto shoppingCartAddDto)
        {
            try
            {
                ShoppingCart shoppingCart = _mapper.Map<ShoppingCart>(shoppingCartAddDto);
                var createdShoppingCart = await _shoppingCartService.CreateCart(shoppingCart);
                var shoppingCartGetDto = _mapper.Map<ShoppingCartGetDto>(createdShoppingCart);

                return Ok(shoppingCartGetDto);
            }
            catch
            {
                throw new ApiException(
                    statusCode: 400,
                    message: "Incorrect request data");
            }
        }

        [HttpPut]
        [Route("{cartId}/pot")]
        public async Task<IActionResult> AddPot(int cartId, PotCartItemAddDto potCartItemAddDto)
        {
            try
            {
                PotCartItem pot = _mapper.Map<PotCartItem>(potCartItemAddDto);
                var newShoppingCart = await _shoppingCartService.UpdateCart(cartId, pot);
                var shoppingCartGetDto = _mapper.Map<ShoppingCartGetDto>(newShoppingCart);

                return Ok(shoppingCartGetDto);
            }
            catch (ArgumentException)
            {
                throw new ApiException(
                    statusCode: 400,
                    message: "Incorrect request data");
            }
        }

        [HttpPut]
        [Route("{cartId}/plant")]
        public async Task<IActionResult> AddPlant(int cartId, PlantCartItemAddDto plantCartItemAddDto)
        {
            try
            {
                PlantCartItem plant = _mapper.Map<PlantCartItem>(plantCartItemAddDto);
                var newShoppingCart = await _shoppingCartService.UpdateCart(cartId, plant);
                var shoppingCartGetDto = _mapper.Map<ShoppingCartGetDto>(newShoppingCart);

                return Ok(shoppingCartGetDto);
            }
            catch (ArgumentException)
            {
                throw new ApiException(
                    statusCode: 400,
                    message: "Incorrect request data");
            }
        }

        [HttpPut]
        [Route("{cartId}/bouquet")]
        public async Task<IActionResult> AddBouquet(int cartId, BouquetCartItemAddDto bouquetCartItemAddDto)
        {
            try
            {
                BouquetCartItem bouquet = _mapper.Map<BouquetCartItem>(bouquetCartItemAddDto);
                var newShoppingCart = await _shoppingCartService.UpdateCart(cartId, bouquet);
                var shoppingCartGetDto = _mapper.Map<ShoppingCartGetDto>(newShoppingCart);

                return Ok(shoppingCartGetDto);
            }
            catch (ArgumentException)
            {
                throw new ApiException(
                    statusCode: 400,
                    message: "Incorrect request data");
            }
        }
    }
}
