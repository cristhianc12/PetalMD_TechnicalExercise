using Application.Interface.TechnicalExercise;
using Domain.Core;
using Domain.Core.ModelFilter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DistributedServices.PetalMD.RestApi.Controllers.Pokemon
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PokemonController : ControllerBase
    {

        private readonly IPokemonAppService _pokemonAppService;


        public PokemonController(IPokemonAppService pokemonAppService)
        {
            _pokemonAppService = pokemonAppService;
        }

        /// <summary>
        /// Returns POKEMON dataset based on filters
        /// </summary>
        /// <remarks>
        /// POST
        /// {
        ///"columnOrderBy": "Name",
        ///"searchText": "",
        ///"state": 3,
        ///"desc": true,
        ///"take": 0,
        ///"skip": 0
        ///}
        /// </remarks>
        /// <param name="Filter"></param>
        /// <returns>Pokemon list </returns>
        [HttpGet("pokemon")]
        public async Task<IActionResult> GetPokemonListByFilter([FromBody] BaseFilter Filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _pokemonAppService.GetPokemon(Filter);

            return new ObjectResult(result);
        }

        [HttpPost("pokemon")]
        public async Task<IActionResult> InsertPokemon([FromBody] PokemonFilter oPokemonFilter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _pokemonAppService.InsertPokemon(oPokemonFilter);

            return new ObjectResult(result);
        }

        [HttpPatch("pokemon")]
        public async Task<IActionResult> UpdatePokemon([FromBody] PokemonFilter oPokemonFilter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _pokemonAppService.UpdatePokemon(oPokemonFilter);

            return new ObjectResult(result);
        }

        [HttpDelete("pokemon")]
        public async Task<IActionResult> DeletePokemon([FromBody] PokemonFilter oPokemonFilter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _pokemonAppService.DeletePokemon(oPokemonFilter);

            return new ObjectResult(result);
        }

    }
}
