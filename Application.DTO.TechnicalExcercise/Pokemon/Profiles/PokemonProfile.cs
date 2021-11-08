using AutoMapper;
using Domain.Pokemon.Entities;

namespace Application.DTO.TechnicalExcercise.Profiles
{
    public class PokemonProfile : Profile
    {

        public PokemonProfile()
        {
            
            CreateMap<Pokemon, PokemonDTO>();

        }


    }
}
