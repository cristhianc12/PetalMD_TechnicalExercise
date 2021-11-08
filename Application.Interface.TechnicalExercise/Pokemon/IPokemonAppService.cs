using Application.DTO;
using Application.DTO.Core.GridDTO;
using Domain.Core;
using Domain.Core.ModelFilter;
using System.Threading.Tasks;

namespace Application.Interface.TechnicalExercise
{
    public interface IPokemonAppService
    {

        /// <summary>
        /// Returns Pokemon List
        /// </summary>
        /// <param name="oBaseFilter"></param>
        /// <returns></returns>
        Task<GridCommonDTO<PokemonDTO>> GetPokemon(BaseFilter oBaseFilter);

        /// <summary>
        /// Inserts new Pokemon
        /// </summary>
        /// <param name="oPokemonFilter"></param>
        /// <returns></returns>
        Task<GridCommonDTO<PokemonDTO>> InsertPokemon(PokemonFilter oPokemonFilter);

        /// <summary>
        /// Updates existing Pokemon
        /// </summary>
        /// <param name="oPokemonFilter"></param>
        /// <returns></returns>
        Task<GridCommonDTO<PokemonDTO>> UpdatePokemon(PokemonFilter oPokemonFilter);

        /// <summary>
        /// Deletes existing Pokemon
        /// </summary>
        /// <param name="oPokemonFilter"></param>
        /// <returns></returns>
        Task<GridCommonDTO<PokemonDTO>> DeletePokemon(PokemonFilter oPokemonFilter);

    }
}
