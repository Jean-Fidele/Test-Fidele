using Test_Fidele.Models.Categories;

namespace Test_Fidele.Models.Produits
{
    public class ProduitRep
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public CategorieRep Categorie { get; set; }
    }

    public class ProduitRepList
    {
        public ProduitRepList()
        {
            Produits = new List<ProduitRep>();
        }
        public int totale { get; set; }
        public List<ProduitRep> Produits { get; set; }
    }
}