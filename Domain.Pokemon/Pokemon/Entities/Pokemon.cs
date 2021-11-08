using Domain.Core.ModelFilter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Pokemon.Entities
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type1 { get; set; }
        public string Type2 { get; set; }
        public decimal Total { get; set; }
        public decimal Hp { get; set; }
        public decimal Attack { get; set; }
        public decimal Defense { get; set; }
        public decimal Sp_Attack { get; set; }
        public decimal Sp_Defense { get; set; }
        public decimal Speed { get; set; }
        public int Generation { get; set; }
        public bool Legendary { get; set; }
    }
}
