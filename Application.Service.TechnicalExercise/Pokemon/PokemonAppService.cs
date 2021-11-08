using Application.Adapter.Core;
using Application.DTO;
using Application.DTO.Core.GridDTO;
using Application.Interface.TechnicalExercise;
using Domain.Core;
using Domain.Core.ModelFilter;
using Domain.Pokemon.Repositories;
using Infrastructure.Transversal.Core.Accessor;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Service
{
    public class PokemonAppService : IPokemonAppService
    {

        readonly IPokemonRepository _pokemonRepository;
        private readonly IContextAccessor _contextAccessor;

        public PokemonAppService(IPokemonRepository pokemonRepository, IContextAccessor contextAccessor)
        {
            _pokemonRepository = pokemonRepository;
            _contextAccessor = contextAccessor;
        }

        public async Task<GridCommonDTO<PokemonDTO>> DeletePokemon(PokemonFilter oPokemonFilter)
        {
            var lst = await _pokemonRepository.DeletePokemon(oPokemonFilter);

            var oBaseFilter = new BaseFilter
            {
                searchText = "",
                columnOrderBy = "Id",
                desc = false,
                take = 0,
                skip = 0
            };

            int totalReg = lst.Count();

            return new GridCommonDTO<PokemonDTO>().CreateGridCommonDTO
                       (lst.ProjectToCollection<PokemonDTO>(), totalReg, oBaseFilter);
        }

        public async Task<GridCommonDTO<PokemonDTO>> GetPokemon(BaseFilter oBaseFilter)
        {
            var lst = await _pokemonRepository.GetPokemonListByBaseFilter(oBaseFilter);


            int totalReg = lst.Count();

            return new GridCommonDTO<PokemonDTO>().CreateGridCommonDTO
                       (lst.ProjectToCollection<PokemonDTO>(), totalReg, oBaseFilter);
        }

        public async Task<GridCommonDTO<PokemonDTO>> InsertPokemon(PokemonFilter oPokemonFilter)
        {
            var lst = await _pokemonRepository.InsertPokemon(oPokemonFilter);

            var oBaseFilter = new BaseFilter
            {
                searchText = "",
                columnOrderBy = "Id",
                desc = true,
                take = 0,
                skip = 0
            };

            int totalReg = lst.Count();

            return new GridCommonDTO<PokemonDTO>().CreateGridCommonDTO
                       (lst.ProjectToCollection<PokemonDTO>(), totalReg, oBaseFilter);
        }

        public async Task<GridCommonDTO<PokemonDTO>> UpdatePokemon(PokemonFilter oPokemonFilter)
        {
            var lst = await _pokemonRepository.UpdatePokemon(oPokemonFilter);

            var oBaseFilter = new BaseFilter
            {
                searchText = "",
                columnOrderBy = "Id",
                desc = false,
                take = 0,
                skip = 0
            };

            int totalReg = lst.Count();

            return new GridCommonDTO<PokemonDTO>().CreateGridCommonDTO
                       (lst.ProjectToCollection<PokemonDTO>(), totalReg, oBaseFilter);
        }
    }
}
