using Domain.Core;
using Domain.Core.ModelFilter;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Pokemon.Repositories
{
    public interface IPokemonRepository //: IRepository<Entities.Pokemon>
    {

        /// <summary>
        /// Returns Pokemon list by BaseFilter
        /// </summary>
        /// <param name="oBaseFilter"></param>
        /// <returns></returns>
        public Task<IEnumerable<Entities.Pokemon>> GetPokemonListByBaseFilter(BaseFilter oBaseFilter);

        /// <summary>
        /// Inserts existing Pokemon
        /// </summary>
        /// <param name="oPokemonFilter"></param>
        /// <returns></returns>
        public Task<IEnumerable<Entities.Pokemon>> InsertPokemon(PokemonFilter oPokemonFilter);

        /// <summary>
        /// Inserts new Pokemon
        /// </summary>
        /// <param name="oPokemonFilter"></param>
        /// <returns></returns>
        public Task<IEnumerable<Entities.Pokemon>> UpdatePokemon(PokemonFilter oPokemonFilter);

        /// <summary>
        /// Deletes existing Pokemon
        /// </summary>
        /// <param name="oBaseFilter"></param>
        /// <returns></returns>
        public Task<IEnumerable<Entities.Pokemon>> DeletePokemon(PokemonFilter pokemonFilter);


        /// <summary>
        /// Returns entire Pokemon list
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public List<Entities.Pokemon> GetPokemonList(BaseFilter oBaseFilter);
    }
}
