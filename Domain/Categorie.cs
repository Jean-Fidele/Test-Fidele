using System;
using System.Text.Json.Serialization;

namespace Domain
{
    public class Categorie
    {
        public int Code { get; set; }
        public string Libelle { get; set; }

        public virtual ICollection<Produit> Produits { get; set; }
    }
}