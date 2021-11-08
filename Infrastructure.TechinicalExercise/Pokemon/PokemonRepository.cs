using Domain.Core;
using Domain.Core.ModelFilter;
using Domain.Pokemon.Entities;
using Domain.Pokemon.Repositories;
using Infrastructure.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.TechinicalExercise
{
    public class PokemonRepository : /*Repository<Pokemon>,*/ IPokemonRepository
    {
        public async Task<IEnumerable<Pokemon>> DeletePokemon(PokemonFilter oPokemonFilter)
        {
            var oBaseFilter = new BaseFilter
            {
                searchText = "",
                columnOrderBy = "Id",
                desc = false,
                take = 0,
                skip = 0
            };

            var lst = GetPokemonListNoFilter().OrderBy(x => x.Id).ToList();

            var el = lst.Find(x => x.Id == oPokemonFilter.Id);

            if (oPokemonFilter.Id > 0 && el != null) lst.Remove(el);

            //Refresh CSV file
            if (oPokemonFilter.Id > 0 && el != null)
            {
                RefreshCsv(lst);
                lst = GetPokemonListNoFilter().OrderBy(x => x.Id).ToList();
            }            

            var query = lst.AsQueryable();
            query = ExpressionEvaluator<Pokemon>.OrderBy(query, oBaseFilter);
            query = ExpressionEvaluator<Pokemon>.Paging(query, oBaseFilter);


            return await Task.FromResult(query.ToList());
        }        

        public async Task<IEnumerable<Pokemon>> GetPokemonListByBaseFilter(BaseFilter oBaseFilter)
        {
            oBaseFilter.columnOrderBy = string.IsNullOrEmpty(oBaseFilter.columnOrderBy) ? "Id" : oBaseFilter.columnOrderBy;

            var lst = GetPokemonList(oBaseFilter).ToList();

            var query = lst.AsQueryable();
            query = ExpressionEvaluator<Pokemon>.OrderBy(query, oBaseFilter);
            query = ExpressionEvaluator<Pokemon>.Paging(query, oBaseFilter);


            return await Task.FromResult(query.ToList());

        }

        public async Task<IEnumerable<Pokemon>> InsertPokemon(PokemonFilter oPokemonFilter)
        {
            var oBaseFilter = new BaseFilter
            {
                searchText = "",
                columnOrderBy = "Id",
                desc = true,
                take = 0,
                skip = 0
            };

            var lst = GetPokemonListNoFilter().ToList();
            var newId = lst.Max(x => x.Id) + 1;

            var newPokemon = new Pokemon
            {
                Id = newId,
                Name = string.IsNullOrEmpty(oPokemonFilter.Name) ? "" : oPokemonFilter.Name,
                Type1 = string.IsNullOrEmpty(oPokemonFilter.Type1) ? "" : oPokemonFilter.Type1,
                Type2 = string.IsNullOrEmpty(oPokemonFilter.Type2) ? "" : oPokemonFilter.Type2,
                Total = oPokemonFilter.Total,
                Hp = oPokemonFilter.Hp,
                Attack = oPokemonFilter.Attack,
                Defense = oPokemonFilter.Defense,
                Sp_Attack = oPokemonFilter.Sp_Attack,
                Sp_Defense = oPokemonFilter.Sp_Defense,
                Speed = oPokemonFilter.Speed,
                Generation = oPokemonFilter.Generation,
                Legendary = oPokemonFilter.Legendary
            };

            lst.Add(newPokemon);

            var query = lst.AsQueryable();
            query = ExpressionEvaluator<Pokemon>.OrderBy(query, oBaseFilter);
            query = ExpressionEvaluator<Pokemon>.Paging(query, oBaseFilter);


            return await Task.FromResult(query.ToList());

        }

        public async Task<IEnumerable<Pokemon>> UpdatePokemon(PokemonFilter oPokemonFilter)
        {
            var oBaseFilter = new BaseFilter
            {
                searchText = "",
                columnOrderBy = "Id",
                desc = false,
                take = 0,
                skip = 0
            };

            var lst = GetPokemonListNoFilter().OrderBy(x => x.Id).ToList();

            var idx = lst.FindIndex(x => x.Id == oPokemonFilter.Id);

            if (!string.IsNullOrEmpty(oPokemonFilter.Name)) lst[idx].Name = oPokemonFilter.Name;
            if(!string.IsNullOrEmpty(oPokemonFilter.Type1)) lst[idx].Type1 = oPokemonFilter.Type1;
            if (!string.IsNullOrEmpty(oPokemonFilter.Type2)) lst[idx].Type2 = oPokemonFilter.Type2;
            if (!lst.Find(x => x.Id == oPokemonFilter.Id).Total.Equals(oPokemonFilter.Total)) lst[idx].Total = oPokemonFilter.Total;
            if (!lst.Find(x => x.Id == oPokemonFilter.Id).Hp.Equals(oPokemonFilter.Hp)) lst[idx].Hp = oPokemonFilter.Hp;
            if (!lst.Find(x => x.Id == oPokemonFilter.Id).Attack.Equals(oPokemonFilter.Attack)) lst[idx].Attack = oPokemonFilter.Attack;
            if (!lst.Find(x => x.Id == oPokemonFilter.Id).Defense.Equals(oPokemonFilter.Defense)) lst[idx].Defense = oPokemonFilter.Defense;
            if (!lst.Find(x => x.Id == oPokemonFilter.Id).Sp_Attack.Equals(oPokemonFilter.Sp_Attack)) lst[idx].Sp_Attack = oPokemonFilter.Sp_Attack;
            if (!lst.Find(x => x.Id == oPokemonFilter.Id).Sp_Defense.Equals(oPokemonFilter.Sp_Defense)) lst[idx].Sp_Defense = oPokemonFilter.Sp_Defense;
            if (!lst.Find(x => x.Id == oPokemonFilter.Id).Speed.Equals(oPokemonFilter.Speed)) lst[idx].Speed = oPokemonFilter.Speed;
            if (!lst.Find(x => x.Id == oPokemonFilter.Id).Generation.Equals(oPokemonFilter.Generation)) lst[idx].Generation = oPokemonFilter.Generation;
            if (!lst.Find(x => x.Id == oPokemonFilter.Id).Legendary.Equals(oPokemonFilter.Legendary)) lst[idx].Legendary = oPokemonFilter.Legendary;

            //Refresh CSV file
            RefreshCsv(lst);
            lst = GetPokemonListNoFilter().OrderBy(x => x.Id).ToList();

            var query = lst.AsQueryable();
            query = ExpressionEvaluator<Pokemon>.OrderBy(query, oBaseFilter);
            query = ExpressionEvaluator<Pokemon>.Paging(query, oBaseFilter);


            return await Task.FromResult(query.ToList());
        }

        public List<Pokemon> GetPokemonList(BaseFilter oBaseFilter)
        {
            var path = "Infrastructure.TechinicalExercise\\Pokemon\\Data\\pokemon.csv";
            var csv = new List<string[]>();
            var lines = File.ReadAllLines(@"pokemon.csv");

            var lst = File.ReadAllLines(@"pokemon.csv")
                                           .Skip(1)
                                           .Select(v => FromCsv(v))
                                           .ToList();

            var lst2 = (from l in lst
                        where (string.IsNullOrEmpty(oBaseFilter.searchText) ? true : (l.Name.ToLower().Trim().Contains(oBaseFilter.searchText.ToLower().Trim())))
                        select l
                        ).ToList();

            var query = lst2.AsQueryable();
            query = ExpressionEvaluator<Pokemon>.OrderBy(query, oBaseFilter);
            query = ExpressionEvaluator<Pokemon>.Paging(query, oBaseFilter);

            return query.ToList();
        }

        public void RefreshCsv(List<Pokemon> lst)
        {

            string newFileName = @"pokemon.csv";
            var csv = new List<string[]>();
            var lines = File.ReadAllLines(@"pokemon.csv");

            string headers = string.Join(",", lines[0]);
            var newLst = new List<string>();
            var lineDet = string.Empty;

            foreach (var item in lst)
            {
                lineDet = string.Empty;
                lineDet += item.Id.ToString() + ",";
                lineDet += item.Name.ToString() + ",";
                lineDet += item.Type1.ToString() + ",";
                lineDet += item.Type2.ToString() + ",";
                lineDet += item.Total.ToString() + ",";
                lineDet += item.Hp.ToString() + ",";
                lineDet += item.Attack.ToString() + ",";
                lineDet += item.Defense.ToString() + ",";
                lineDet += item.Sp_Attack.ToString() + ",";
                lineDet += item.Sp_Defense.ToString() + ",";
                lineDet += item.Speed.ToString() + ",";
                lineDet += item.Generation.ToString() + ",";
                lineDet += item.Legendary.ToString();
                newLst.Add(string.Join(",", lineDet));
            }

            string[] detail = newLst.ToArray();

            // File replacemente with new data
            string clientHeader = headers + Environment.NewLine;
            File.WriteAllText(newFileName, clientHeader);

            foreach (var item in detail)
            {
                File.AppendAllText(newFileName, string.Join(",", item.ToString()) + Environment.NewLine);
            }            

        }

        public List<Pokemon> GetPokemonListNoFilter()
        {
            var path = "Infrastructure.TechinicalExercise\\Pokemon\\Data\\pokemon.csv";
            var csv = new List<string[]>();
            var lines = File.ReadAllLines(@"pokemon.csv");

            var lst = File.ReadAllLines(@"pokemon.csv")
                                           .Skip(1)
                                           .Select(v => FromCsv(v))
                                           .ToList();

            var oBaseFilter = new BaseFilter
            {
                searchText = "",
                columnOrderBy = "Name",
                desc = false,
                take = 0,
                skip = 0
            };

            var query = lst.AsQueryable();
            query = ExpressionEvaluator<Pokemon>.OrderBy(query, oBaseFilter);
            query = ExpressionEvaluator<Pokemon>.Paging(query, oBaseFilter);

            return query.ToList();
        }

        public Pokemon FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            Pokemon pokemonValues = new Pokemon();
            pokemonValues.Id = Convert.ToInt32(values[0]);
            pokemonValues.Name = values[1];
            pokemonValues.Type1 = values[2];
            pokemonValues.Type2 = values[3];
            pokemonValues.Total = Convert.ToDecimal(values[4]);
            pokemonValues.Hp = Convert.ToDecimal(values[5]);
            pokemonValues.Attack = Convert.ToDecimal(values[6]);
            pokemonValues.Defense = Convert.ToDecimal(values[7]);
            pokemonValues.Sp_Attack = Convert.ToDecimal(values[8]);
            pokemonValues.Sp_Defense = Convert.ToDecimal(values[9]);
            pokemonValues.Speed = Convert.ToDecimal(values[10]);
            pokemonValues.Generation = Convert.ToInt32(values[11]);
            pokemonValues.Legendary = Convert.ToBoolean(values[12]);

            return pokemonValues;
        }
    }
}
