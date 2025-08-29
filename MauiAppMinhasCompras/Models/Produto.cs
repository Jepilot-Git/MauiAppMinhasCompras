﻿using SQLite;

namespace MauiAppMinhasCompras.Models
{
    public class Produto
    {
        [PrimaryKey, AutoIncrement] 
        public int Id { get; set; }
        public string Descrição { get; set; }
        public double Quantidade { get; set; }
        public double Preço {  get; set; }
        public double Total { get => Quantidade * Preço; }
    }
}
