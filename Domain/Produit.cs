namespace Domain
{
    public class Produit
    {
        public int Code { get; set; }
        public string Name { get;set; }

        public int CategorieId { get; set; }
        public virtual Categorie Categorie { get; set; }
    }
}